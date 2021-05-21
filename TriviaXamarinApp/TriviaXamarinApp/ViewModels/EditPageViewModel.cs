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
    class EditPageViewModel
    {
        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public EditPageViewModel(AmericanQuestion q)
        {

            QuestionText = q.QText;
            CorrectAnswer = q.CorrectAnswer;
            Error = string.Empty;
            WrongAnswers = new ObservableCollection<string>();
            WrongAnswers.Add(q.OtherAnswers[0]);
            WrongAnswers.Add(q.OtherAnswers[1]);
            WrongAnswers.Add(q.OtherAnswers[2]);
            question = q;
        }
        public ICommand SubmitQCommand => new Command(SubmitQ);


        private AmericanQuestion question;
        private async void SubmitQ()
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            if (WrongAnswers[0] == "" || WrongAnswers[1] == "" || WrongAnswers[2] == "" || CorrectAnswer == "")
            {
                Error = "Something Went Wrong...";
            }
            else
            {
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
                    {

                        ((App)App.Current).CurrentUser.Questions.Add(newQ);
                        proxy.DeleteQuestion(question);
                        await App.Current.MainPage.Navigation.PushAsync(new TriviaXamarinApp.Views.QuestionsPage());
                    }
                    else
                        Error = "Something Went Wrong...";


                }
                catch (Exception)
                {
                    Error = "Something Went Wrong...";
                }
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



        public event Action<Page> Push;



    }
}