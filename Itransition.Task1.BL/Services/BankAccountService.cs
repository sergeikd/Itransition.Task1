using System;
using System.Collections.Generic;
using System.Linq;
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
            _bankAccountRepository = bankAccountRepository ?? throw new ArgumentNullException();
            _userRepository = userRepository ?? throw new ArgumentNullException();
        }

        public IList<BankAccount> GetAllBankAccounts()
        {
            return _bankAccountRepository.GetAll().ToList();
        }

        public BankAccount GetBankAccountById(int id)
        {
            throw new NotImplementedException();
        }

        public void PutMoney(string userName, decimal money)
        {
            var account = _userRepository.GetSingle(u => u.Name == userName).BankAccount;
            account.Amount += money;
            _bankAccountRepository.Edit(account);
        }

        public void TransferMoney(AppUser user, decimal money , string toAccount)
        {
            var ownAccount = user.BankAccount;
            if (ownAccount.Amount < money)
            {
                throw new ArgumentOutOfRangeException();
            }
            var destAccount = _bankAccountRepository.GetSingle(a => a.AccountNumber == toAccount);
            if (destAccount == null)
            {
                throw new ArgumentNullException();
            }
            ownAccount.Amount -= money;
            _bankAccountRepository.Edit(ownAccount);
            destAccount.Amount += money;
            _bankAccountRepository.Edit(destAccount);
        }
    }
}
