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
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            this.Title = "Register Page";

            InitializeComponent();
            RegisterViewModel Rgm = new RegisterViewModel();
            BindingContext = Rgm;
            Rgm.Push += (p) => Navigation.PushAsync(p);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Page p = new LoginPage();
            App.Current.MainPage.Navigation.PushAsync(p);
        }
    }
}