namespace ServPay.Models
{
    public class Transaction
    {
        public int _id { get; set; }
        public int user_id { get; set; }
        public float amount { get; set; }
        public DateTime timestamp { get; set; }
        public int sender_card_id { get; set; }
        public int receiver_card_id { get; set; }

    }
}