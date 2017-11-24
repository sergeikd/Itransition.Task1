using Itransition.Task1.DAL.Interfaces;
using Itransition.Task1.Domain;

namespace Itransition.Task1.DAL.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    { 
        public TransactionRepository(AppDbContext context)
        : base(context)
        { }
    }   
}
