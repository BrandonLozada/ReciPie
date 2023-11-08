using Firebase.Database;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ReciPie.Models;

namespace ReciPie
{
    public class RecipieRepository
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://reci-pie-default-rtdb.firebaseio.com/");
        FirebaseStorage firebaseStorage = new FirebaseStorage("reci-pie.appspot.com");

        public async Task<bool> AddRecipie(AddRecipie recipie)
        {
            var data = await firebaseClient.Child("recipes").PostAsync(JsonConvert.SerializeObject(recipie));
            
            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;
        }

        public async Task<List<Recipie>> GetAll()
        {
            return (await firebaseClient.Child("recipes").OnceAsync<Recipie>()).Select(item => new Recipie
            {
                Id = item.Key,
                Title = item.Object.Title,
                Description = item.Object.Description,
                ImageCover = item.Object.ImageCover,
                Instructions = item.Object.Instructions,
                PreparationTime = item.Object.PreparationTime,
                CookingTempeture = item.Object.CookingTempeture,
                Ingredients = item.Object.Ingredients,
                Categories = item.Object.Categories
            }).ToList();
        }

        public async Task<List<Recipie>> GetAllByName(string name)
        {
            return (await firebaseClient.Child("recipes").OnceAsync<Recipie>()).Select(item => new Recipie
            {
                Id = item.Key,
                Title = item.Object.Title,
                Description = item.Object.Description,
                ImageCover = item.Object.ImageCover,
                Instructions = item.Object.Instructions,
                PreparationTime = item.Object.PreparationTime,
                CookingTempeture = item.Object.CookingTempeture,
                Ingredients = item.Object.Ingredients,
                Categories = item.Object.Categories
            }).Where(c => c.Title.ToLower().Contains(name.ToLower())).ToList();
        }

        public async Task<Recipie> GetById(string id)
        {
            return (await firebaseClient.Child("recipes" + "/" + id).OnceSingleAsync<Recipie>());
        }

        public async Task<bool> Update(Recipie recipie)
        {
            await firebaseClient.Child("recipes" + "/" + recipie.Id).PutAsync(JsonConvert.SerializeObject(recipie));
            return true;
        }

        public async Task<bool> Delete(string id)
        {
            await firebaseClient.Child("recipes" + "/" + id).DeleteAsync();
            return true;
        }

        public async Task<string> Upload(Stream img, string fileName)
        {
            var image = await firebaseStorage.Child("recipe-covers").Child(fileName).PutAsync(img);
            return image;
        }
    }
}
