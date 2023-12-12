using ServPay.View_Models;

namespace ServPay.Views;

public partial class TransferPage : ContentPage
{
	public TransferPage()
    {
		InitializeComponent();
        BindingContext = new PaymentTransferViewModel();

	}
}