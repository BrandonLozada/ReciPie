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
using ReciPie.Models;
using Xamarin.Essentials;
using Firebase.Auth;
using Newtonsoft.Json;
using Xamarin.CommunityToolkit.Extensions;

namespace ReciPie.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class EditRecipePage : ContentPage
    {
        MediaFile file;
        RecipieRepository _RecipieRepository = new RecipieRepository();

        public EditRecipePage(Recipie recipie)
        {
            InitializeComponent();

            Id.Text = recipie.Id;
            Title.Text = recipie.Title;
            //ImageCover.Source = recipie.ImageCover;
            Description.Text = recipie.Description;
            Instructions.Text = recipie.Instructions;
            PreparationTime.Text = recipie.PreparationTime;
            CookingTempeture.Text = recipie.CookingTempeture;
            Ingredients.Text = recipie.Ingredients;
            Categories.Text = recipie.Categories;
        }

        private async void BtnEditRecipie_Clicked(object sender, EventArgs e)
        {
            string id = Id.Text;
            string title = Title.Text;
            //string imageCover = ImageCover.Source;
            string description = Description.Text;
            string instructions = Instructions.Text;
            string preparationTime = PreparationTime.Text;
            string cookingTempeture = CookingTempeture.Text;
            string ingredients = Ingredients.Text;
            string categories = Categories.Text;

            if (string.IsNullOrEmpty(title))
            {
                await DisplayAlert("Advertencia", "El título es necesario.", "Aceptar");
                return;
            }
            else if (string.IsNullOrEmpty(description))
            {
                await DisplayAlert("Advertencia", "La descripción es necesaria.", "Aceptar");
                return;
            }
            else if (string.IsNullOrEmpty(instructions))
            {
                await DisplayAlert("Advertencia", "Las instrucciones son necesarias.", "Aceptar");
                return;
            }
            else if (string.IsNullOrEmpty(preparationTime))
            {
                await DisplayAlert("Advertencia", "El tiempo de preparación es necesaria.", "Aceptar");
                return;
            }
            else if (string.IsNullOrEmpty(cookingTempeture))
            {
                await DisplayAlert("Advertencia", "La temperatura de preparación es necesaria.", "Aceptar");
                return;
            }
            else if (string.IsNullOrEmpty(ingredients))
            {
                await DisplayAlert("Advertencia", "Al menos un ingredientes es necesaris.", "Aceptar");
                return;
            }
            else if (string.IsNullOrEmpty(categories))
            {
                await DisplayAlert("Advertencia", "Al menos una categoria es necesaria.", "Aceptar");
                return;
            }
            
            try
            {
                isVisible.IsVisible = true;
                isLoading.IsRunning = true;

                var UserCredential = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get("UserCredential", ""));

                AddRecipie recipie = new AddRecipie();
                recipie.Title = title;
                recipie.ImageCover = 
                recipie.Description = description;
                recipie.Instructions = instructions;
                recipie.PreparationTime = preparationTime;
                recipie.CookingTempeture = cookingTempeture;
                recipie.Ingredients = ingredients;
                recipie.Categories = categories;
                recipie.UserId = UserCredential.User.LocalId;

                if (file != null)
                {
                    string imageCover = await _RecipieRepository.Upload(file.GetStream(), Path.GetFileName(file.Path));
                    recipie.ImageCover = imageCover;
                }

                bool isUpdated = await _RecipieRepository.Update(id, recipie);

                if (isUpdated)
                {
                    // await Navigation.PopAsync();
                    await Navigation.PushAsync(new MyRecipesPage());
                    await this.DisplayToastAsync("Receta editada correctamente.", 6000);
                }
                else
                {
                    await DisplayAlert("Error", "Hubo un error al editar la receta.", "Aceptar");
                }    
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Hubo un error al editar la receta.\n{ex.Message}", "Aceptar");
            }
            finally
            {
                isVisible.IsVisible = false;
                isLoading.IsRunning = false;
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

                ImageCover.Source = ImageSource.FromStream(() =>
                {
                    return file.GetStream();
                });
            }
            catch (Exception ex)
            {

            }
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