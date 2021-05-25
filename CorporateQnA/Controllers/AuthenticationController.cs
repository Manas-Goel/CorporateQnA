using CorporateQnA.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Authentication;

namespace CorporateQnA.Controllers
{
    public class AuthenticationController : ControllerBase
    {

        private readonly IAuthService _authService;
        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public ActionResult Signup([FromBody] User userData)
        {
            var tuple = _authService.Signup(userData);
            var result = tuple.Item1;
            var user = tuple.Item2;

            if (result)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest(new { message = "Email already registered!" });
            }
        }

        [HttpPost]
        public ActionResult Login([FromBody] User userData)
        {
            var user = _authService.Login(userData.Email, userData.Password);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest();
        }
    }
}