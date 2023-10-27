using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReciPie.Views.Proveedor
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProveedorListPage : ContentPage
    {
        ProveedorRepository proveedorRepository = new ProveedorRepository();

        public ProveedorListPage()
        {
            InitializeComponent();

            ProveedorListView.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
        }

        protected override async void OnAppearing()
        {
            var prover = await proveedorRepository.GetAll();
            ProveedorListView.ItemsSource = null;
            ProveedorListView.ItemsSource = prover;
            ProveedorListView.IsRefreshing = false;

        }

        private void AddToolBarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProveedorEntry());
        }

        private void ProveedorListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }
            var prove = e.Item as ProveedorModel;
            Navigation.PushAsync(new ProveedorDetails(prove));
            ((ListView)sender).SelectedItem = null;

        }



        private async void DeleteTapp_Tapped(object sender, EventArgs e)
        {

            var response = await DisplayAlert("Advertencia", "¿Quieres eliminar este Proveedor?", "Yes", "No");
            if (response)
            {
                string id = ((TappedEventArgs)e).Parameter.ToString();
                bool isDelete = await proveedorRepository.Delete(id);
                if (isDelete)
                {
                    await DisplayAlert("Advertencia", "El Proveedor ha sido eliminado", "Ok");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Error", "No se eliminó el Proveedor", "Ok");
                }
            }
        }

        private async void EditTap_Tapped(object sender, EventArgs e)
        {
            //DisplayAlert("Edit", "Do you want to Edit?", "Ok");

            string id = ((TappedEventArgs)e).Parameter.ToString();
            var prov = await proveedorRepository.GetById(id);
            if (prov == null)
            {
                await DisplayAlert("Advertencia", "Datos no encontrados", "Ok");
            }
            prov.Id = id;
            await Navigation.PushAsync(new ProveedorEdit(prov));

        }

        private async void TxtSearch_SearchButtonPressed(object sender, EventArgs e)
        {
            string searchValue = TxtSearch.Text;
            if (!String.IsNullOrEmpty(searchValue))
            {
                var provers = await proveedorRepository.GetAllByName(searchValue);
                ProveedorListView.ItemsSource = null;
                ProveedorListView.ItemsSource = provers;
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
                var provers = await proveedorRepository.GetAllByName(searchValue);
                ProveedorListView.ItemsSource = null;
                ProveedorListView.ItemsSource = provers;
            }
            else
            {
                OnAppearing();
            }
        }

        private async void EditMenuItem_Clicked(object sender, EventArgs e)
        {
            string id = ((MenuItem)sender).CommandParameter.ToString();
            var product = await proveedorRepository.GetById(id);
            if (product == null)
            {
                await DisplayAlert("Advertencia", "Datos no encontrados", "Ok");
            }
            product.Id = id;
            await Navigation.PushModalAsync(new ProveedorEdit(product));
        }

        private async void DeleteMenuItem_Clicked(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Advertencia", "¿Quiere eliminar este proveedor?", "Yes", "No");
            if (response)
            {
                string id = ((MenuItem)sender).CommandParameter.ToString();
                bool isDelete = await proveedorRepository.Delete(id);
                if (isDelete)
                {
                    await DisplayAlert("Advertencia", "El proveedor ha sido eliminado", "Ok");
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
            var product = await proveedorRepository.GetById(id);
            if (product == null)
            {
                await DisplayAlert("Warning", "Data not found.", "Ok");
            }
            product.Id = id;
            await Navigation.PushModalAsync(new ProveedorEdit(product));
        }
    }
}