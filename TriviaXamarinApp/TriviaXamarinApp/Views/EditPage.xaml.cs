using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaXamarinApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TriviaXamarinApp.Models;

namespace TriviaXamarinApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPage : ContentPage
    {
        public EditPage(AmericanQuestion q)
        {
            this.Title = "edit Page";
            InitializeComponent();
            EditPageViewModel Edp = new EditPageViewModel(q);
            BindingContext = Edp;
            Edp.Push += (p) => Navigation.PushAsync(p);
        }
    }
}