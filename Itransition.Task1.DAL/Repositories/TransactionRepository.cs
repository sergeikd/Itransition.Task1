using Itransition.Task1.DAL.Interfaces;
using Itransition.Task1.DAL.Paginator;
using Itransition.Task1.Domain;

namespace Itransition.Task1.DAL.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        private readonly AppDbContext _context;
        public TransactionRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public Page<Transaction> GetPagedTransactions(int pageSize, int currentPage, string searchText, string sortBy, string jobTitle)
        {
            var filters = new Filters<Transaction>();
            //filters.Add(!string.IsNullOrEmpty(searchText), x=>x.LoginID.Contains(searchText));
            //filters.Add(!string.IsNullOrEmpty(jobTitle), x=> x.JobTitle.Equals(jobTitle));
            
            //filters.Add(!string.IsNullOrEmpty(jobTitle), x => x.Recipient.Equals(jobTitle));
            filters.Add(!string.IsNullOrEmpty(searchText), x => x.Sender.Contains(searchText));

            var sorts = new Sorts<Transaction>();
            //sorts.Add(sortBy == 1, x=>x.BusinessEntityID);
            //sorts.Add(sortBy == 2, x=>x.LoginID);
            //sorts.Add(sortBy == 3, x=>x.JobTitle);
            sorts.Add(sortBy == "Id", x => x.Id);
            sorts.Add(sortBy == "Date", x => x.Date);
            sorts.Add(sortBy == "Sender", x => x.Sender);
            sorts.Add(sortBy == "Recipient", x => x.Recipient);
            sorts.Add(sortBy == "Amount", x => x.Amount);

            var pagedTransactions = _context.Transactions.Paginate(currentPage, pageSize, sorts, filters);
            return pagedTransactions;
        }
    }   
}
