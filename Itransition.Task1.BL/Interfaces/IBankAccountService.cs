using System.Collections.Generic;
using Itransition.Task1.Domain;

namespace Itransition.Task1.BL.Interfaces
{
    public interface IBankAccountService
    {
        IList<BankAccount> GetAllBankAccounts();
        BankAccount GetBankAccountById(int id);
        void PutMoney(string userName, decimal money);
        void TransferMoney(AppUser user, decimal money, string toAccount);
    }
}
