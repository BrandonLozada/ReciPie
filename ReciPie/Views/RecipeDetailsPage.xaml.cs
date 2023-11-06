using ReciPie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReciPie.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeDetailsPage : ContentPage
    {
        public RecipeDetailsPage(Recipie recipie)
        {
            InitializeComponent();

            Title.Text = recipie.Title;
            Description.Text = recipie.Description;
            Instructions.Text = recipie.Instructions;
            PreparationTime.Text = recipie.PreparationTime;
            CookingTempeture.Text = recipie.CookingTempeture;
            Ingredients.Text = recipie.Ingredients;
            Categories.Text = recipie.Categories;
        }
    }
}