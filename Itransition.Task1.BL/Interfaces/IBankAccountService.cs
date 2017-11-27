﻿using System.Collections.Generic;
using Itransition.Task1.BL.DtoModels;
using Itransition.Task1.Domain;

namespace Itransition.Task1.BL.Interfaces
{
    public interface IBankAccountService
    {
        IList<BankAccount> GetAllBankAccounts();
        BankAccount GetBankAccountById(int id);
        string PutMoney(string userName, string money);
        string TransferMoney(string userName, string money, string toAccount);
        GlobalDataDto GetGlobalData(string email, int pageSize, int currentPage, string sort);
        //GlobalDataDto GetGlobalData(BankAccount ownAccount);
    }
}
