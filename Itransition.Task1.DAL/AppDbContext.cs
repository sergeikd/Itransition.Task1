using System.Data.Entity;
using Itransition.Task1.Domain;

namespace Itransition.Task1.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection")
        { }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<ResetPassword> ResetPasswords { get; set; }
    }
}
