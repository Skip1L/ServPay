using ServPay.View_Models;

namespace ServPay.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
    {
        Title = "LoginPage";

        BindingContext = new LoginViewModel();

		InitializeComponent();
    }
}