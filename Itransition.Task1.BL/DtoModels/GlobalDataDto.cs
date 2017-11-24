using System.Collections.Generic;

namespace Itransition.Task1.BL.DtoModels
{
    public class GlobalDataDto
    {
        public decimal Amount { get; set; }
        public string OwnAccountNumber { get; set; }
        public List<string> OthersAccountNumbers { get; set; }
        public List<TransactionDto> Transactions { get; set; }
        public string ErrorMsg { get; set; }
    }
}
