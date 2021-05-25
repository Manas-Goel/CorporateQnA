using CorporateQnA.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Integrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporateQnA.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public ActionResult GetUser(string id)
        {
            var user = _userService.GetUserById(id);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest();
        }

        [HttpGet("search")]
        public IEnumerable<User> SearchUsers(string keyword)
        {
            return _userService.SearchUsers(keyword);
        }

        [HttpGet("getratings")]
        public UserRating GetUserRating(string userRated, string ratingUser)
        {
            UserRating rating = new UserRating()
            {
                UserBeingRatedId = userRated,
                UserGivingRatingId = ratingUser
            };

            return _userService.GetUserRating(rating);
        }

        [HttpGet("rate")]
        public UserRating GiveUserRating(string userRated, string ratingUser, bool liked, bool disliked)
        {
            UserRating rating = new UserRating()
            {
                UserBeingRatedId = userRated,
                UserGivingRatingId = ratingUser,
                Liked = liked,
                DisLiked = disliked
            };

            return _userService.GiveUserRating(rating);
        }
    }
}
