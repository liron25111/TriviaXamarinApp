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
        public MainPageViewModel()
        {
          
                RegisterCommand = new Command(Register);
                LoginCommand = new Command(LogIn);
                LogoutCommand = new Command(LogOut);
                BeAgustCommand = new Command(BeAgust);
            
        }
        private void LogIn()
        {
            Push?.Invoke(new TriviaXamarinApp.Views.LoginPage());
        }
        private void LogOut()
        {
            ((App)App.Current).CurrentUser = null;
        }
        private void Register()
        {
            Push?.Invoke(new TriviaXamarinApp.Views.RegisterPage());
        }
        private void BeAgust()
        {
            Push?.Invoke(new TriviaXamarinApp.Views.QuestionsPage());
        }
        public ICommand PlayCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand BeAgustCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
      
    }
}
