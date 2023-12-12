using System.Text.Json.Serialization;

namespace ServPay.Models
{
    public class TransactionDetails
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("UserId")]
        public int UserId { get; set; }
        [JsonPropertyName("Amount")]
        public double Amount { get; set; }
        [JsonPropertyName("Timestamp")]
        public DateTime Timestamp { get; set; }
        [JsonPropertyName("SenderCardId")]
        public int SenderCardId { get; set; }
        [JsonPropertyName("ReceiverCardId")]
        public int ReceiverCardId { get; set; }

    }
}