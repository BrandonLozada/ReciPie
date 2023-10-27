using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReciPie.Views.Producto
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetails : ContentPage
    {
        public ProductDetails(ProductoModel producto)
        {
            InitializeComponent();

            LabelName.Text = producto.Nombre;
            LabelCantidad.Text = producto.Cantidad;
            LabelMarca.Text = producto.Marca;
            LabelDescripcion.Text = producto.Descripcion;
        }
    }
}