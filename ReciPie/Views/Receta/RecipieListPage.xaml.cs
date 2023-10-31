using ReciPie.Models;
using ReciPie.Views.Receta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReciPie.Views.Producto
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipieListPage : ContentPage
    {
        ProductRepository productRepository = new ProductRepository();
        RecipieRepository _RecipieRepository = new RecipieRepository();

        public RecipieListPage()
        {
            InitializeComponent();

            ProductListView.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
        }

        protected override async void OnAppearing()
        {
            var recipies = await _RecipieRepository.GetAll();
            ProductListView.ItemsSource = null;
            ProductListView.ItemsSource = recipies;
            ProductListView.IsRefreshing = false;
        }

        private void AddToolBarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RecetaEntry());
        }

        private void ProductListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }
            var recipie = e.Item as Recipie;
            Navigation.PushAsync(new RecipieDetails(recipie));
            ((ListView)sender).SelectedItem = null;
        }

        private async void DeleteTapp_Tapped(object sender, EventArgs e)
        {

            var response = await DisplayAlert("Advertencia", "¿Quieres eliminar este Producto?", "Yes", "No");
            if (response)
            {
                string id = ((TappedEventArgs)e).Parameter.ToString();
                bool isDelete = await productRepository.Delete(id);
                if (isDelete)
                {
                    await DisplayAlert("Advertencia", "El Producto ha sido eliminado", "Ok");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Error", "No se eliminó el Producto", "Ok");
                }
            }
        }

        private async void EditTap_Tapped(object sender, EventArgs e)
        {
            //DisplayAlert("Edit", "Do you want to Edit?", "Ok");

            string id = ((TappedEventArgs)e).Parameter.ToString();
            var product = await productRepository.GetById(id);
            if (product == null)
            {
                await DisplayAlert("Advertencia", "Datos no encontrados", "Ok");
            }
            product.Id = id;
            await Navigation.PushAsync(new ProductEdit(product));

        }

        private async void TxtSearch_SearchButtonPressed(object sender, EventArgs e)
        {
            string searchValue = TxtSearch.Text;
            if (!String.IsNullOrEmpty(searchValue))
            {
                var products = await productRepository.GetAllByName(searchValue);
                ProductListView.ItemsSource = null;
                ProductListView.ItemsSource = products;
            }
            else
            {
                OnAppearing();
            }
        }

        private async void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchValue = TxtSearch.Text;
            if (!String.IsNullOrEmpty(searchValue))
            {
                var products = await productRepository.GetAllByName(searchValue);
                ProductListView.ItemsSource = null;
                ProductListView.ItemsSource = products;
            }
            else
            {
                OnAppearing();
            }
        }

        private async void EditMenuItem_Clicked(object sender, EventArgs e)
        {
            string id = ((MenuItem)sender).CommandParameter.ToString();
            var product = await productRepository.GetById(id);
            if (product == null)
            {
                await DisplayAlert("Advertencia", "Datos no encontrados", "Ok");
            }
            product.Id = id;
            await Navigation.PushModalAsync(new ProductEdit(product));
        }

        private async void DeleteMenuItem_Clicked(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Advertencia", "¿Quiere eliminar este producto?", "Yes", "No");
            if (response)
            {
                string id = ((MenuItem)sender).CommandParameter.ToString();
                bool isDelete = await productRepository.Delete(id);
                if (isDelete)
                {
                    await DisplayAlert("Advertencia", "El producto ha sido eliminado", "Ok");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Error", "Error, producto no eliminado", "Ok");
                }
            }
        }

        private async void EditSwipeItem_Invoked(object sender, EventArgs e)
        {
            string id = ((MenuItem)sender).CommandParameter.ToString();
            var product = await productRepository.GetById(id);
            if (product == null)
            {
                await DisplayAlert("Warning", "Data not found.", "Ok");
            }
            product.Id = id;
            await Navigation.PushModalAsync(new ProductEdit(product));
        }

    }
}