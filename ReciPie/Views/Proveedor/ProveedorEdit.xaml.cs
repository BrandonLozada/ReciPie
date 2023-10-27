using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace ReciPie.Views.Proveedor
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProveedorEdit : ContentPage
    {
        MediaFile file;
        ProveedorRepository proveedorRepository = new ProveedorRepository();

        public ProveedorEdit(ProveedorModel prover)
        {
            InitializeComponent();
            TxtName.Text = prover.Nombre;
            TxtDireccion.Text = prover.Direccion;
            TxtTelefono.Text = prover.Telefono;
            TxtId.Text = prover.Id;
            TxtGiro.Text = prover.Giro;
            TxtEmail.Text = prover.EmailProveedor;
        }

        private async void ButtonUpdate_Clicked(object sender, EventArgs e)
        {
            string name = TxtName.Text;
            string direccion = TxtDireccion.Text;
            string telefono = TxtTelefono.Text;
            string giro = TxtGiro.Text;
            string email = TxtEmail.Text;
            if (string.IsNullOrEmpty(name))
            {
                await DisplayAlert("Advertencia", "Por favor ingresa el Nombre del Proveedor", "Cancel");
                return;
            }
            else if (string.IsNullOrEmpty(direccion))
            {
                await DisplayAlert("Advertencia", "Por favor ingresa la Dirección del Proveedor", "Cancel");
                return;
            }
            else if (string.IsNullOrEmpty(telefono))
            {
                await DisplayAlert("Advertencia", "Por favor ingresa el Teléfono del Proveedor", "Cancel");
                return;
            }
            else if (string.IsNullOrEmpty(giro))
            {
                await DisplayAlert("Advertencia", "Por favor ingresa el Giro del Proveedor", "Cancel");
                return;
            }
            else if (string.IsNullOrEmpty(email))
            {
                await DisplayAlert("Advertencia", "Por favor ingresa el Email del Proveedor", "Cancel");
                return;
            }

            ProveedorModel prove = new ProveedorModel();
            prove.Id = TxtId.Text;
            prove.Nombre = name;
            prove.Direccion = direccion;
            prove.Telefono = telefono;
            prove.Giro = giro;
            prove.EmailProveedor = email;
            if (file != null)
            {
                string image = await proveedorRepository.Upload(file.GetStream(), Path.GetFileName(file.Path));
                prove.Image = image;
            }
            bool isUpdated = await proveedorRepository.Update(prove);
            if (isUpdated)
            {
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Actualización Fallida", "Cancel");
            }

        }

        private async void ImageTap_Tapped(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium
                });
                if (file == null)
                {
                    return;
                }
                StudentImage.Source = ImageSource.FromStream(() =>
                {
                    return file.GetStream();
                });
            }
            catch (Exception ex)
            {

            }
        }
    }
}