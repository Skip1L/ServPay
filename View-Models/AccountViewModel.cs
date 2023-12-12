using CommunityToolkit.Mvvm.ComponentModel;
using ServPay.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ServPay.Views;

namespace ServPay.View_Models
{
    [QueryProperty("Email", "_email")]
    [QueryProperty("Cards", "_cards")]
    [QueryProperty("User", "_user")]
    [QueryProperty("TransactionHistory", "_transactionHistory")]
    public partial class AccountViewModel : ObservableObject
    {

        public ICommand AddCardCommand { get; }
        private string _email;

        public string Email
        {
            get => _email;
            set
            {
                SetProperty(ref _email, value);
                OnPropertyChanged();
            }
        }

        private User _user;

        public User User
        {
            get => _user;
            set
            {
                SetProperty(ref _user, value);
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Card> _cards;

        public ObservableCollection<Card> Cards
        {
            get => _cards;
            set
            {
                SetProperty(ref _cards, value);
                OnPropertyChanged();
            }
        }

        private ObservableCollection<TransactionDetails> _transactionHistory;

        public AccountViewModel()
        {
            TransferCommand = new Command(Transfer);
            AddCardCommand = new Command(AddCard);
        }

        private async void AddCard()
        {
            //await Shell.Current.GoToAsync(nameof(AddCardPage), true);
            await Application.Current!.MainPage!.DisplayAlert("Stop!", "To be continued...", "OK");

        }

        private async void Transfer()
        {
            var navigationParameter = new Dictionary<string, object>
            {
                {"_cards", Cards.ToList()}

            };
            await Shell.Current.GoToAsync(nameof(TransferPage), true, navigationParameter);
            //await Application.Current!.MainPage!.DisplayAlert("Stop!", "To be continued...", "OK");

        }

        [ObservableProperty] private string _senderName;

        public ObservableCollection<TransactionDetails> TransactionHistory
        {
            get => _transactionHistory;
            set
            {
                SetProperty(ref _transactionHistory, value);
                _senderName = User.Username;
                OnPropertyChanged();
            }
        }

        public ICommand TransferCommand { get; }
    }
}