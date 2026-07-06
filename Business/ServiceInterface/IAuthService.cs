using DevTask2.Models.UserModels;

namespace DevTask2.Business.ServiceInterface
{
    public interface IAuthService
    {
        Task<bool> RegisterUser(RegisterAuthModel addUser, CancellationToken cancellationToken);
        Task<string?> LoginUserAsync(UserAuthModel user, CancellationToken cancellationToken);
    }
}
