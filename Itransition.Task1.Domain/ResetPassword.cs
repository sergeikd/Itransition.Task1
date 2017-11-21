namespace Itransition.Task1.Domain
{
    public class ResetPassword : Entity
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
