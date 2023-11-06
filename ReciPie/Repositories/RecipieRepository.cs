﻿using Firebase.Database;
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
                //Image = item.Object.Image,
                Instructions = item.Object.Instructions,
                PreparationTime = item.Object.PreparationTime,
                CookingTempeture = item.Object.CookingTempeture,
                Ingredients = item.Object.Ingredients,
                Categories = item.Object.Categories
            }).ToList();
        }

        //public async Task<List<ProductoModel>> GetAllByName(string name)
        //{
        //    return (await firebaseClient.Child(nameof(ProductoModel)).OnceAsync<ProductoModel>()).Select(item => new ProductoModel
        //    {
        //        Nombre = item.Object.Nombre,
        //        Cantidad = item.Object.Cantidad,
        //        Image = item.Object.Image,
        //        Id = item.Key,
        //        Marca = item.Object.Marca,
        //        Descripcion = item.Object.Descripcion
        //    }).Where(c => c.Nombre.ToLower().Contains(name.ToLower())).ToList();
        //}

        //private object ProductoModel()
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<ProductoModel> GetById(string id)
        //{
        //    return (await firebaseClient.Child(nameof(ProductoModel) + "/" + id).OnceSingleAsync<ProductoModel>());
        //}

        //public async Task<bool> Update(ProductoModel product)
        //{
        //    await firebaseClient.Child(nameof(ProductoModel) + "/" + product.Id).PutAsync(JsonConvert.SerializeObject(product));
        //    return true;
        //}

        //public async Task<bool> Delete(string id)
        //{
        //    await firebaseClient.Child(nameof(ProductoModel) + "/" + id).DeleteAsync();
        //    return true;
        //}

        //public async Task<string> Upload(Stream img, string fileName)
        //{
        //    var image = await firebaseStorage.Child("Images").Child(fileName).PutAsync(img);
        //    return image;
        //}
    }
}