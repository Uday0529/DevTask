using DevTask2.Business.ServiceInterface;
using DevTask2.Models.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevTask2.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        //POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterAuthModel addUser, CancellationToken cancellationToken)
        {
            var result = await _authService.RegisterUser(addUser, cancellationToken);
            if (!result)
            {
                return BadRequest(result);
            }
            return Ok("User Registered Successfully.");
        }

        //POST: api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult> LoginUser(UserAuthModel userModel, CancellationToken cancellationToken)
        {
            var token = await _authService.LoginUserAsync(userModel, cancellationToken);
            if (token == null)
            {
                return BadRequest("Invalid Username or Password.");
            }
            return Ok(token);

        }

        [Authorize(Roles = "Admin")]
        [HttpGet("AdminAuthOnly")]
        public IActionResult AuthenticatedOnlyEndpoint()
        {
            return Ok("Admin You are Arthenticated!");
        }

        [Authorize(Roles = "User")]
        [HttpGet("UserOnly")]
        public IActionResult UserAuthenticatedOnlyEndpoint()
        {
            return Ok("User you are Arthenticated!");
        }

    }
}
