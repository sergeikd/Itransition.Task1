using System.Data.Entity;
using Itransition.Task1.Domain;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Itransition.Task1.DAL
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }
}
