using ServPay.View_Models;

namespace ServPay.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
        BindingContext = new RegistrationViewModel();

		InitializeComponent();

    }
}