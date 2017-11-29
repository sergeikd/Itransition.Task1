using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Itransition.Task1.DALMongo.Interfaces;
using Itransition.Task1.Domain;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Itransition.Task1.DALMongo.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            if (context == null) throw new ArgumentNullException();
            _context = context;
        }

        public IEnumerable<AppUser> GetAll()
        {
            return _context.Users.Find(new BsonDocument()).ToListAsync().GetAwaiter().GetResult();
        }

        public AppUser GetSingle(Expression<Func<AppUser, bool>> condition)
        {
            return _context.Users.Find(condition).FirstOrDefaultAsync().GetAwaiter().GetResult();
        }


        public void Add(AppUser entity)
        {
            _context.Users.InsertOneAsync(entity).GetAwaiter().GetResult();
        }

        public void Edit(AppUser entity)
        {
            var filter = Builders<AppUser>.Filter.Eq("_id", entity.Id);
            _context.Users.ReplaceOneAsync(filter, entity).GetAwaiter().GetResult();
        }

        public void Delete(AppUser entity)
        {
            var filter = Builders<AppUser>.Filter.Eq("_id", entity.Id);
            _context.Users.DeleteOneAsync(filter).GetAwaiter().GetResult();
        }
    }

}
