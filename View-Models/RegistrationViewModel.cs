using CommunityToolkit.Mvvm.ComponentModel;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using ServPay.Models;
using ServPay.Services;
using System.Windows.Input;

namespace ServPay.View_Models;

public partial class RegistrationViewModel : ObservableObject
{
    [ObservableProperty] private string _username;

    [ObservableProperty] private string _email;

    [ObservableProperty] private string _password;

    [ObservableProperty] private string _confirmPassword;

    public ICommand RegisterCommand { get; private set; }

    public RegistrationViewModel()
    {
        RegisterCommand = new Command(Register);
    }

    private async void Register()
    {
        try
        {
            if (Password != ConfirmPassword) throw new ArgumentException("Not same pass");
            var loginServices = new LoginServices();
            if (loginServices.IsUserExist(Email)) throw new ArgumentException("User with this email already exist");
            FirebaseClient firebaseClient =
                new("https://servpay-1a2b5-default-rtdb.europe-west1.firebasedatabase.app/");
            var newUser = new User
            {
                Password = Password,
                Email = Email,
                Username = Username
            };
            await firebaseClient.Child("Users").Child(loginServices.GetUserCount().ToString)
                .PutAsync(JsonConvert.SerializeObject(newUser));
            await Application.Current!.MainPage!.DisplayAlert("Success", "Registration successful", "OK");
            loginServices.Refresh();
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("Error", ex.Message, "OK");
        }
    }
}