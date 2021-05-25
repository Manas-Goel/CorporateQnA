using CorporateQnA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Integrate
{
    public interface IUserService
    {
        public IEnumerable<User> SearchUsers(string keyword);
        public User GetUserById(string id);
        public UserRating GetUserRating(UserRating rate);
        public UserRating GiveUserRating(UserRating rate);
    }
}
