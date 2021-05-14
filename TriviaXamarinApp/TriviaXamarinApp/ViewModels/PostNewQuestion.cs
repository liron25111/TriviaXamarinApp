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
    class PostNewQuestion : INotifyPropertyChanged
    {
        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public PostNewQuestion()
        {

            QuestionText = string.Empty;
            CorrectAnswer = string.Empty;
            Error = string.Empty;
            WrongAnswers = new ObservableCollection<string>();
            WrongAnswers.Add(string.Empty);
            WrongAnswers.Add(string.Empty);
            WrongAnswers.Add(string.Empty);
            SubmitQCommand = new Command(SubmitQ);
        }

        private async void SubmitQ()
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
           
            try
            {
                AmericanQuestion newQ = new AmericanQuestion
                {
                    QText = this.QuestionText,
                    CorrectAnswer = this.CorrectAnswer,
                    CreatorNickName = ((App)App.Current).CurrentUser.NickName,
                    OtherAnswers = new string[]
                    {
                        WrongAnswers[0],
                        WrongAnswers[1],
                        WrongAnswers[2]
                    }
                };

                bool b = await proxy.PostNewQuestion(newQ);
                if (b)
                    Error = " Your Question Added Successfully";
                else
                    Error = "Something Went Wrong...";

            }
            catch (Exception)
            {
                Error = "Something Went Wrong...";
            }
        }


        private string questionText;

        public string QuestionText
        {
            get => questionText;
            set
            {
                if (value != questionText)
                {
                    questionText = value;
                    OnPropertyChanged();
                }
            }
        }

        private string correctAnswer;

        public string CorrectAnswer
        {
            get => correctAnswer;
            set
            {
                if (value != correctAnswer)
                {
                    correctAnswer = value;
                    OnPropertyChanged();
                }
            }
        }


        public ObservableCollection<string> WrongAnswers { get; set; }

        private string error;

        public string Error
        {
            get => error;
            set
            {
                if (value != error)
                {
                    error = value;
                    OnPropertyChanged();
                }
            }
        }


        public ICommand SubmitQCommand { get; set; }




    }
}