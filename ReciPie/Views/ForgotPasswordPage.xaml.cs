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
                isVisible.IsVisible = true;
                isLoading.IsRunning = true;

                string email = TxtEmail.Text;

                if (string.IsNullOrEmpty(email))
                {
                    await DisplayAlert("Advertencia", "Ingresa tu correo electrónico.", "Aceptar");
                    return;
                }

                bool isSend = await _userRepository.ResetPassword(email);

                if (isSend)
                {
                    await DisplayAlert("Éxito", "Se ha enviado un correo a tu correo electrónico.", "Aceptar");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Hubo un error al enviar el correo.", "Aceptar");
                }
            }
            catch (Exception exception)
            {
                if (exception.Message.Contains("INVALID_EMAIL"))
                {
                    await DisplayAlert("Advertencia", "Correo inválido, ingresa un correo electrónico existente.", "Aceptar");
                } 
                else
                {
                    await DisplayAlert("Advertencia", "No existe una cuenta asociada a ese correo electrónico.", "Aceptar");

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