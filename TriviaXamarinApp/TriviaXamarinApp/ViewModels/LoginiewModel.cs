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
    class LoginViewModel
    {
        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //משתנים-
        private string email;
        private string password;
        private string error;

        //events


        public string Error// get eror
        {
            get
            {
                return error;
            }
            set
            {
                if (error != value)
                {
                    error = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Email // get Email
        {
            get
            {
                return email;
            }
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Password // GetPassword
        {
            get
            {
                return password;
            }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand LoginCommand { get; set; }// login Command
        public ICommand BeAGustCommand { get; set; } // Continue As A gust
        public ICommand RegisterCommand { get; set; } // RegisterCommand
        public event Action<Page> Push;
        public LoginViewModel()
        {
            Error = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            BeAGustCommand = new Command(ContinueAsGust);
            LoginCommand = new Command(Login);
            RegisterCommand = new Command(Register);


        }
        // פעולות
        private void Register()
        {
            Push?.Invoke(new TriviaXamarinApp.Views.RegisterPage());
        }
        private void ContinueAsGust()
        {
            Push?.Invoke(new TriviaXamarinApp.Views.QuestionsPage());

        }
        private async void Login()
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();

            try
            {
                User u = await proxy.LoginAsync(Email, Password);
                if (u != null)
                {
                    ((App)App.Current).CurrentUser = u;
                    Push?.Invoke(new TriviaXamarinApp.Views.QuestionsPage());
                }
            }
            catch (Exception)
            {
                Error = "Something went Wrong";
            }
        }
    }
}