﻿using ReciPie.Models;
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
    public partial class MyProfilePage : ContentPage
    {
        public MyProfilePage()
        {
            InitializeComponent();
        }

        private async void GoToEditProfilePage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditProfilePage());
        }
    }
}