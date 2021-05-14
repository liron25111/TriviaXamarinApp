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
        public ObservableCollection<AmericanQuestion> QuestionList { get; set; }

        public YourQViewModel()
        {
            QuestionList = new ObservableCollection<AmericanQuestion>();
            CreateQuestionCollection();
            NickName = ((App)App.Current).CurrentUser.NickName;
            Email = ((App)App.Current).CurrentUser.Email;
            Password = ((App)App.Current).CurrentUser.Password;
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
        private string correctAnswer;
        public string CorrectAnswer
        {
            get { return this.correctAnswer; }
            set
            {
                if (this.correctAnswer != value)
                {
                    this.correctAnswer = value;
                    OnPropertyChanged(nameof(CorrectAnswer));
                }
            }
        }
        private void CreateQuestionCollection()
        {
            App a = (App)App.Current;
            List<AmericanQuestion> theQuestions = a.CurrentUser.Questions;
            foreach (AmericanQuestion q in theQuestions)
            {
                this.QuestionList.Add(q);
            }

        }
        public ICommand DeleteCommand => new Command<AmericanQuestion>(RemoveQuestion);

        public async void RemoveQuestion(AmericanQuestion americanQ)
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            await proxy.DeleteQuestion(americanQ);
        }
        public ICommand EditCommand => new Command<AmericanQuestion>(EditQuestion);

        public Func<Page, Task> Push { get; set; }

        public async void EditQuestion(AmericanQuestion americanQ)
        {
            //TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            //await proxy.DeleteQuestion(americanQ);
        }
    }
}