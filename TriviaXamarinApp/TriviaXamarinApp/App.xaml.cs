using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TriviaXamarinApp.Services;
using TriviaXamarinApp.Models;
using System.Threading.Tasks;

namespace TriviaXamarinApp
{
    public partial class App : Application
    {
        public User CurrentUser { get; set;  }
        public App()
        {
            InitializeComponent();
            CurrentUser = null;
            MainPage = new NavigationPage(new TriviaXamarinApp.Views.MainPage());
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
