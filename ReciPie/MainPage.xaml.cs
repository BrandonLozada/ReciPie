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

            bool hasKey = Preferences.ContainsKey("token");
            if (hasKey)
            {
                string token = Preferences.Get("token", "");
                if (!string.IsNullOrEmpty(token))
                {
                    Navigation.PushAsync(new HomePage());
                }
            }
        }

        public async void GoToSignInPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignInView());
        }

        public async void GoToSignUpPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpView());
        }        
        
        public async void GoToHomePage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());
        }
    }
}
