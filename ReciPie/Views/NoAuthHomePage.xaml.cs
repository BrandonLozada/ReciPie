using ReciPie.Models;
using ReciPie.Views.Receta;
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
	public partial class NoAuthHomePage : ContentPage
	{
        ProductRepository productRepository = new ProductRepository();
        RecipieRepository _RecipieRepository = new RecipieRepository();

        public NoAuthHomePage()
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

        public async void GoToSignInPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignInView());
        }

        public async void GoToSignUpPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpView());
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
    }
}