using ServPay.View_Models;

namespace ServPay.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
    {

        BindingContext = new LoginViewModel();

		InitializeComponent();
    }
}