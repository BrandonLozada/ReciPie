using Firebase.Auth;
using Newtonsoft.Json;
using ReciPie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.CommunityToolkit.Extensions;

namespace ReciPie.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilePage : ContentPage
    {
        UserInfoRepository _UserÍnfoRepository = new UserInfoRepository();
        AddUserInformation _myInfo = new AddUserInformation();

        public EditProfilePage()
        {
            InitializeComponent();
        }

        private async void BtnEditProfile_Clicked(object sender, EventArgs e)
        {
            //TODO: Agregar un "si existe esta key, editamela, si no, creala".
            string firstName = FirstName.Text;
            string lastName = LastName.Text;
            string gender = Gender.SelectedItem.ToString();
            string birthdate = Birthdate.Date.ToShortDateString();
            string phone = Phone.Text;
            string country = Country.Text;

            if (string.IsNullOrEmpty(firstName))
            {
                await DisplayAlert("Advertencia", "El nombre es necesario.", "Aceptar");
                return;
            }
            else if (string.IsNullOrEmpty(lastName))
            {
                await DisplayAlert("Advertencia", "Los apellidos son necesarios.", "Aceptar");
                return;
            }
            else if (string.IsNullOrEmpty(gender))
            {
                await DisplayAlert("Advertencia", "El genéro es necesario.", "Aceptar");
                return;
            }
            else if (string.IsNullOrEmpty(birthdate))
            {
                await DisplayAlert("Advertencia", "La fecha de nacimiento es necesaria.", "Aceptar");
                return;
            }
            else if (string.IsNullOrEmpty(phone))
            {
                await DisplayAlert("Advertencia", "El teléfono es necesario.", "Aceptar");
                return;
            }
            else if (string.IsNullOrEmpty(country))
            {
                await DisplayAlert("Advertencia", "La ciudad es necesaria.", "Aceptar");
                return;
            }

            try
            {
                isVisible.IsVisible = true;
                isLoading.IsRunning = true;

                var UserCredential = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get("UserCredential", ""));
                string UserId = UserCredential.User.LocalId;

                _myInfo.FirstName = firstName;
                _myInfo.LastName = lastName;
                _myInfo.Gender = gender;
                _myInfo.Birthdate = birthdate;
                _myInfo.Phone = phone;
                _myInfo.Country = country;

                bool isCreated = await _UserÍnfoRepository.AddUserInformation(UserId, _myInfo);

                if (isCreated)
                {
                    await Navigation.PushAsync(new HomePage());
                    await this.DisplayToastAsync("Información agregada correctamente.", 6000);
                }
                else
                {
                    await DisplayAlert("Error", "Hubo un error al editar la información del perfil.", "Aceptar");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Hubo un error al editar la receta.\n{ex.Message}", "Aceptar");
            }
            finally
            {
                isVisible.IsVisible = false;
                isLoading.IsRunning = false;
            }
        }
    }
}