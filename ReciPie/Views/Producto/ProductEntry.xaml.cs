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
    public partial class ProductEntry : ContentPage
    {
        ProductRepository repository = new ProductRepository();
        public ProductEntry()
        {
            InitializeComponent();
        }

        private async void ButtonSave_Clicked(object sender, EventArgs e)
        {
            string name = TxtName.Text;
            string cantidad = TxtCantidad.Text;
            string marca = TxtMarca.Text;
            string descripcion = TxtDescripcion.Text;
            if (string.IsNullOrEmpty(name))
            {
                await DisplayAlert("Advertencia", "Por favor ingresa el Nombre del Producto", "Ok");
            }
            else if (string.IsNullOrEmpty(cantidad))
            {
                await DisplayAlert("Advertencia", "Por favor ingresa la Cantidad del Producto", "Ok");
            }
            else if (string.IsNullOrEmpty(marca))
            {
                await DisplayAlert("Advertencia", "Por favor ingresa la Marca del Producto", "Ok");
            }
            else if (string.IsNullOrEmpty(descripcion))
            {
                await DisplayAlert("Advertencia", "Por favor ingresa la Descripción del Producto", "Ok");
            }
            else
            {
                ProductoModel producto = new ProductoModel();
                producto.Nombre = name;
                producto.Cantidad = cantidad;
                producto.Marca = marca;
                producto.Descripcion = descripcion;

                var isSaved = await repository.Save(producto);
                if (isSaved)
                {
                    await DisplayAlert("Exito", "Producto añadido", "Ok");
                    Clear();
                }
                else
                {
                    await DisplayAlert("Error", "No se guardó el Producto", "Ok");
                }
            }
            

        }

        public void Clear()
        {
            TxtName.Text = string.Empty;
            TxtCantidad.Text = string.Empty;
            TxtMarca.Text = string.Empty;
            TxtDescripcion.Text = string.Empty;
        }
    }
}