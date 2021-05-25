using CorporateQnA.Models;
using System;

namespace Services.Authentication
{
    public interface IAuthService
    {
        public Tuple<bool, User> Signup(User user);
        public User Login(string email, string password);
    }
}
