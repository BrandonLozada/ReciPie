﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ReciPie.Models;
using ReciPie.Repositories;

namespace ReciPie.Views.Receta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecetaEntry : ContentPage
    {
        RecipieRepository _RecipieRepository = new RecipieRepository();

        public RecetaEntry()
        {
            InitializeComponent();
        }

        private async void BtnAddRecipie_Clicked(object sender, EventArgs e)
        {
            // TODO: Agregar un Try - Catch en la petición.
            isVisible.IsVisible = true;
            isLoading.IsRunning = true;

            string title = Title.Text;
            string description = Description.Text;
            string instructions = Instructions.Text;
            string preparationTime = PreparationTime.Text;
            string cookingTempeture = CookingTempeture.Text;
            string ingredients = Ingredients.Text;
            string categories = Categories.Text;

            if (string.IsNullOrEmpty(title))
            {
                await DisplayAlert("Advertencia", "El título es necesario.", "Aceptar");
            }
            else if (string.IsNullOrEmpty(description))
            {
                await DisplayAlert("Advertencia", "La descripción es necesaria.", "Aceptar");
            }
            else if (string.IsNullOrEmpty(instructions))
            {
                await DisplayAlert("Advertencia", "Las instrucciones son necesarias.", "Aceptar");
            }
            else if (string.IsNullOrEmpty(preparationTime))
            {
                await DisplayAlert("Advertencia", "El tiempo de preparación es necesaria.", "Aceptar");
            }
            else if (string.IsNullOrEmpty(cookingTempeture))
            {
                await DisplayAlert("Advertencia", "La temperatura de preparación es necesaria.", "Aceptar");
            }
            else if (string.IsNullOrEmpty(ingredients))
            {
                await DisplayAlert("Advertencia", "Al menos un ingredientes es necesaris.", "Aceptar");
            }
            else if (string.IsNullOrEmpty(categories))
            {
                await DisplayAlert("Advertencia", "Al menos una categoria es necesaria.", "Aceptar");
            }
            else
            {
                AddRecipie recipie = new AddRecipie();
                recipie.Title = title;
                recipie.Description = description;
                recipie.Instructions = instructions;
                recipie.PreparationTime = preparationTime;
                recipie.CookingTempeture = cookingTempeture;
                recipie.Ingredients = ingredients;
                recipie.Categories = categories;

                var isSaved = await _RecipieRepository.AddRecipie(recipie);
                
                if (isSaved)
                {
                    await DisplayAlert("Información", "Receta agregada correctamente.", "Aceptar");
                    Clear();
                }
                else
                {
                    await DisplayAlert("Error", "Hubo un error al guardar la receta.", "Aceptar");
                }
            }
            isVisible.IsVisible = true;
            isLoading.IsRunning = true;
        }

        public void Clear()
        {
            Title.Text = string.Empty;
            Description.Text = string.Empty;
            Instructions.Text = string.Empty;
            PreparationTime.Text = string.Empty;
            CookingTempeture.Text = string.Empty;
            Ingredients.Text = string.Empty;
            Categories.Text = string.Empty;
        }
    }
}