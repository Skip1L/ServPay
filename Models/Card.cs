using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServPay.Models
{
    class Card
    {
        public int UserId { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Cvv { get; set; }
        public bool IsExpired { get; set; }
        public float Balance { get; set; }
    }
}
