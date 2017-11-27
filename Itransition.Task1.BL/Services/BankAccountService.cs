using System;
using System.Collections.Generic;
using System.Globalization;
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
        private readonly ITransactionRepository _transactionRepository;
        public BankAccountService(IBankAccountRepository bankAccountRepository, IUserRepository userRepository, ITransactionRepository transactionRepository)
        {
            if (bankAccountRepository == null) throw new ArgumentNullException();
            if (userRepository == null) throw new ArgumentNullException();
            if (transactionRepository == null) throw new ArgumentNullException();
            _bankAccountRepository = bankAccountRepository;
            _userRepository = userRepository;
            _transactionRepository = transactionRepository;
        }

        public IList<BankAccount> GetAllBankAccounts()
        {
            return _bankAccountRepository.GetAll().ToList();
        }

        public BankAccount GetBankAccountById(int id)
        {
            throw new NotImplementedException();
        }

        public string PutMoney(string email, string money)
        {
            var result = string.Empty;
            try
            {
                var moneyValue = StringToDecimal(money);
                var account = _userRepository.GetSingle(u => u.Email == email).BankAccount;
                account.Amount += moneyValue;
                _bankAccountRepository.Edit(account);
                var transaction = new Transaction
                {
                    Amount = moneyValue,
                    Sender = account.AccountNumber,
                    Recipient = account.AccountNumber,
                    Date = DateTime.Now
                };
                _transactionRepository.Add(transaction);
            }
            catch (FormatException)
            {
                result = "Please enter correct value!";
            }

            return result;
        }

        public string TransferMoney(string email, string money , string toAccount)
        {
            var result = string.Empty;
            var ownAccount = _userRepository.GetSingle(u => u.Email == email).BankAccount;
            decimal moneyValue;
            try
            {
                moneyValue = StringToDecimal(money);
            }
            catch (FormatException)
            {
                return "Please enter correct value!";
            }

            if (ownAccount.Amount < moneyValue)
            {
                return "Insufficient money!";
            }
            var destAccount = _bankAccountRepository.GetSingle(a => a.AccountNumber == toAccount);
            if (destAccount == null)
            {
                return "No such recipient!";
            }
            ownAccount.Amount -= moneyValue;
            _bankAccountRepository.Edit(ownAccount);
            destAccount.Amount += moneyValue;
            _bankAccountRepository.Edit(destAccount);
            var transaction = new Transaction
            {
                Amount = moneyValue,
                Sender = ownAccount.AccountNumber,
                Recipient = destAccount.AccountNumber,
                Date = DateTime.Now
            };
            _transactionRepository.Add(transaction);
            return result;
        }
        //public GlobalDataDto GetGlobalData(BankAccount ownAccount)
        //{
        //    var globalData = new GlobalDataDto();
        //    var transactionDtoList = new List<TransactionDto>();
        //    var othersAccounts = GetAllBankAccounts().Where(x => x.AccountNumber != ownAccount.AccountNumber).Select(a => a.AccountNumber).ToList(); //Remove own account from the list
        //    var transactions = _transactionRepository.GetAll().Where(t => t.Sender == ownAccount.AccountNumber).ToList();
        //    foreach (var transaction in transactions)
        //    {
        //        transactionDtoList.Add(new TransactionDto { Id = transaction.Id, Amount = transaction.Amount, Sender = transaction.Sender, Recipient = transaction.Recipient, Date = transaction.Date });
        //    }
        //    globalData.Amount = ownAccount.Amount;
        //    globalData.OwnAccountNumber = ownAccount.AccountNumber;
        //    globalData.OthersAccountNumbers = othersAccounts;
        //    globalData.Transactions = transactionDtoList;
        //    return globalData;
        //}
        public GlobalDataDto GetGlobalData(string email, int pageSize, int currentPage)
        {
            var ownAccount = _userRepository.GetSingle(u => u.Email == email).BankAccount;
            var othersAccounts = GetAllBankAccounts().Where(x => x.AccountNumber != ownAccount.AccountNumber).Select(a => a.AccountNumber).ToList(); //Remove own account from the list
            //var transactions = _transactionRepository.GetAll().Where(t => t.Sender == ownAccount.AccountNumber).ToList();
            var transactions = _transactionRepository.GetPagedTransactions(pageSize, currentPage, null, 1, null); //TODO fill here with actual arguments
            var globalData = new GlobalDataDto
            {
                Amount = ownAccount.Amount,
                OwnAccountNumber = ownAccount.AccountNumber,
                OthersAccounts = othersAccounts,
                Transactions = transactions
            };
            return globalData;
        }

        private static decimal StringToDecimal(string str)
        {
            str = str.Replace("_", "");
            str = str.Replace(",", ".");
            return Convert.ToDecimal(str);
        }
    }
}
