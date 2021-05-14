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
    public partial class YourQ : ContentPage
    {
        public YourQ()
        {
            InitializeComponent();
            YourQViewModel yQVM = new YourQViewModel();
            BindingContext = yQVM;
            yQVM.Push += (p) => Navigation.PushAsync(p);
        }
        private void Login_Clicked(object sender, EventArgs e)
        {
            Page p = new LoginPage();
            App.Current.MainPage.Navigation.PushAsync(p);
        }
        private void Question_Clicked(object sender, EventArgs e)
        {
            //Page p = new YourQ();
            //App.Current.MainPage.Navigation.PushAsync(p);
        }
        private void Swipe(object sender, EventArgs e)
        {

        }

        private void Swipe1(object sender, EventArgs e)
        {

        }
    }
}