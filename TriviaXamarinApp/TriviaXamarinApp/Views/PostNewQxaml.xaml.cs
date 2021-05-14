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
    public partial class PostNewQxaml : ContentPage
    {
        public PostNewQxaml()
        {
            InitializeComponent();
            BindingContext = new PostNewQuestion();
        }
        private void Submut_Q(object sender, EventArgs e)
        {
            Page p = new QuestionsPage();
            App.Current.MainPage.Navigation.PushAsync(p);
        }
    }
}