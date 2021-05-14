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
            App a = (App)App.Current;
            if (a.CurrentUser!=null)
            {
                App.Current.MainPage.Navigation.PushAsync(new TriviaXamarinApp.Views.YourQ());
            }
            else
            {
                Page p = new LoginPage();
                App.Current.MainPage.Navigation.PushAsync(p);
            }
          
        }
        private void Your_Questions(object sender, EventArgs e)
        {
            Page p = new YourQ();
            App.Current.MainPage.Navigation.PushAsync(p);
        }
    }
}