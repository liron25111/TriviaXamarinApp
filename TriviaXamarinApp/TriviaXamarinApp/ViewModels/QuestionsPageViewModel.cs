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
    class QuestionsPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }   
                    

        public QuestionsPageViewModel()
        {
            this.Score = 0;
            this.Enabled = true;
            this.OtherAnswers = new ObservableCollection<string>();
            GetQ();
            NextQCommand = new Command(NextQ);
            AddNewQCommand = new Command(AddQ);
            IsCorrectCommand = new Command<string>(IsCorrect);
        }

        private void IsCorrect(string answer)
        {
            if(answer == correctAnswer)
            {
                score++;
                enabled = false;
            }
            if(score%3==0)
            {
                CanAddQ = true;
            }    
        }
            public bool IsCorrectAnswer(string answer)
        {
            return answer == correctAnswer;
        }
        private void AddQ()
        {
            if(CanAddQ)
            {
                Push?.Invoke(new TriviaXamarinApp.Views.PostNewQxaml());
                CanAddQ = false;
            }
        }

        private  void NextQ()
        {
             GetQ();
            Enabled = true;
        }

        public async void GetQ()
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            try
            {
                AmericanQuestion q = await proxy.GetRandomQuestion();
                if (q != null)
                {
                    QuestionText = q.QText;
                    correctAnswer = q.CorrectAnswer;
                    OtherAnswers.Add(q.OtherAnswers[0]);
                    OtherAnswers.Add(q.OtherAnswers[1]);
                    OtherAnswers.Add(q.OtherAnswers[2]);
                    nickname = q.CreatorNickName;
                }
                else
                    Error = "Something went Wrong....";


            }
            catch
            {
                Error = "Something went Wrong....";
            }
        }
        private bool canAddQ;
        public bool CanAddQ
        {
            get
            {
                return this.canAddQ;
            }
            set
            {
                if(this.canAddQ!=value)
                {
                    this.canAddQ = value;
                    OnPropertyChanged(nameof(CanAddQ));
              }
            }
        }



        private int score;
        public int Score
        {
            get
            {
                return this.score;
            }
            set
            {
                if(this.score!=value)
                {
                    this.score = value;
                    OnPropertyChanged(nameof(Score));
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

        private bool enabled;
        public bool Enabled
        {
            get
            {
                return this.enabled;
            }
            set
            {
                if(this.enabled!= value)
                {
                    this.enabled = value;
                    OnPropertyChanged(nameof(Enabled));
                }
            }
        }
        private string questionText;
        public string QuestionText
        {
            get {
                return this.questionText;
            }
            set
            {
                if(this.questionText!=value)
                {
                    this.questionText = value;
                    OnPropertyChanged(nameof(QuestionText));
                }
            }
        }
        private string nickname;

        public string NickName
        {
            get
            {
                return this.nickname;
            }
            set
            {
                if (this.nickname != value)
                {
                    this.nickname = value;
                    OnPropertyChanged(nameof(NickName));
                }
            }
        }
        private string correctAnswer;
        public string CorrectAnswer
        {
            get
            {
                return this.correctAnswer;
            }
            set
            {
                if (this.correctAnswer != value)
                {
                    this.correctAnswer = value;
                    OnPropertyChanged(nameof(CorrectAnswer));
                }
            }
        }
        public ObservableCollection<string> OtherAnswers { get; set; }

       

        public ICommand NextQCommand { get; set; }
        public ICommand AddNewQCommand { get; set; }
        public ICommand IsCorrectCommand { get; set; }


        public event Action<Page> Push; 

    }
}

