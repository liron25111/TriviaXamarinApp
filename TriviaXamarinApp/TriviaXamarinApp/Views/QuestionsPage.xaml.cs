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
    public partial class QuestionsPage : ContentPage
    {
        public QuestionsPage()
        {
            this.BindingContext = new QuestionsPageViewModel();
            this.Title = "Trivia Q";
            InitializeComponent();

        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            Page p = new LoginPage();
            App.Current.MainPage.Navigation.PushAsync(p);
        }
        private void Add_Clicked(object sender, EventArgs e)
        {
            Page p = new PostNewQxaml();
            App.Current.MainPage.Navigation.PushAsync(p);
        }
        private void Register_Clicked(object sender, EventArgs e)
        {
            Page p = new RegisterPage();
            App.Current.MainPage.Navigation.PushAsync(p);
        }

        private void User_Clicked(object sender, EventArgs e)
        {
            Page p = new ProfilePage();
            App.Current.MainPage.Navigation.PushAsync(p);
        }
        private void Edit_Clicked(object sender, EventArgs e)
        {
            Page p = new EditPage();
            App.Current.MainPage.Navigation.PushAsync(p);
        }
    }
}