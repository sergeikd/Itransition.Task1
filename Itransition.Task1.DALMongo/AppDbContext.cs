using System.Configuration;
using Itransition.Task1.Domain;
using MongoDB.Driver;

namespace Itransition.Task1.DALMongo
{
    public class AppDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly MongoClient _client;
        private readonly MongoUrlBuilder _connection;

        public AppDbContext()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;
            var connection = new MongoUrlBuilder(connectionString);
            var client = new MongoClient(connectionString);
            _connection = connection;
            _client = client;
            _database = client.GetDatabase(connection.DatabaseName);
        }

        public void DropDataBase()
        {
            _client.DropDatabase(_connection.DatabaseName);
        }
        public IMongoCollection<AppUser> Users
        {
            get { return _database.GetCollection<AppUser>("Users"); }
        }

        public IMongoCollection<BankAccount> BankAccounts
        {
            get { return _database.GetCollection<BankAccount>("BankAccounts"); }
        }

        public IMongoCollection<ResetPassword> ResetPasswords
        {
            get { return _database.GetCollection<ResetPassword>("ResetPasswords"); }
        }

        public IMongoCollection<Transaction> Transactions
        {
            get { return _database.GetCollection<Transaction>("Transactions"); }
        }
    }
}
