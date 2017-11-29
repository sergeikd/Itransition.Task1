using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Itransition.Task1.DALMongo.Interfaces;
using Itransition.Task1.Domain;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Itransition.Task1.DALMongo.Repositories
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly AppDbContext _context;

        public BankAccountRepository(AppDbContext context)
        {
            if (context == null) throw new ArgumentNullException();
            _context = context;
        }
        public IEnumerable<BankAccount> GetAll()
        {
            return _context.BankAccounts.Find(new BsonDocument()).ToListAsync().GetAwaiter().GetResult();
        }

        public BankAccount GetSingle(Expression<Func<BankAccount, bool>> condition)
        {
            return _context.BankAccounts.Find(condition).FirstOrDefaultAsync().GetAwaiter().GetResult();
        }


        public void Add(BankAccount entity)
        {
            _context.BankAccounts.InsertOneAsync(entity).GetAwaiter().GetResult();
        }

        public void Edit(BankAccount entity)
        {
            var filter = MongoDB.Driver.Builders<BankAccount>.Filter.Eq("_id", entity.Id);
            _context.BankAccounts.ReplaceOneAsync(filter, entity).GetAwaiter().GetResult();
        }

        public void Delete(BankAccount entity)
        {
            var filter = MongoDB.Driver.Builders<BankAccount>.Filter.Eq("_id", entity.Id);
            _context.BankAccounts.DeleteOneAsync(filter).GetAwaiter().GetResult();
        }
    }
}
