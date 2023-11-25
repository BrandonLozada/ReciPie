using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ReciPie.Views;
using ReciPie.Models;

namespace ReciPie.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        RecipieRepository _RecipieRepository = new RecipieRepository();

        public HomePage()
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

        private void GoToMyRecipesPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MyRecipesPage());
        }             
        
        private void GoToAddRecipiePage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddRecipePage());
        }
        
        private void GoToMyProfilePage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MyProfilePage());
        }

        private void BtnChangePassword_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChangePassword());
        }

        async void LogOut_Clicked(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Advertencia", "¿Está seguro que desea cerrar sesión en ReciPie?", "Sí", "No");
            
            if (response)
            {
                Preferences.Remove("UserCredential");
                App.Current.MainPage = new NavigationPage(new SignInView());
            }
        }
    }
}