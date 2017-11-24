using System;

namespace Itransition.Task1.BL.DtoModels
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public decimal Amount { get; set; }
        public string Date { get; set; }
    }
}
