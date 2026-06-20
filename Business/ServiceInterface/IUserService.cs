using DevTask2.Models.UserModels;

namespace DevTask2.Business.ServiceInterface
{
    public interface IUserService
    {
        Task<ViewUserModel> AddUserAsync(Add_UserModel user, CancellationToken cancellationToken);
        Task<bool> DeleteUserAsync(string v, string value, CancellationToken cancellationToken);
        Task<IEnumerable<ViewUserModel>> GetAllUsersAsync(CancellationToken cancellationToken);
        Task<ViewUserModel> GetUserByIdAsync(string userId, CancellationToken cancellationToken);
        Task<ViewUserModel> GetUserByUsername(string username, CancellationToken cancellationToken);
    }
}
