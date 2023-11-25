using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using Firebase.Auth;

namespace ReciPie.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePassword : ContentPage
    {
        UserRepository _userRepository = new UserRepository();
        public ChangePassword()
        {
            InitializeComponent();
        }

        private async void BtnChangePassword_Clicked(object sender, EventArgs e)
        {
            try
            {
                isVisible.IsVisible = true;
                isLoading.IsRunning = true;

                string password = TxtPassword.Text;
                string confirmPass = TxtConfirm.Text;

                if(string.IsNullOrEmpty(password))
                {
                   await DisplayAlert("Advertencia", "La contraseña es necesaria.","Aceptar");
                    return;
                }
                if (string.IsNullOrEmpty(confirmPass))
                {
                  await  DisplayAlert("Advertencia", "La confirmación de contraseña es necesaria.", "Aceptar");
                    return;
                }
                if(password!=confirmPass)
                {
                    await DisplayAlert("Advertencia", "La confirmación no coincide con la contraseña.", "Aceptar");
                    return;
                }
                string token = Preferences.Get("UserCredential", "");
                var UserCredential = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get("UserCredential", ""));

                var newUserCredential = await _userRepository.ChangePassword(UserCredential.FirebaseToken, password);

                if (!newUserCredential.Equals(new object()))
                {
                    await DisplayAlert("Exito", "La contraseña de tu cuenta en ReciPie ha cambiado.", "Aceptar");

                    string jsonString = JsonConvert.SerializeObject(newUserCredential);
                    Preferences.Set("UserCredential", jsonString);
                    //Preferences.Clear();
                    await Navigation.PushAsync(new SignInView());
                }
                else
                {
                    await DisplayAlert("Error", "Hubo un error en el cambio de contraseña.", "Aceptar");
                }
            }
            catch(Exception ex)
            {
                // TODO: Manejar mensajes de excepción.
            }
            finally
            {
                isVisible.IsVisible = false;
                isLoading.IsRunning = false;
            }
        }
    }
}