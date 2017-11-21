using System.Collections.Generic;
using Itransition.Task1.Domain;

namespace Itransition.Task1.BL.Interfaces
{
    public interface IPasswordService
    {
        IList<ResetPassword> GetAllResets();
        void RemoveResets(string email);
        void SendForgotPasswordEmail(string rootUrl, string email);
    }
}
