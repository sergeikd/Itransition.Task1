using System;
using Itransition.Task1.DALMongo.Repositories;
using Itransition.Task1.Domain;

namespace Itransition.Task1.DALMongo
{
    public class DbInitializer
    {
        public void Initialize()
        {
            var rnd = new Random();
            var context = new AppDbContext();
            context.DropDataBase();
            var userRepository = new UserRepository(context);

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
                    Password = user.Password,
                    BankAccount = new BankAccount
                    {
                        AccountNumber = Guid.NewGuid().ToString().ToUpper(),
                        Amount = rnd.Next(10) * 10
                    }
                };
                userRepository.Add(newUser);
            }
        }
    }
}
