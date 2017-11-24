using System;

namespace Itransition.Task1.Domain
{
    public class Transaction : Entity
    {
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
