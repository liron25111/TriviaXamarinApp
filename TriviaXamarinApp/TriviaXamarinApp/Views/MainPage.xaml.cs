using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaXamarinApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TriviaXamarinApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            TriviaXamarinApp.ViewModels.MainPageViewModel mainPage = new ViewModels.MainPageViewModel();
            this.BindingContext = mainPage;
            mainPage.Push += (p) => Navigation.PushAsync(p);
        }
    }
}