using System.Collections.ObjectModel;
using Newtonsoft.Json;
using ServPay.Models;

namespace ServPay.Services
{
    internal class LoginServices
    {
        public LoginServices()
        {
            Refresh();
        }

        private static int CountOfUsers { get; set; }
        private static int CountOfTransactions { get; set; }
        //private static int CountOfCards { get; set; }

        private const string FirebaseClientLink =
            "https://servpay-1a2b5-default-rtdb.europe-west1.firebasedatabase.app/";

        private static ObservableCollection<User> _users;
        private static ObservableCollection<Card> _cards;
        private static ObservableCollection<TransactionDetails> _transactions;

        public bool IsUserExist(string email, string password)
        {
            return _users?.Any(u => u.Email == email && u.Password == password) ?? false;
        }

        public bool IsUserExist(string email)
        {
            return _users?.Any(u => u.Email == email) ?? false;
        }

        private static async Task<ObservableCollection<T>> GetObservableCollection<T>(string table)
        {
            using var httpClient = new HttpClient();
            try
            {
                string req = FirebaseClientLink + table + "/.json";
                var response = await httpClient.GetAsync(req);

                if (!response.IsSuccessStatusCode) throw new ArgumentException("Response is failed", nameof(response));
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ObservableCollection<T>>(json);
            }
            catch (Exception e)
            {
                await Application.Current!.MainPage!.DisplayAlert("Error", $"Failed to fetch data. Status code:",
                    e.Message, "OK");
            }

            return new ObservableCollection<T>();
        }

        public int GetUserCount() => CountOfUsers+1;
        public int GetTransCount() => CountOfTransactions+1;
        //public int GetCardsCount() => CountOfCards+1;


        public async void Refresh()
        {
            _users = await GetObservableCollection<User>("Users");
            _cards = await GetObservableCollection<Card>("Cards");
            _transactions = await GetObservableCollection<TransactionDetails>("Transactions");
            CountOfUsers = _users.Max(u => u.Id);
            CountOfTransactions = _transactions?.Max(u => u.Id) ?? 0;
        }

        public int GetUserId(string email)
        {
            return _users.ToList().Find(u => u.Email == email).Id;
        }

        public User GetUserById(int id) => _users.ToList().Find(u => u.Id == id);

        public ObservableCollection<Card> GetCardsByUserId(int id) => new(_cards.Where(c => c.UserId == id));
        public async Task<Card> GetCardByNumber(string number)
        {
            var res = await GetObservableCollection<Card>("Cards");
            return res.ToList().Find(c => c.CardNumber == number);
        }

        public ObservableCollection<TransactionDetails> GetTransactionsByUserId(int id)
        {
            if (_transactions == null)
            {
                return new ObservableCollection<TransactionDetails>();
            }
            ObservableCollection<TransactionDetails> listTrans = new(_transactions.Where(c => c.UserId == id));
            return listTrans;
        }

        public User GetUserByCardId(int id) => _users.ToList().Find(u => u.Id == (_cards.ToList().Find(c => c.Id == id).UserId));
    }
}