using ReciPie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReciPie.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyRecipesPage : ContentPage
    {
        RecipieRepository _RecipieRepository = new RecipieRepository();

        public MyRecipesPage()
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
            Navigation.PushAsync(new AddRecipePage());
        }

        private void ProductListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }
            var recipie = e.Item as Recipie;
            Navigation.PushAsync(new RecipeDetailsPage(recipie));
            ((ListView)sender).SelectedItem = null;
        }

        private async void DeleteTapp_Tapped(object sender, EventArgs e)
        {

            var response = await DisplayAlert("Advertencia", "¿Quieres eliminar este Producto?", "Yes", "No");
            if (response)
            {
                string id = ((TappedEventArgs)e).Parameter.ToString();
                bool isDelete = await _RecipieRepository.Delete(id);
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
            string id = ((TappedEventArgs)e).Parameter.ToString();
            var recipie = await _RecipieRepository.GetById(id);
           
            if (recipie == null)
            {
                await DisplayAlert("Advertencia", "Datos no encontrados.", "Aceptar");
            }
            recipie.Id = id;
            await Navigation.PushAsync(new EditRecipePage(recipie));
        }

        private async void EditMenuItem_Clicked(object sender, EventArgs e)
        {
            string id = ((MenuItem)sender).CommandParameter.ToString();
            var recipie = await _RecipieRepository.GetById(id);
            
            if (recipie == null)
            {
                await DisplayAlert("Advertencia", "Datos no encontrados.", "Aceptar");
            }
            recipie.Id = id;
            await Navigation.PushModalAsync(new EditRecipePage(recipie));
        }

        private async void TxtSearch_SearchButtonPressed(object sender, EventArgs e)
        {
            string searchValue = TxtSearch.Text;
            if (!string.IsNullOrEmpty(searchValue))
            {
                var recipies = await _RecipieRepository.GetAllByName(searchValue);
                ProductListView.ItemsSource = null;
                ProductListView.ItemsSource = recipies;
            }
            else
            {
                OnAppearing();
            }
        }

        private async void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchValue = TxtSearch.Text;
            if (!string.IsNullOrEmpty(searchValue))
            {
                var recipies = await _RecipieRepository.GetAllByName(searchValue);
                ProductListView.ItemsSource = null;
                ProductListView.ItemsSource = recipies;
            }
            else
            {
                OnAppearing();
            }
        }

        private async void DeleteMenuItem_Clicked(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Advertencia", "¿Estás seguro de eliminar esta receta?", "Sí", "No");
            
            if (response)
            {
                string id = ((MenuItem)sender).CommandParameter.ToString();
                bool isDelete = await _RecipieRepository.Delete(id);
                if (isDelete)
                {
                    await DisplayAlert("Advertencia", "La receta ha sido eliminada.", "Aceptar");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Error", "Hubo un error en la eliminación de la receta.", "Aceptar");
                }
            }
        }
    }
}