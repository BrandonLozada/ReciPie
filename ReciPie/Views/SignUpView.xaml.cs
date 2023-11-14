using Newtonsoft.Json;
using ReciPie.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReciPie.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpView : ContentPage
    {
        UserRepository _UserRepository = new UserRepository();

        public SignUpView()
        {
            InitializeComponent();
        }

        private async void ButtonRegister_Clicked(object sender, EventArgs e)
        {
            try
            {
                isVisible.IsVisible = true;
                isLoading.IsRunning = true;

                string name = TxtName.Text;
                string email = TxtEmail.Text;
                string password = TxtPassword.Text;
                string confirmPassword = TxtConfirmPass.Text;

                if (string.IsNullOrEmpty(name))
                {
                    await DisplayAlert("Advertencia", "El titulo es necesario.", "Aceptar");
                    return;
                }
                if (!await Repository.ValidateEmail(email))
                {
                    await DisplayAlert("Advertencia", "El formato de correo electrónico es inválido.", "Aceptar");
                    return;
                }
                else if (string.IsNullOrEmpty(password))
                {
                    await DisplayAlert("Advertencia", "La contraseña es necesaria.", "Aceptar");
                    return;
                }
                else if (password.Length<6)
                {
                    await DisplayAlert("Advertencia", "La contraseña debe de ser mayor a 6 dígitos", "Aceptar");
                    return;
                }
                else if (string.IsNullOrEmpty(confirmPassword))
                {
                    await DisplayAlert("Advertencia", "La confirmación de contraseña es necesaria.", "Aceptar");
                    return;
                }
                else if (password != confirmPassword)
                {
                    await DisplayAlert("Advertencia", "La confirmación no coincide con la contraseña.", "Aceptar");
                    return;
                }

                var UserCredential = await _UserRepository.Register(email, name, password);

                if (!UserCredential.Equals(new object()))
                {
                    string jsonString = JsonConvert.SerializeObject(UserCredential);
                    Preferences.Set("UserCredential", jsonString);
                    Preferences.Set("userPassword", password);
                    Preferences.Set("userEmail", email);

                    await DisplayAlert("¡Bienvenido!", $"Haz creado tu cuenta e iniciado sesión en ReciPie", "Aceptar");
                    await Navigation.PushAsync(new HomePage());
                }
                else
                {
                    await DisplayAlert("Error", "Hubo un error al crear la cuenta.", "Aceptar");
                }
            }
            catch(Exception exception)
            {
               // TODO: Manejar mensajes de excepción.
               if(exception.Message.Contains("EMAIL_EXISTS"))
                {
                   await DisplayAlert("Advertencia", "El Email ya existe, Ingresa otro distinto", "Aceptar");
                }
                else
                {
                    await DisplayAlert("Exito", "Cuenta creada correctamente" , "Aceptar");
                }
            }
            finally
            {
                isVisible.IsVisible = false;
                isLoading.IsRunning = false;
            }
        }   
    }
}