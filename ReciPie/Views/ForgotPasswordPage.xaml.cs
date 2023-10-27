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
    public partial class ForgotPasswordPage : ContentPage
    {
        UserRepository _userRepository = new UserRepository();
        public ForgotPasswordPage()
        {
            InitializeComponent();
        }

        private async void ButtonSendLink_Clicked(object sender, EventArgs e)
        {
            try
            {
                string email = TxtEmail.Text;
                if (string.IsNullOrEmpty(email))
                {
                    await DisplayAlert("Advertencia", "Ingresa tu Email", "Ok");
                    return;
                }

                bool isSend = await _userRepository.ResetPassword(email);
                if (isSend)
                {
                    await DisplayAlert("Email Enviado", "Se ha enviado un correo a tu Email", "Ok");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Advertencia", "El Link no se ha enviado", "Ok");
                }
            }
            catch (Exception exception)
            {
                if (exception.Message.Contains("INVALID_EMAIL"))
                {
                    await DisplayAlert("Advertencia", "Email Invalido, ingresa un email existente", "Ok");
                } 
                else
                {
                    await DisplayAlert("Advertencia", "El email no existe", "Ok");

                }
            }
                
            
        }
    }
}