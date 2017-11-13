using System;
using System.Data.Entity;
using Itransition.Task1.Domain;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace Itransition.Task1.DAL
{
    public class DbInitializer : DropCreateDatabaseAlways<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var userList = new[] {
                            new { Name = "qqq@qqq.qq", Password = "qqqqqq" }, 
                            new { Name = "aaa@aaa.aa", Password = "aaaaaa" },
                            new { Name = "zzz@zzz.zz", Password = "zzzzzz" },
            };
            foreach (var user in userList)
            {
                var isentityUser = new ApplicationUser            
                {
                    Email = user.Name,
                    UserName = user.Name,
                    EmailConfirmed = true
                };
                userManager.Create(isentityUser, user.Password);
                isentityUser.BankAccount = new BankAccount { AccountNumber = Guid.NewGuid().ToString().ToUpper(), Amount = 0 };
            }
            context.SaveChanges();
        }
    }
}
