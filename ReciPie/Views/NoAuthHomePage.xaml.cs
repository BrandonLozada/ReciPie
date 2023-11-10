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
	public partial class NoAuthHomePage : ContentPage
	{
        RecipieRepository _RecipieRepository = new RecipieRepository();

        public NoAuthHomePage()
        {
            InitializeComponent();

            RecipieListView.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
        }

        protected override async void OnAppearing()
        {
            var recipies = await _RecipieRepository.GetAll();
            RecipieListView.ItemsSource = null;
            RecipieListView.ItemsSource = recipies;
            RecipieListView.IsRefreshing = false;
        }

        public async void GoToSignInPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignInView());
        }

        public async void GoToSignUpPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpView());
        }

        private void RecipieListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }
            var recipie = e.Item as Recipie;
            Navigation.PushAsync(new RecipeDetailsPage(recipie));
            ((ListView)sender).SelectedItem = null;
        }

        private async void TxtSearch_SearchButtonPressed(object sender, EventArgs e)
        {
            string searchValue = TxtSearch.Text;
            if (!String.IsNullOrEmpty(searchValue))
            {
                var recipies = await _RecipieRepository.GetAllByName(searchValue);
                RecipieListView.ItemsSource = null;
                RecipieListView.ItemsSource = recipies;
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
                var recipies = await _RecipieRepository.GetAllByName(searchValue);
                RecipieListView.ItemsSource = null;
                RecipieListView.ItemsSource = recipies;
            }
            else
            {
                OnAppearing();
            }
        }
    }
}