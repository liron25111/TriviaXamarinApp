using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using TriviaXamarinApp.Views;
using Xamarin.Forms;
using System.Threading.Tasks;
using TriviaXamarinApp.Services;
using TriviaXamarinApp.Models;

namespace TriviaXamarinApp.ViewModels
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event Action<Page> Push;

        public ICommand logInCommand => new Command(LogIn);
           private  void LogIn()
        {
            Push?.Invoke(new TriviaXamarinApp.Views.LoginPage());
        }
        public ICommand RegisterCommand => new Command(Register);
        private void Register()
        {
            Push?.Invoke(new TriviaXamarinApp.Views.RegisterPage());
        }
        public ICommand BeagustCommand => new Command(BeAgust);
        private void BeAgust()
        {
            Push?.Invoke(new TriviaXamarinApp.Views.QuestionsPage());
        }
    }
}
