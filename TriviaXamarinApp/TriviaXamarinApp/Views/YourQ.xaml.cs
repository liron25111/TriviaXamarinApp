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
            YourQViewModel yqvm = new YourQViewModel();
            BindingContext = yqvm;
            yqvm.Push += (p) => Navigation.PushAsync(p);
        }
    }
}