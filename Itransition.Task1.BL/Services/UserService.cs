﻿using Itransition.Task1.BL.Interfaces;
using Itransition.Task1.DALMongo.Interfaces;
using Itransition.Task1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Itransition.Task1.BL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            if(userRepository == null) throw new ArgumentNullException();
            _userRepository = userRepository;
        }
        public IList<AppUser> GetAllUsers()
        {
            return _userRepository.GetAll().ToList();
        }

        public AppUser GetCurrentUser(string name)
        {
            return _userRepository.GetSingle(u => u.Email == name);
        }

        public decimal GetUserAmount(string name)
        {
            return _userRepository.GetSingle(u => u.Email == name).BankAccount.Amount;
        }

        public AppUser GetUserById(int id)
        {
            return _userRepository.GetSingle(u => u.Id == id.ToString());
        }

        public void RegisterUser(AppUser user)
        {
            user.BankAccount = new BankAccount{AccountNumber = Guid.NewGuid().ToString(), Amount = 0};
            _userRepository.Add(user);
        }

        public void ChangePassword(string email, string password)
        {
            var user = _userRepository.GetSingle(u => u.Email == email);
            user.Password = password;
            _userRepository.Edit(user);
        }
    }
}
