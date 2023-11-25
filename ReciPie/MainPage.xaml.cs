using ReciPie.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace ReciPie
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            // TODO: En el momento que mostremos la aplicación, descomentamos esta parte del código.
            //bool hasUserCredential = Preferences.ContainsKey("UserCredential");
            
            //if (hasUserCredential)
            //{
            //    string UserCredential = Preferences.Get("UserCredential", "");
            //    if (!string.IsNullOrEmpty(UserCredential))
            //    {
            //        Navigation.PushAsync(new HomePage());
            //    }
            //}
        }

        public async void GoToSignInPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignInView());
        }

        public async void GoToSignUpPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpView());
        }        
        
        public async void GoToNoAuthHomePage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NoAuthHomePage());
        }
    }
}
