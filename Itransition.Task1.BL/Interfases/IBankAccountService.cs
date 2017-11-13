using System.Collections.Generic;
using Itransition.Task1.Domain;

namespace Itransition.Task1.BL.Interfases
{
    public interface IBankAccountService
    {
        IList<BankAccount> GetAllBankAccounts();
        BankAccount GetBankAccountById(int id);
        void PutMoney(ApplicationUser user, decimal money);
        void TransferMoney(ApplicationUser user, string money);
    }
}
