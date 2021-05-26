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
        private readonly Database Database;

        public UserService()
        {
            Database = new Database("CorporateQnADatabase", "SqlServer");
        }

        public IEnumerable<User> SearchUsers(string keyword)
        {
            return Database.Query<Db.User>($"SELECT * FROM UserDetails WHERE Name LIKE '%{keyword}%'")
                .MapTo<IEnumerable<User>>();
        }

        public User GetUserById(string id)
        {
            return Database.SingleOrDefault<Db.User>("SELECT * FROM UserDetails WHERE Id=@0", id)
                .MapTo<User>();
        }

        public UserRating GiveUserRating(UserRating rate)
        {
            var rating = GetUserRating(rate);

            if (rating == null)
            {
                RateNewUser(rate);
            }
            else
            {
                RateOldUser(rate, rating);
            }

            return rate;
        }

        public UserRating GetUserRating(UserRating rate)
        {
            return Database.SingleOrDefault<Db.UserRating>(
                "SELECT * FROM UserRatings WHERE UserBeingRatedId=@0 and UserGivingRatingId=@1",
                rate.UserBeingRatedId,rate.UserGivingRatingId).MapTo<UserRating>();
        }

        private void RateNewUser(UserRating rate)
        {
            try
            {
                Database.BeginTransaction();

                if(rate.Liked && !rate.DisLiked)
                {
                    Database.Execute("UPDATE UserDetails " +
                        "SET Likes = Likes + 1 " +
                        "WHERE Id = @0", rate.UserBeingRatedId);
                }
                else if(!rate.Liked && rate.DisLiked)
                {
                    Database.Execute("UPDATE UserDetails " +
                        "SET Dislikes = Dislikes + 1 " +
                        "WHERE Id = @0", rate.UserBeingRatedId);
                }

                Database.Execute("INSERT INTO UserRatings(UserBeingRatedId,UserGivingRatingId,Liked,Disliked) " +
                    "VALUES(@0, @1, @2, @3)",
                    rate.UserBeingRatedId,rate.UserGivingRatingId,rate.Liked,rate.DisLiked);

                Database.CommitTransaction();
            }
            catch(Exception e)
            {
                Database.RollbackTransaction();
            }
        }

        private void RateOldUser(UserRating rate, UserRating previousRating)
        {
            try
            {
                Database.BeginTransaction();

                if(rate.Liked && !rate.DisLiked)
                {
                    if (previousRating.DisLiked)
                    {
                        Database.Execute("UPDATE UserDetails " +
                            "SET Likes = Likes + 1, Dislikes = Dislikes - 1 " +
                            "WHERE Id = @0", rate.UserBeingRatedId);
                    }
                    else
                    {
                        Database.Execute("UPDATE UserDetails " +
                            "SET Likes = Likes + 1 " +
                            "WHERE Id = @0", rate.UserBeingRatedId);
                    }
                }
                else if(!rate.Liked && rate.DisLiked)
                {
                    if (previousRating.Liked)
                    {
                        Database.Execute("UPDATE UserDetails " +
                            "SET Likes = Likes - 1, Dislikes = Dislikes + 1 " +
                            "WHERE Id = @0", rate.UserBeingRatedId);
                    }
                    else
                    {
                        Database.Execute("UPDATE UserDetails " +
                            "SET Dislikes = Dislikes + 1 " +
                            "WHERE Id = @0", rate.UserBeingRatedId);
                    }
                }
                else
                {
                    if (previousRating.Liked)
                    {
                        Database.Execute("UPDATE UserDetails " +
                            "SET Likes = Likes - 1 " +
                            "WHERE Id = @0", rate.UserBeingRatedId);
                    }
                    else
                    {
                        Database.Execute("UPDATE UserDetails " +
                            "SET Dislikes = Dislikes - 1 " +
                            "WHERE Id = @0", rate.UserBeingRatedId);
                    }
                }

                Database.Execute("UPDATE UserRatings " +
                    "SET Liked = @0, Disliked = @1 " +
                    "WHERE UserBeingRatedId = @2 and UserGivingRatingId = @3",
                    rate.Liked, rate.DisLiked, rate.UserBeingRatedId, rate.UserGivingRatingId);

                Database.CommitTransaction();
            }
            catch(Exception e)
            {
                Database.RollbackTransaction();
            }
        }
    }
}
