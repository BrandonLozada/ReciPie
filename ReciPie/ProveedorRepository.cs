using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;
using Firebase.Storage;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace ReciPie
{
    public class ProveedorRepository
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://reci-pie-default-rtdb.firebaseio.com/");
        FirebaseStorage firebaseStorage = new FirebaseStorage("reci-pie.appspot.com");
        public async Task<bool> Save(ProveedorModel prov)
        {
            var data = await firebaseClient.Child(nameof(ProveedorModel)).PostAsync(JsonConvert.SerializeObject(prov));
            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;
        }

        public async Task<List<ProveedorModel>> GetAll()
        {
            return (await firebaseClient.Child(nameof(ProveedorModel)).OnceAsync<ProveedorModel>()).Select(item => new ProveedorModel
            {
                Nombre = item.Object.Nombre,
                Direccion = item.Object.Direccion,
                Image = item.Object.Image,
                Id = item.Key,
                Telefono = item.Object.Telefono,
                Giro = item.Object.Giro,
                EmailProveedor = item.Object.EmailProveedor
            }).ToList();
        }

        public async Task<List<ProveedorModel>> GetAllByName(string name)
        {
            return (await firebaseClient.Child(nameof(ProveedorModel)).OnceAsync<ProveedorModel>()).Select(item => new ProveedorModel
            {
                Nombre = item.Object.Nombre,
                Direccion = item.Object.Direccion,
                Image = item.Object.Image,
                Id = item.Key,
                Telefono = item.Object.Telefono,
                Giro = item.Object.Giro,
                EmailProveedor = item.Object.EmailProveedor
            }).Where(c => c.Nombre.ToLower().Contains(name.ToLower())).ToList();
        }

        private object ProveedorModel()
        {
            throw new NotImplementedException();
        }

        public async Task<ProveedorModel> GetById(string id)
        {
            return (await firebaseClient.Child(nameof(ProveedorModel) + "/" + id).OnceSingleAsync<ProveedorModel>());
        }

        public async Task<bool> Update(ProveedorModel product)
        {
            await firebaseClient.Child(nameof(ProveedorModel) + "/" + product.Id).PutAsync(JsonConvert.SerializeObject(product));
            return true;
        }

        public async Task<bool> Delete(string id)
        {
            await firebaseClient.Child(nameof(ProveedorModel) + "/" + id).DeleteAsync();
            return true;
        }

        public async Task<string> Upload(Stream img, string fileName)
        {
            var image = await firebaseStorage.Child("Images").Child(fileName).PutAsync(img);
            return image;
        }

    }
}
