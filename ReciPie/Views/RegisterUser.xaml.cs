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
    public partial class RegisterUser : ContentPage
    {

        UserRepository _userRepository = new UserRepository();
        public RegisterUser()
        {
            InitializeComponent();
        }

        private async void ButtonRegister_Clicked(object sender, EventArgs e)
        {
            try
            {
                string name = TxtName.Text;
                string email = TxtEmail.Text;
                string password = TxtPassword.Text;
                string confirmPassword = TxtConfirmPass.Text;
                if (String.IsNullOrEmpty(name))
                {
                    await DisplayAlert("Advertencia", "Ingresa tu Nombre de Usuario", "Ok");
                    return;
                }
                else if (String.IsNullOrEmpty(email))
                {
                    await DisplayAlert("Advertencia", "Ingresa tu Email", "Ok");
                    return;
                }
                else if (String.IsNullOrEmpty(password))
                {
                    await DisplayAlert("Advertencia", "Ingresa tu Contraseña", "Ok");
                    return;
                }
                else if (password.Length<6)
                {
                    await DisplayAlert("Advertencia", "La contraseña debe de ser mayor a 6 Digitos", "Ok");
                    return;
                }
                else if (String.IsNullOrEmpty(confirmPassword))
                {
                    await DisplayAlert("Advertencia", "Ingresa tu Confirmación Contraseña", "Ok");
                    return;
                }
                else if (password != confirmPassword)
                {
                    await DisplayAlert("Advertencia", "Las contraseñas no coinciden", "Ok");
                    return;
                }

                bool isSave = await _userRepository.Register(email, name, password);
                if (isSave)
                {
                    await DisplayAlert("Usuario Registrado", "El registro ha sido un exito", "Ok");
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Advertencia", "Registro incorrecto", "Ok");
                }
            }
            catch(Exception exception)
            {
               if(exception.Message.Contains("EMAIL_EXISTS"))
                {
                   await DisplayAlert("Advertencia", "El Email ya existe, Ingresa otro distinto", "Ok");
                }
                else
                {
                    await DisplayAlert("Exito", "Cuenta creada correctamente" , "Ok");
                }
                
            }
            

        }   
    }
}