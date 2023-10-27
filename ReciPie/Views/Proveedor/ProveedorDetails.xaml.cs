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
    public partial class ProveedorDetails : ContentPage
    {

        public ProveedorDetails(ProveedorModel prov)
        {
            InitializeComponent();

            LabelName.Text = prov.Nombre;
            LabelDireccion.Text = prov.Direccion;
            LabelTelefono.Text = prov.Telefono;
            LabelGiro.Text = prov.Giro;
            LabelEmail.Text = prov.EmailProveedor;
        }
    }
}