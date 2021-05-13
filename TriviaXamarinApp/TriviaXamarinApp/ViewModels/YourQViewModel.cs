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
        public YourQViewModel()
        {
            Error = string.Empty;
            Questions = new ObservableCollection<AmericanQuestion>();
            foreach (AmericanQuestion question in ((App)App.Current).CurrentUser.Questions)
            {
                Questions.Add(question);
            }
            DeleteQuestionCommand = new Command<AmericanQuestion>(Delete);
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
        public ObservableCollection<AmericanQuestion> Questions { get; set; }
        public ICommand DeleteQuestionCommand { get; set; }
        public event Func<Page, Task> Push;

    }
}
