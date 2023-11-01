using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ReciPie.Views;
using ReciPie.Views.Student;

namespace ReciPie
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // TODO: Implementar la MainPage que tenemos en la anterior aplicación.
            MainPage = new NavigationPage(new SignInView());
           
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
