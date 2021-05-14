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
        public EditPageViewModel(AmericanQuestion question)
        {
            questionBefore = question;
            QTextAfter = question.QText;
            CorrectAnswerAfter = question.CorrectAnswer;
            OtherAnswersAfterChange = new ObservableCollection<string>();
            foreach (string wrongAnswer in question.OtherAnswers)
            {
                OtherAnswersAfterChange.Add(wrongAnswer);
            }

            SubmitQuestionCommand = new Command(SubmitQuestion);
        }

        private async void SubmitQuestion()
        {
            try
            {
                TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
                if (!await proxy.DeleteQuestion(questionBefore))
                {
                    Error = "Something Went Wrong...";
                }

            }
            catch (Exception)
            {
                Error = "Something Went Wrong...";
            }

            if (string.IsNullOrEmpty(Error))
            {
                AmericanQuestion newQuestion = new AmericanQuestion
                {
                    QText = this.QTextAfter,
                    CorrectAnswer = this.CorrectAnswerAfter,
                    CreatorNickName = ((App)App.Current).CurrentUser.NickName,
                    OtherAnswers = new string[]
                    {
                        OtherAnswersAfterChange[0],
                        OtherAnswersAfterChange[1],
                        OtherAnswersAfterChange[2]
                    }
                };

                try
                {
                    TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
                    if (!await proxy.PostNewQuestion(newQuestion))
                    {
                        Error = "Something Went Wrong...";
                    }
                    else
                    {
                        Error = "Your Edit Completed!";
                    }

                    ((App)App.Current).CurrentUser = await proxy.LoginAsync(((App)App.Current).CurrentUser.Email, ((App)App.Current).CurrentUser.Password);
                    Pop?.Invoke();
                }
                catch (Exception)
                {
                    Error = "Something Went Wrong..";
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


        private const int OTHER_ANSWERS_NUM = 3;

        private AmericanQuestion questionBefore;


        private string qTextAfter;

        public string QTextAfter
        {
            get => qTextAfter;
            set
            {
                if (value != qTextAfter)
                {
                    qTextAfter = value;
                    OnPropertyChanged();
                }
            }
        }

        private string correctAnswerAfter;

        public string CorrectAnswerAfter
        {
            get => correctAnswerAfter;
            set
            {
                if (value != correctAnswerAfter)
                {
                    correctAnswerAfter = value;
                    OnPropertyChanged();
                }
            }
        }


        public ObservableCollection<string> OtherAnswersAfterChange { get; set; }


        public ICommand SubmitQuestionCommand { get; set; }



        public event Action Pop;



    }
}