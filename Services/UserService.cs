using CorporateQnA.Models;
using Services.Helpers;
using Services.Integrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService:IUserService
    {
        public IEnumerable<User> SearchUsers(string keyword)
        {
            return SqlHelper.Query<Db.User>($"SELECT * FROM UserDetails WHERE Name LIKE '%{keyword}%'")
                .MapTo<IEnumerable<User>>();
        }

        public User GetUserById(string id)
        {
            return SqlHelper.SingleOrDefault<Db.User>("SELECT * FROM UserDetails WHERE Id=@0", id)
                .MapTo<User>();
        }

        public UserRating GiveUserRating(UserRating rate)
        {
            var rating = GetUserRating(rate);

            if (rating == null)
            {
                SqlHelper.Execute("EXECUTE spRateNewUser @0,@1,@2,@3", rate.Liked, rate.DisLiked,
                    rate.UserBeingRatedId, rate.UserGivingRatingId);
            }
            else
            {
                SqlHelper.Execute("EXECUTE spRateOldUser @0,@1,@2,@3,@4,@5",
                    rate.Liked, rate.DisLiked, rate.UserBeingRatedId,
                    rate.UserGivingRatingId, rating.Liked, rating.DisLiked);
            }

            return rate;
        }

        public UserRating GetUserRating(UserRating rate)
        {
            return SqlHelper.SingleOrDefault<Db.UserRating>(
                "SELECT * FROM UserRatings WHERE UserBeingRatedId=@0 and UserGivingRatingId=@1",
                rate.UserBeingRatedId,rate.UserGivingRatingId).MapTo<UserRating>();
        }
    }
}
