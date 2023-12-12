using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using ServPay.Models;
using ServPay.Services;
using ServPay.Views;

namespace ServPay.View_Models;

[QueryProperty("Cards", "_cards")]
public partial class PaymentTransferViewModel : ObservableObject
{
    private List<Card> _cards;

    public List<Card> Cards
    {
        get => _cards;
        set
        {
            SetProperty(ref _cards, value);
            CardName = value.Select(c => c.CardNumber).ToList();
            OnPropertyChanged();
        }
    }

    [ObservableProperty] private List<string> _cardName;

    private string _selectedCard;
    public string SelectedCard
    {
        get => _selectedCard;
        set
        {
            _selectedCard = value;
            OnPropertyChanged();
        }
    }

    private string _receiverCard;
    public string ReceiverCard
    {
        get => _receiverCard;
        set
        {
            _receiverCard = value;
            OnPropertyChanged();
        }
    }

    private double _amount;
    public double Amount
    {
        get => _amount;
        set
        {
            _amount = value;
            OnPropertyChanged();
        }
    }

    public ICommand TransferCommand { get; }

    public PaymentTransferViewModel()
    {
        TransferCommand = new Command(Transfer);
    }

    private LoginServices _loginService;

    private async void Transfer()
    {
        try
        {
            _loginService = new LoginServices();
            var senderCard = Cards.Find(c => c.CardNumber == SelectedCard);
            var receiveCard = await _loginService.GetCardByNumber(ReceiverCard);
            if (!(senderCard!.Balance > Amount)) throw new ArgumentException("Not enough money!");
            FirebaseClient firebaseClient =
                new("https://servpay-1a2b5-default-rtdb.europe-west1.firebasedatabase.app/");
            var newTransaction = new TransactionDetails()
            {
                Id = _loginService.GetTransCount(),
                Amount = Amount,
                ReceiverCardId = receiveCard.Id,
                SenderCardId = senderCard.Id,
                Timestamp = DateTime.Now,
                UserId = senderCard.UserId

            };
            await firebaseClient.Child("Transactions").Child(newTransaction.Id.ToString())
                .PutAsync(JsonConvert.SerializeObject(newTransaction));
            senderCard.Balance -= Amount;
            receiveCard.Balance += Amount;
            await firebaseClient.Child("Cards").Child(senderCard.Id.ToString()).Child("Balance")
                .PutAsync(senderCard.Balance);
            await firebaseClient.Child("Cards").Child(receiveCard.Id.ToString()).Child("Balance")
                .PutAsync(receiveCard.Balance);
            _loginService.Refresh();
            await Application.Current!.MainPage!.DisplayAlert("Success", "Transaction successfully", "OK");
            var navigationParameter = new Dictionary<string, object>
            {
                {"_email", _loginService.GetUserByCardId(senderCard.Id).Email},
                {"_cards", _loginService.GetCardsByUserId(senderCard.UserId)},
                {"_user", _loginService.GetUserByCardId(senderCard.Id)},
                {"_transactionHistory", _loginService.GetTransactionsByUserId(_loginService.GetUserByCardId(senderCard.Id).Id)}

            };
            Shell.Current.GoToAsync($"{nameof(AccountPage)}", true, navigationParameter);
        }
        catch (Exception e)
        {
            await Application.Current!.MainPage!.DisplayAlert("Error", e.Message, "OK");
        }
        
    }
}