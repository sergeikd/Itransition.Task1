using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Itransition.Task1.DALMongo.Interfaces;
using Itransition.Task1.Domain;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Itransition.Task1.DALMongo.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository (AppDbContext context)
        {
            if (context == null) throw new ArgumentNullException();
            _context = context;
        }

        public IEnumerable<Transaction> GetAll()
        {
            return _context.Transactions.Find(new BsonDocument()).ToListAsync().GetAwaiter().GetResult();
        }

        public Transaction GetSingle(Expression<Func<Transaction, bool>> condition)
        {
            return _context.Transactions.Find(condition).FirstOrDefaultAsync().GetAwaiter().GetResult();
        }


        public void Add(Transaction entity)
        {
            _context.Transactions.InsertOneAsync(entity).GetAwaiter().GetResult();
        }

        public void Edit(Transaction entity)
        {
            var filter = Builders<Transaction>.Filter.Eq("_id", entity.Id);
            _context.Transactions.ReplaceOneAsync(filter, entity).GetAwaiter().GetResult();
        }

        public void Delete(Transaction entity)
        {
            var filter = Builders<Transaction>.Filter.Eq("_id", entity.Id);
            _context.Transactions.DeleteOneAsync(filter).GetAwaiter().GetResult();
        }
    }
}
