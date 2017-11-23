using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Itransition.Task1.BL.Interfaces;
using Itransition.Task1.DAL.Interfaces;
using Itransition.Task1.Domain;

namespace Itransition.Task1.BL.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly IResetPasswordRepository _resetPasswordRepository;

        public PasswordService (IResetPasswordRepository resetPasswordRepository)
        {
            if (resetPasswordRepository == null) throw new ArgumentNullException();
            _resetPasswordRepository = resetPasswordRepository;
        }

        public IList<ResetPassword> GetAllResets()
        {
            return _resetPasswordRepository.GetAll().ToList();
        }

        public void RemoveResets(string email)
        {
            var resetPasswords = _resetPasswordRepository.GetAll().ToList().Where(x => x.Email == email);
            if (!resetPasswords.Any()) return;
            foreach (var item in resetPasswords)
            {
                _resetPasswordRepository.Delete(item);
            }
        }

        public void SendForgotPasswordEmail(string rootUrl, string email)
        {
            var code = Guid.NewGuid().ToString().Replace("-", "").ToLower();
            var rstPwd = _resetPasswordRepository.GetSingle(r => r.Email == email);
            if (rstPwd == null)
            {
                _resetPasswordRepository.Add(new ResetPassword { Email = email, Code = code });
            }
            else
            {
                rstPwd.Code = code;
                _resetPasswordRepository.Edit(rstPwd);
            }
            var resetData = "?email=" + email + "&code=" + code;
            var text = "Hello! Please follow the link for reset your password: " +
                       rootUrl + "Password/ResetResult" + resetData;
            SendEmail(text);
        }

        public void SendEmail(string text)
        {
            var from = new MailAddress("task1.itransition@gmail.com", "Admin");
            var to = new MailAddress("task1.itransition@gmail.com");
            var msg = new MailMessage(from, to)
            {
                Subject = "Password reset",
                Body = text,
                IsBodyHtml = true
            };
            var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("task1.itransition@gmail.com", "itransition"),
                EnableSsl = true
            };
            smtp.Send(msg);
        }
    }
}

