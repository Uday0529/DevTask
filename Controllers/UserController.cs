using DevTask2.Business.ServiceInterface;
using DevTask2.Models.UserModels;
using Microsoft.AspNetCore.Mvc;

namespace DevTask2.Controllers
{
    [Route("api/AdminUser")]
    [ApiController]
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

        [HttpGet]
        public Task<IEnumerable<ViewUserModel>> GetAllUsers(CancellationToken cancellationToken)
        {
            return _userService.GetAllUsersAsync(cancellationToken);
        }

        [HttpGet("{UserId}")]
        public Task<ViewUserModel> GetUserById([FromRoute] string UserId, CancellationToken cancellationToken)
        {
            return _userService.GetUserByIdAsync(UserId, cancellationToken);
        }

        [HttpGet("Search/{Username}")]
        public Task<ViewUserModel> GetUserByUserName([FromRoute] string Username, CancellationToken cancellationToken)
        {
            return _userService.GetUserByUsername(Username, cancellationToken);
        }

        [HttpPut("AddUser")]
        public Task<ViewUserModel> PutNewUser([FromBody] Add_UserModel User, CancellationToken cancellationToken)
        {
            return _userService.AddUserAsync(User, cancellationToken);
        }

        [HttpDelete("Delete")]
        public Task<bool> DeleteUser([FromQuery] Property Prop, string Value, CancellationToken cancellationToken)
        {
            return _userService.DeleteUserAsync(Prop.ToString(), Value, cancellationToken);
        }
    }
}
