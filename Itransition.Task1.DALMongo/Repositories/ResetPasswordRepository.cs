using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Itransition.Task1.DALMongo.Interfaces;
using Itransition.Task1.Domain;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Itransition.Task1.DALMongo.Repositories
{
    public class ResetPasswordRepository : IResetPasswordRepository
    {
        private readonly AppDbContext _context;

        public ResetPasswordRepository(AppDbContext context)
        {
            if (context == null) throw new ArgumentNullException();
            _context = context;
        }

        public IEnumerable<ResetPassword> GetAll()
        {
            return _context.ResetPasswords.Find(new BsonDocument()).ToListAsync().GetAwaiter().GetResult();
        }

        public ResetPassword GetSingle(Expression<Func<ResetPassword, bool>> condition)
        {
            return _context.ResetPasswords.Find(condition).FirstOrDefaultAsync().GetAwaiter().GetResult();
        }

        public void Add(ResetPassword entity)
        {
            _context.ResetPasswords.InsertOneAsync(entity).GetAwaiter().GetResult();
        }

        public void Edit(ResetPassword entity)
        {
            var filter = Builders<ResetPassword>.Filter.Eq("_id", entity.Id);
            _context.ResetPasswords.ReplaceOneAsync(filter, entity).GetAwaiter().GetResult();
        }

        public void Delete(ResetPassword entity)
        {
            var filter = Builders<ResetPassword>.Filter.Eq("_id", entity.Id);
            _context.ResetPasswords.DeleteOneAsync(filter).GetAwaiter().GetResult();
        }
    }
}
