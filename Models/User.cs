using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace ServPay.Models
{
    public class User
    {
        [JsonPropertyName("Email")]
        public string Email { get; set; }
        [JsonPropertyName("Password")]
        public string Password { get; set; }
        [JsonPropertyName("Username")]
        public string Username { get; set; }
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        
    }
}
