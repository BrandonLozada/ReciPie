using Firebase.Database;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ReciPie
{
    public class ProductRepository
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://reci-pie-default-rtdb.firebaseio.com/");
        FirebaseStorage firebaseStorage = new FirebaseStorage("reci-pie.appspot.com");

        public async Task<bool> Save(ProductoModel product)
        {
            var data = await firebaseClient.Child(nameof(ProductoModel)).PostAsync(JsonConvert.SerializeObject(product));
            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;
        }

        public async Task<List<ProductoModel>> GetAll()
        {
            return (await firebaseClient.Child(nameof(ProductoModel)).OnceAsync<ProductoModel>()).Select(item => new ProductoModel
            {
                Nombre = item.Object.Nombre,
                Cantidad = item.Object.Cantidad,
                Image = item.Object.Image,
                Id = item.Key,
                Marca = item.Object.Marca,
                Descripcion = item.Object.Descripcion
            }).ToList();
        }

        public async Task<List<ProductoModel>> GetAllByName(string name)
        {
            return (await firebaseClient.Child(nameof(ProductoModel)).OnceAsync<ProductoModel>()).Select(item => new ProductoModel
            {
                Nombre = item.Object.Nombre,
                Cantidad = item.Object.Cantidad,
                Image = item.Object.Image,
                Id = item.Key,
                Marca = item.Object.Marca,
                Descripcion = item.Object.Descripcion
            }).Where(c => c.Nombre.ToLower().Contains(name.ToLower())).ToList();
        }

        private object ProductoModel()
        {
            throw new NotImplementedException();
        }

        public async Task<ProductoModel> GetById(string id)
        {
            return (await firebaseClient.Child(nameof(ProductoModel) + "/" + id).OnceSingleAsync<ProductoModel>());
        }

        public async Task<bool> Update(ProductoModel product)
        {
            await firebaseClient.Child(nameof(ProductoModel) + "/" + product.Id).PutAsync(JsonConvert.SerializeObject(product));
            return true;
        }

        public async Task<bool> Delete(string id)
        {
            await firebaseClient.Child(nameof(ProductoModel) + "/" + id).DeleteAsync();
            return true;
        }

        public async Task<string> Upload(Stream img, string fileName)
        {
            var image = await firebaseStorage.Child("Images").Child(fileName).PutAsync(img);
            return image;
        }
    }
}
