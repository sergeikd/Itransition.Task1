namespace Itransition.Task1.Domain
{
    public class AppUser : Entity
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public virtual BankAccount BankAccount { get; set; }
    }
}
