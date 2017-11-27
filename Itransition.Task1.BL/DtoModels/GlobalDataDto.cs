using System.Collections.Generic;
using Itransition.Task1.DAL.Paginator;
using Itransition.Task1.Domain;

namespace Itransition.Task1.BL.DtoModels
{
    public class GlobalDataDto
    {
        public decimal Amount { get; set; }
        public string OwnAccountNumber { get; set; }
        public List<string> OthersAccounts { get; set; }
        public Page<Transaction> Transactions { get; set; }
    }
}
