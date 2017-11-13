using System;
using System.Collections.Generic;
using Itransition.Task1.BL.Interfases;
using Itransition.Task1.DAL;
using Itransition.Task1.DAL.Interfaces;
using Itransition.Task1.Domain;

namespace Itransition.Task1.BL.Services 
{
    public class BankAccountService : IBankAccountService
    {

        private readonly AppDbContext _context;
        private readonly IBankAccountRepository _bankAccountRepository;
        public BankAccountService(AppDbContext context, IBankAccountRepository bankAccountRepository)
        {
            _context = context ?? throw new ArgumentNullException();
            _bankAccountRepository = bankAccountRepository ?? throw new ArgumentNullException();
        }

        public IList<BankAccount> GetAllBankAccounts()
        {
            throw new NotImplementedException();
        }

        public BankAccount GetBankAccountById(int id)
        {
            throw new NotImplementedException();
        }

        public void PutMoney(ApplicationUser user, decimal money)
        {
            decimal sum;

            try
            {
                sum = Convert.ToDecimal(money);
            }
            catch (FormatException)
            {
                throw new FormatException();
            }
            
            var account = user.BankAccount;
            account.Amount += sum;
            _bankAccountRepository.Edit(account);
        }

        public void TransferMoney(ApplicationUser user, string money)
        {
            throw new NotImplementedException();
        }
    }
}
