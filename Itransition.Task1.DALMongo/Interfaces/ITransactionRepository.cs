using Itransition.Task1.Domain;

namespace Itransition.Task1.DALMongo.Interfaces
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        //Page<Transaction> GetPagedTransactions(int pageSize, int currentPage, string searchText, string sortBy, string jobTitle);
    }
}
