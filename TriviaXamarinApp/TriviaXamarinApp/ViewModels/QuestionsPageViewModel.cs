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
        public string aText;
        public string AText
        {
            get { return this.aText;}
            set
            {
                if(this.aText!=value)
                {
                    this.aText = value;
                    OnPropertyChanged(nameof(AText));
                }
            }
        }
  
        private string message;
        public string Message
        {
            get { return this.message; }
            set
            {
                if(this.message !=value)
                {
                    this.message = value;
                    OnPropertyChanged(nameof(Message));
                }    
            }
        }
     
        public AmericanQuestion AQ { get; set; }
        private bool pressed = false;
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
        public bool enabeld;
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
        public static int count = 0;
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
            this.AQ = await proxy.GetRandomQuestion();
            Random r = new Random();
            string[] arr = new string[4];
            if(AQ!= null)
            {
                arr[r.Next(0, 4)] = AQ.CorrectAnswer;
                int counter = 0;
                for (int i = 0; i < 4; i++)
                {
                    if(arr[i]==null)
                    {
                        arr[i] = AQ.OtherAnswers[counter]; counter++;
                    }
                }
                foreach(string s in arr)
                {
                    this.AnswersList.Add(new AnswerViewModel
                    {
                        Answer = s,
                        Color = Color.Black
                    });
                    }
                }
            AText = AQ.QText;
            }
        public ICommand ChekCommand => new Command<AnswerViewModel>(Answer);

        public void Answer(AnswerViewModel s)
        {
            if(!pressed)
            {   
                if (s.Answer == AQ.CorrectAnswer)
                {
                    s.Color = Color.Green;
                    count++;
                }
                else
                {
                    s.Color = Color.Red;
                }
                pressed = true;
                if(count!=0&&count%3==0)
                {
                    clicked = true;
                }
            }
        }
        
       


    }



}
        