using CorporateQnA.Models;
using Microsoft.Extensions.Configuration;
using Services.Helpers;
using System;

namespace Services.Authentication
{
    public class AuthService:IAuthService
    {
        private readonly Database Database;

        public AuthService() 
        {
            Database = new Database("CorporateQnADatabase", "SqlServer"); 
        }

        public Tuple<bool,User> Signup(User userData)
        {
            var user = Database.SingleOrDefault<Db.User>("SELECT * FROM UserDetails WHERE Email=@0", userData.Email);
            if (user != null)
            {
                return new Tuple<bool, User>(false,userData);
            }

            user = userData.MapTo<Db.User>();
            user.Password = BCrypt.Net.BCrypt.HashPassword(userData.Password);
            user.Id = Guid.NewGuid().ToString();

            Database.Execute("INSERT INTO UserDetails (Id,Email,Password,Name,JobProfile,Department,Location,ProfileImageUrl) " +
                "VALUES(@0,@1,@2,@3,@4,@5,@6,@7)",user.Id,user.Email,user.Password,user.Name,user.JobProfile,
                user.Department,user.Location,user.ProfileImageUrl);

            return new Tuple<bool, User>(true, user.MapTo<User>());
        }

        public User Login(string email, string password)
        {
            var user = Database.SingleOrDefault<Db.User>(
                "SELECT * FROM UserDetails WHERE Email=@0",email);

            if (user != null)
            {
                bool isMatch = BCrypt.Net.BCrypt.Verify(password,user.Password);
                if (isMatch)
                {
                    return user.MapTo<User>();
                }
            }
            return null;
        }
    }
}
