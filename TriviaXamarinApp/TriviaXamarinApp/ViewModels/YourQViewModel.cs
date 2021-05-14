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
using System.Collections.ObjectModel;

namespace TriviaXamarinApp.ViewModels
{
    class YourQViewModel : INotifyPropertyChanged
    {
        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

   
        private string nickName;
        public string NickName
        {
            get { return this.nickName; }
            set
            {
                if (this.nickName != value)
                {
                    this.nickName = value;
                    OnPropertyChanged(nameof(NickName));
                }
            }
        }
        private string password;

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
        private string email;
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
        private string qText;
        public string QText
        {
            get { return this.qText; }
            set
            {
                if (this.qText != value)
                {
                    this.qText = value;
                    OnPropertyChanged(nameof(QText));
                }
            }
        }
        private string error;

        public string Error
        {
            get => error;
            set
            {
                if (value != error)
                {
                    error = value;
                    OnPropertyChanged();//maybe need nameof
                }
            }
        }

        public YourQViewModel()
        {
            Error = string.Empty;
            Questions = new ObservableCollection<AmericanQuestion>();
            GetQuestions();
            foreach (AmericanQuestion question in ((App)App.Current).CurrentUser.Questions)
            {
                Questions.Add(question);
            }
            Email = ((App)App.Current).CurrentUser.Email;
            Password = ((App)App.Current).CurrentUser.Password;
            NickName = ((App)App.Current).CurrentUser.NickName;

            DeleteQuestionCommand = new Command<AmericanQuestion>(Delete);
        }
        public ObservableCollection<AmericanQuestion> Questions { get; set; }
        public ICommand DeleteQuestionCommand { get; set; }

        private async void GetQuestions()
        {
            try
            {
                TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
                ((App)App.Current).CurrentUser = await proxy.LoginAsync(((App)App.Current).CurrentUser.Email, ((App)App.Current).CurrentUser.Password);
            }
            catch (Exception)
            {
                Error = "Something Went Wrong...";
            }
        }
        private async void Delete(AmericanQuestion question)
        {

            try
            {
                TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
                bool b = await proxy.DeleteQuestion(question);
                if (!b)
                    Error = "Something Went Wrong...";
                else
                {
                    Questions.Remove(question);
                    ((App)App.Current).CurrentUser = await proxy.LoginAsync(((App)App.Current).CurrentUser.Email, ((App)App.Current).CurrentUser.Password);
                }
            }
            catch (Exception)
            {
                Error = "Something Went Wrong...";
            }
        }
        //public ICommand EditCommand => new Command<AmericanQuestion>(EditQuestion);

        public Func<Page, Task> Push { get; set; }

        //public async void EditQuestion(AmericanQuestion americanQ)
        //{
        //    //TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
        //    //await proxy.DeleteQuestion(americanQ);
        //}
    }
}