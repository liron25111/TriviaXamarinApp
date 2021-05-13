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
        this.CorrectAnswer = string.Empty;
        this.QText = string.Empty;
        this.WrongAnswers = new string[NUM_WRONG];
        for (int i = 0; i < this.WrongAnswers.Length; i++)
        {
            this.WrongAnswers[i] = string.Empty;
        }

        AddtQuestionCommand = new Command(AddQ);
    }

    private async void AddQ()
    {
        TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
        try
        {
            AmericanQuestion question = new AmericanQuestion
            {
                CorrectAnswer = this.CorrectAnswer,
                QText = this.QText,
                CreatorNickName = ((App)App.Current).CurrentUser.NickName,
                OtherAnswers = new string[NUM_WRONG],
            };
            for (int i = 0; i < this.WrongAnswers.Length; i++)
            {
                question.OtherAnswers[i] = this.WrongAnswers[i];
            }

            bool b = await proxy.PostNewQuestion(question);
            if (b)
            {
                Error = " your question was added successfully";
            }
            else
            {
                Error = "Something Went Wrong... Please try again";
            }


        }
        catch (Exception)
        {
            Error = "Something Went Wrong...";
        }
    }



    private const int NUM_WRONG = 3;
    private string qText;
    public string QText
    {
        get => this.qText;
        set
        {
            if (value != qText)
            {
                qText = value;
                OnPropertyChanged();
            }
        }
    }


    private string correctAnswer;

    public string CorrectAnswer
    {
        get => this.correctAnswer;
        set
        {
            if (value != correctAnswer)
            {
                correctAnswer = value;
                OnPropertyChanged();
            }
        }
    }


    private string[] wrongAnswers;

    public string[] WrongAnswers
    {
        get => this.wrongAnswers;
        set
        {
            if (value != wrongAnswers)
            {
                wrongAnswers = value;
                OnPropertyChanged();
            }
        }
    }


    private string error;

    public string Error
    {
        get => this.error;
        set
        {
            if (value != error)
            {
                error = value;
                OnPropertyChanged();
            }
        }
    }

    public ICommand AddtQuestionCommand { get; set; }
    public event Action<Page> Push;

}
}
