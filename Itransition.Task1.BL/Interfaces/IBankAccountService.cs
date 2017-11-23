﻿using System.Collections.Generic;
using Itransition.Task1.BL.DtoModels;
using Itransition.Task1.Domain;

namespace Itransition.Task1.BL.Interfaces
{
    public interface IBankAccountService
    {
        IList<BankAccount> GetAllBankAccounts();
        BankAccount GetBankAccountById(int id);
        GlobalDataDto PutMoney(string userName, string money);
        GlobalDataDto TransferMoney(string userName, string money, string toAccount);
        GlobalDataDto GetInitGlobalData(string email);
        GlobalDataDto GetGlobalData(BankAccount ownAccount);
    }
}
