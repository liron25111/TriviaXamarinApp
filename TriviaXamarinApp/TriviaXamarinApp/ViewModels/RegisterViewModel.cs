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
    class RegisterViewModel
    {
        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //משתנים
        private string email;
        private string password;
        private string nickName;
        private string error;

        public event Action<Page> Push;

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

        public string NickName // get Nickname
        {
            get
            {
                return nickName;
            }
            set
            {
                if (nickName != value)
                {
                    nickName = value;
                    OnPropertyChanged();
                }
            }
        }

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
        public ICommand RegisterCommand { get; set; } // RegisterCommand
        public RegisterViewModel()
        {
            Email = string.Empty;
            Password = string.Empty;
            NickName = string.Empty;
            RegisterCommand = new Command(Register);
        }
        private async void Register()
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            try
            {
                User u = new User() { Email = email, NickName = nickName, Password = password };
                bool t = await proxy.RegisterUser(u);
                if (t)
                {
                    ((App)App.Current).CurrentUser = u;
                    Push?.Invoke(new TriviaXamarinApp.Views.LoginPage());
                }
            }
            catch (Exception)
            {
                Error = "Something went Wrong";
            }
        }
    }
}
