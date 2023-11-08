using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ReciPie.Views;

namespace ReciPie.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            //LblUser.Text = Preferences.Get("userEmail", "default");
        }

        private void GoToMyRecipesPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MyRecipesPage());
        }        
        
        private void GoToMyProfilePage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MyProfilePage());
        }

        private void BtnChangePassword_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChangePassword());
        }

        void LogOut_Clicked(System.Object sender, System.EventArgs e)
        {
            Preferences.Remove("MyFirebaseRefreshToken");
            App.Current.MainPage = new NavigationPage(new SignInView());
        }
    }
}