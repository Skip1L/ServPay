using System.Collections.ObjectModel;
using ServPay.Models;
using ServPay.View_Models;

namespace ServPay.Views;


public partial class AccountPage : ContentPage
{
	public AccountPage()
	{
        InitializeComponent();

        BindingContext = new AccountViewModel();

    }
}