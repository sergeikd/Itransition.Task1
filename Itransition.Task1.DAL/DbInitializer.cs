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
                            new { Name = "qqq", Password = "qqq" }, 
                            new { Name = "aaa", Password = "aaa" },
                            new { Name = "zzz", Password = "zzz" },
            };
            foreach (var user in userList)
            {
                var newUser = new AppUser            
                {
                    Name = user.Name,
                    Password = user.Password
                };
                context.Users.Add(newUser);
                newUser.BankAccount = new BankAccount { AccountNumber = Guid.NewGuid().ToString().ToUpper(), Amount = rnd.Next(10)*10 };
            }
            context.SaveChanges();
        }
    }
}
