using System;
using System.Collections.Generic;
using System.Linq;
using Itransition.Task1.BL.DtoModels;
using Itransition.Task1.BL.Interfaces;
using Itransition.Task1.DAL.Interfaces;
using Itransition.Task1.Domain;

namespace Itransition.Task1.BL.Services 
{
    public class BankAccountService : IBankAccountService
    {

        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IUserRepository _userRepository;
        public BankAccountService(IBankAccountRepository bankAccountRepository, IUserRepository userRepository)
        {
            if (bankAccountRepository == null) throw new ArgumentNullException();
            if (userRepository == null) throw new ArgumentNullException();
            _bankAccountRepository = bankAccountRepository;
            _userRepository = userRepository;
        }

        public IList<BankAccount> GetAllBankAccounts()
        {
            return _bankAccountRepository.GetAll().ToList();
        }

        public BankAccount GetBankAccountById(int id)
        {
            throw new NotImplementedException();
        }

        public GlobalDataDto PutMoney(string email, string money)
        {
            GlobalDataDto globalData;
            try
            {
                money = money.Replace("_", "");
                money = money.Replace(",", ".");
                var moneyValue = Convert.ToDecimal(money);
                var account = _userRepository.GetSingle(u => u.Email == email).BankAccount;
                account.Amount += moneyValue;
                _bankAccountRepository.Edit(account);
                globalData = GetGlobalData(account);
            }
            catch (FormatException)
            {
                var account = _userRepository.GetSingle(u => u.Email == email).BankAccount;
                globalData = GetGlobalData(account);
                globalData.ErrorMsg = "Please enter correct value!";
            }

            return globalData;
        }

        public GlobalDataDto TransferMoney(string email, string money , string toAccount)
        {
            GlobalDataDto globalData;
            var ownAccount = _userRepository.GetSingle(u => u.Email == email).BankAccount;
            decimal moneyValue;
            try
            {
                money = money.Replace("_", "");
                money = money.Replace(",", ".");
                moneyValue = Convert.ToDecimal(money);
            }
            catch (FormatException)
            {
                globalData = GetGlobalData(ownAccount);
                globalData.ErrorMsg = "Please enter correct value!";
                return globalData;
            }

            if (ownAccount.Amount < moneyValue)
            {
                globalData = GetGlobalData(ownAccount);
                globalData.ErrorMsg = "Insufficient money!";
                return globalData;
            }
            var destAccount = _bankAccountRepository.GetSingle(a => a.AccountNumber == toAccount);
            if (destAccount == null)
            {
                globalData = GetGlobalData(ownAccount);
                globalData.ErrorMsg = "No such recipient!";
                return globalData;
            }
            ownAccount.Amount -= moneyValue;
            _bankAccountRepository.Edit(ownAccount);
            destAccount.Amount += moneyValue;
            _bankAccountRepository.Edit(destAccount);
            return GetGlobalData(ownAccount);
        }
        public GlobalDataDto GetGlobalData(BankAccount ownAccount)
        {
            var globalData = new GlobalDataDto();
            var othersAccounts = GetAllBankAccounts().Where(x => x.AccountNumber != ownAccount.AccountNumber).Select(a => a.AccountNumber).ToList(); //Remove own account from the list
            globalData.Amount = ownAccount.Amount;
            globalData.OwnAccountNumber = ownAccount.AccountNumber;
            globalData.OthersAccountNumbers = othersAccounts;
            return globalData;
        }
        public GlobalDataDto GetInitGlobalData(string email)
        {
            var globalData = new GlobalDataDto();
            var ownAccount = _userRepository.GetSingle(u => u.Email == email).BankAccount;
            var othersAccounts = GetAllBankAccounts().Where(x => x.AccountNumber != ownAccount.AccountNumber).Select(a => a.AccountNumber).ToList(); //Remove own account from the list
            globalData.Amount = ownAccount.Amount;
            globalData.OwnAccountNumber = ownAccount.AccountNumber;
            globalData.OthersAccountNumbers = othersAccounts;
            return globalData;
        }
    }
}
