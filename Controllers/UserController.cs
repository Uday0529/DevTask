using DevTask2.Business.ServiceInterface;
using DevTask2.Models.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevTask2.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ViewUserModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public enum Property
        {
            Id,
            UserName
        }

        //GET: api/users
        [HttpGet]
        public Task<IEnumerable<ViewUserModel>> GetAllUsers(CancellationToken cancellationToken)
        {
            return _userService.GetAllUsersAsync(cancellationToken);

        }

        //GET: api/users/{id}
        [HttpGet("{userId}")]
        public Task<ViewUserModel?> GetUserById(string userId, CancellationToken cancellationToken)
        {
            return _userService.GetUserByIdAsync(userId, cancellationToken);
        }

        //GET: api/users/search?username=...
        [HttpGet("search")]
        public async Task<IActionResult?> GetUserByUserName([FromQuery] string username, CancellationToken cancellationToken)
        {
            var result = await _userService.GetUserByUsername(username, cancellationToken);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);

        }

        //DELETE: api/users/delete
        [HttpDelete("Delete")]
        public Task<bool> DeleteUser([FromQuery] Property Prop, string Value, CancellationToken cancellationToken)
        {
            return _userService.DeleteUserAsync(Prop.ToString(), Value, cancellationToken);
        }
    }
}
