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