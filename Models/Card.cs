using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServPay.Models
{
    public class Card
    {
        [JsonPropertyName("UserId")]
        public int UserId { get; set; }
        [JsonPropertyName("CardNumber")]
        public string CardNumber { get; set; }
        [JsonPropertyName("ExpiryDate")]
        public DateTime ExpiryDate { get; set; }
        [JsonPropertyName("Cvv")]
        public int Cvv { get; set; }
        [JsonPropertyName("IsExpired")]
        public bool IsExpired { get; set; }
        [JsonPropertyName("Balance")]
        public double Balance { get; set; }
        [JsonPropertyName("Id")]
        public int Id { get; set; }
    }
}
