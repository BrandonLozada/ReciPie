using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReciPie.Views.Proveedor
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProveedorEntry : ContentPage
    {
        ProveedorRepository repository = new ProveedorRepository();

        public ProveedorEntry()
        {
            InitializeComponent();
        }

        private async void ButtonSave_Clicked(object sender, EventArgs e)
        {
            string name = TxtName.Text;
            string direccion = TxtDireccion.Text;
            string telProv = TxtTel.Text;
            string giroProv = TxtGiro.Text;
            string emailProv = TxtEmailProv.Text;

            if (string.IsNullOrEmpty(name))
            {
                await DisplayAlert("Advertencia", "Por favor ingresa el Nombre del Proveedor", "Ok");
            }
            else if (string.IsNullOrEmpty(direccion))
            {
                await DisplayAlert("Advertencia", "Por favor ingresa la Dirección del Proveedor", "Ok");
            }
            else if (string.IsNullOrEmpty(telProv))
            {
                await DisplayAlert("Advertencia", "Por favor ingresa el Teléfono del Proveedor", "Ok");
            }
            else if (string.IsNullOrEmpty(giroProv))
            {
                await DisplayAlert("Advertencia", "Por favor ingrese el Giro del Proveedor", "Ok");
            }
            else if (string.IsNullOrEmpty(emailProv))
            {
                await DisplayAlert("Advertencia", "Por favor ingrese el Email del Proveedor", "Ok");
            }
            else
            {
                ProveedorModel prov = new ProveedorModel();
                prov.Nombre = name;
                prov.Direccion = direccion;
                prov.Telefono = telProv;
                prov.Giro = giroProv;
                prov.EmailProveedor = emailProv;

                var isSaved = await repository.Save(prov);
                if (isSaved)
                {
                    await DisplayAlert("Exito", "Proveedor añadido", "Ok");
                    Clear();
                }
                else
                {
                    await DisplayAlert("Error", "No se guardó el Proveedor", "Ok");
                }
            }


        }

        public void Clear()
        {
            TxtName.Text = string.Empty;
            TxtTel.Text = string.Empty;
            TxtDireccion.Text = string.Empty;
            TxtGiro.Text = string.Empty;
            TxtEmailProv.Text = string.Empty;
        }

    }
}