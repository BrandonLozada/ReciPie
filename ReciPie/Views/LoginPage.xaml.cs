using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ReciPie.Views.Student;

namespace ReciPie.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        UserRepository _userRepository = new UserRepository();
        public ICommand TapCommand => new Command(async() => await Navigation.PushModalAsync(new RegisterUser()));

        public LoginPage()
        {
            InitializeComponent();
            //bool hasKey = Preferences.ContainsKey("token");
            //if(hasKey)
            //{
            //    string token = Preferences.Get("token", "");
            //    if(!string.IsNullOrEmpty(token))
            //    {
            //        Navigation.PushAsync(new HomePage());
            //    }
            //}
        }

        private async void BtnSignIn_Clicked(object sender, EventArgs e)
        {
            try
            {
                isVisible.IsVisible = true;
                isLoading.IsRunning = true;
                IsBusy = true;

                string email = TxtEmail.Text;
                string password = TxtPassword.Text;
                if (String.IsNullOrEmpty(email))
                {
                    await DisplayAlert("Advertencia", "Ingresa tu Email", "Ok");
                    return;
                }
                if (String.IsNullOrEmpty(password))
                {
                    await DisplayAlert("Advertencia", "Ingresa tu Contraseña", "Ok");
                    return;
                }
                string token = await _userRepository.SignIn(email, password);
                if (!string.IsNullOrEmpty(token))
                {
                    Preferences.Set("token", token);
                    Preferences.Set("userPassword", password);
                    Preferences.Set("userEmail", email);
                    await Navigation.PushAsync(new HomePage());
                }
                else
                {
                    await DisplayAlert("Advertencia", "Inicio de sesión fallido", "ok");
                }
            }
            catch(Exception exception)
            {
                if(exception.Message.Contains("INVALID_EMAIL"))
                {
                    await DisplayAlert("Advertencia", "Email no encontrado", "ok");
                }
                else if (exception.Message.Contains("EMAIL_NOT_FOUND"))
                {
                    await DisplayAlert("Advertencia", "Email no encontrado", "ok");
                }
                else if(exception.Message.Contains("INVALID_PASSWORD"))
                {
                    await DisplayAlert("Advertencia", "Contraseña incorrecta", "ok");
                }
                else
                {
                    await DisplayAlert("Error", exception.Message, "ok");
                }
            }
            isVisible.IsVisible = false;
            isLoading.IsRunning = false;
        }

        private async void RegisterTap_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterUser());
        }

        private async void ForgotTap_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ForgotPasswordPage());
        }
    }
}