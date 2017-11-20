using System;
using System.Data.Entity;
using Itransition.Task1.Domain;

namespace Itransition.Task1.DAL
{
    public class DbInitializer : DropCreateDatabaseAlways<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            var rnd = new Random();

            var userList = new[] {
                            new { Email = "qqq@qqq.qq", Password = "qqq" }, 
                            new { Email = "aaa@aaa.aa", Password = "aaa" },
                            new { Email = "zzz@zzz.zz", Password = "zzz" },
            };
            foreach (var user in userList)
            {
                var newUser = new AppUser            
                {
                    Email = user.Email,
                    Password = user.Password
                };
                context.Users.Add(newUser);
                newUser.BankAccount = new BankAccount { AccountNumber = Guid.NewGuid().ToString().ToUpper(), Amount = rnd.Next(10)*10 };
            }
            context.SaveChanges();
        }
    }
}
