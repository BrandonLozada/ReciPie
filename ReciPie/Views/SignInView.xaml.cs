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
using ReciPie.Repositories;
using Newtonsoft.Json;

namespace ReciPie.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInView : ContentPage
    {
        UserRepository _UserRepository = new UserRepository();
        public ICommand TapCommand => new Command(async() => await Navigation.PushModalAsync(new SignUpView()));

        public SignInView()
        {
            InitializeComponent();
        }

        private async void BtnSignIn_Clicked(object sender, EventArgs e)
        {
            try
            {
                isVisible.IsVisible = true;
                isLoading.IsRunning = true;

                string email = TxtEmail.Text;
                string password = TxtPassword.Text;

                if (string.IsNullOrEmpty(email))
                {
                    await DisplayAlert("Advertencia", "Ingresa tu Email", "Ok");
                    return;
                }
                if (string.IsNullOrEmpty(password))
                {
                    await DisplayAlert("Advertencia", "Ingresa tu Contraseña", "Ok");
                    return;
                }

                var UserCredential = await _UserRepository.SignIn(email, password);

                if (!UserCredential.Equals(new object()))
                {
                    string jsonString = JsonConvert.SerializeObject(UserCredential);
                    Preferences.Set("UserCredential", jsonString);
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
            await Navigation.PushAsync(new SignUpView());
        }

        private async void ForgotTap_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ForgotPasswordPage());
        }
    }
}