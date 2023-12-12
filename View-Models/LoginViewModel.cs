using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

//using Firebase.Database;
//using Firebase.Database.Query;
using ServPay.Services;
using ServPay.Views;
using System.Windows.Input;
using ServPay.Models;

#pragma warning disable MVVMTK0034

namespace ServPay.View_Models
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty] private string _email;

        [ObservableProperty] private string _imageSource;

        [ObservableProperty] private string _password;

        [ObservableProperty] private bool _isPasswordHiden;

        private readonly LoginServices _loginService;

        public ICommand LoginCommand { get; }
        public ICommand TogglePasswordCommand { get; }
        public ICommand SignInCommand { get; }

        public LoginViewModel()
        {
            _loginService = new LoginServices();
            SignInCommand = new Command(OnSignIn);
            LoginCommand = new Command(OnLoginAsync);
            TogglePasswordCommand = new Command(OnTogglePassword);
            IsPasswordHiden = true;
            ImageSource = "noevilmonkey94.png";
        }

        private static async void OnSignIn()
        {
            await Shell.Current.GoToAsync(nameof(RegisterPage), true);
        }

        private async void OnLoginAsync()
        {
            try
            {
                if (!_loginService.IsUserExist(Email, Password))
                    throw new ArgumentException("Incorrect email or password");
                var cards = _loginService.GetCardsByUserId(_loginService.GetUserId(Email));
                var user = _loginService.GetUserById(_loginService.GetUserId(Email));
                var transactionHistory = _loginService.GetTransactionsByUserId(user.Id);
                //var transactionHistory = new ObservableCollection<TransactionDetails>();
                var navigationParameter = new Dictionary<string, object>
                {
                    {"_email", Email},
                    {"_cards", cards},
                    {"_user", user},
                    {"_transactionHistory", transactionHistory}

                };
                await Shell.Current.GoToAsync($"{nameof(AccountPage)}", true, navigationParameter);
                
            }
            catch (Exception ex)
            {
                await Application.Current!.MainPage!.DisplayAlert("Error", ex.Message, "OK");
            }
            Email = "";
            Password = "";
        }

        private void OnTogglePassword()
        {
            IsPasswordHiden = !IsPasswordHiden;
            ImageSource = IsPasswordHiden
                ? "noevilmonkey94.png"
                : "monkeyface94.png";
        }
    }
}