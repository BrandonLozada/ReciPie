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
                string password = TxtPassword.Text;
                string confirmPass = TxtConfirm.Text;
                if(string.IsNullOrEmpty(password))
                {
                   await DisplayAlert("Advertencia", "Ingresa tu nueva contraseña","Ok");
                    return;
                }
                if (string.IsNullOrEmpty(confirmPass))
                {
                  await  DisplayAlert("Advertencia", "Por favor, confirme su contraseña", "Ok");
                    return;
                }
                if(password!=confirmPass)
                {
                    await DisplayAlert("Advertencia", "Las Contraseñas no coinciden", "Ok");
                    return;
                }
                string token = Preferences.Get("token","");
                string newToken =await _userRepository.ChangePassword(token, password);
                if(!string.IsNullOrEmpty(newToken))
                {
                    await DisplayAlert("Exito", "La contraseña se ha cambiado", "Ok");
                    Preferences.Set("token", newToken);
                    //Preferences.Clear();
                    //await Navigation.PushAsync(new SignInView());
                }
                else
                {
                    await DisplayAlert("Advertencia", "Cambio de contraseña fallido", "Ok");
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}