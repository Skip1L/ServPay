using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Layouts;
using static System.Net.WebRequestMethods;

namespace ServPay.View_Models
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string username;
        private string imageSource;
        private string password;
        private bool isPasswordHiden;

        public string Username
        {
            get { return username; }
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Image
        {
            get { return imageSource; }
            set
            {
                if (imageSource != value)
                {
                    imageSource = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsPasswordHiden
        {
            get { return isPasswordHiden; }
            set
            {
                if (isPasswordHiden != value)
                {
                    isPasswordHiden = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand LoginCommand { get; private set; }
        public ICommand TogglePasswordCommand { get; private set; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLogin);
            TogglePasswordCommand = new Command(OnTogglePassword);
            isPasswordHiden = true;
            imageSource = "noevilmonkey94.png";
        }

        private void OnLogin()
        {
            // Handle login logic here
        }

        private void OnTogglePassword()
        {
            IsPasswordHiden = !IsPasswordHiden;
            Image= IsPasswordHiden
                ? "noevilmonkey94.png"
                : "monkeyface94.png";

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task LoginAsync()
        {
            // Perform login logic here
            // If login is successful, navigate to the next page
            //await Application.Current.MainPage.Navigation.PushAsync();
        }
    }
}
