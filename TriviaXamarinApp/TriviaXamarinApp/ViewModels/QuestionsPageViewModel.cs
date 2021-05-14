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
using System.Linq;

namespace TriviaXamarinApp.ViewModels
{
    public class AnswerViewModel :INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string Answer { get; set; }
        public Color color;
        public Color Color
        {
            get { return this.color; }
            set
            {
                if (this.color != value)
                    OnPropertyChanged(nameof(Color));
            }
        }
    }
    class QuestionsPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string qText;
        public string QText
        {
            get { return this.qText; }
            set
            {
                if(this.qText != value)
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
        private string[] answers;
        public string[] Answers
        {
            get { return this.answers; }
            set
            {
                if (this.answers != value)
                {
                    this.answers = value;
                    OnPropertyChanged(nameof(Answers));
                }
            }
        }
  
        private string nickName;
        public string NickName
        {
            get { return this.nickName; }
            set
            {
                if(this.nickName != value)
                {
                    this.nickName = value;
                    OnPropertyChanged(nameof(NickName));
                }    
            }
        }
        private string message;
        public string Message
        {
            get { return this.message; }
            set
            {
                if (this.message != value)
                {
                    this.message = value;
                    OnPropertyChanged(nameof(Message));
                }
            }
        }


        public AmericanQuestion AQ { get; set; }
        public ObservableCollection<AnswerViewModel> AnswersList { get; set; }
        public bool clicked;
        public bool Clicked
        {
            get { return this.clicked; }
            set
            {
                if(this.clicked!= value)
                {
                    this.clicked = value;
                    OnPropertyChanged(nameof(Clicked));
                }
            }
        }
        private bool enabeld;
        public bool Enabeld
        {
            get { return this.enabeld; }
            set
            {
                if (this.enabeld != value)
                {
                    this.enabeld = value;
                    OnPropertyChanged(nameof(Enabeld));
                }
            }
        }
        private int count;
        public int Count
        {
            get { return this.count; }
            set
            {
                if (this.count != value)
                {
                    this.count = value;
                    OnPropertyChanged(nameof(Count));
                }
            }
        }
        public ICommand UserCommand { get; set; } 

        public QuestionsPageViewModel()
        {
            Clicked = false;
            Message = "";
            AnswersList = new ObservableCollection<AnswerViewModel>();
            AQ = new AmericanQuestion();
            CreateQuestion();
        }
        private async void CreateQuestion()
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();

            AmericanQuestion q = await proxy.GetRandomQuestion();

            this.CorrectAnswer = q.CorrectAnswer;
            this.NickName = q.CreatorNickName;
            this.QText = q.QText;
            Answers = new string[4];

            for (int i = 0; i < 3; i++)
            {
                Answers[i] = q.OtherAnswers[i];
            }
            Answers[3] = q.CorrectAnswer;

            Random r = new Random();
            Answers = Answers.OrderBy(x => r.Next()).ToArray();
        }
        public ICommand checkCommand => new Command<string>(Answer);


        public void Answer(string s)
        {
           
                if (s == CorrectAnswer)
                {

                    Count++;
                }
               if( count!=0&&Count %3==0)
                {
                App a = (App)App.Current;
                if(a.CurrentUser!= null)
                {
                    App.Current.MainPage.Navigation.PushAsync(new TriviaXamarinApp.Views.PostNewQxaml());
                }

                }
           
            CreateQuestion();
        }
        
 }



}
        