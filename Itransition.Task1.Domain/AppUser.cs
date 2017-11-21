using System.Collections.Generic;

namespace Itransition.Task1.Domain
{
    public class AppUser : Entity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual BankAccount BankAccount { get; set; }
    }
}
