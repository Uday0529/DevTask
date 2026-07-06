using AutoMapper;
using DevTask2.Business.ServiceInterface;
using DevTask2.DataAdapters.DBModels;
using DevTask2.DataAdapters.IDataAdapter;
using DevTask2.Models.UserModels;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace DevTask2.Business
{
    public class UserService : IUserService
    {
        private readonly IUserDataAdapter _userDataAdapter;
        private readonly IMapper _mapper;
        public UserService(IMapper mapper, IUserDataAdapter dataAdapter) { _mapper = mapper; _userDataAdapter = dataAdapter; }

        public async Task<ViewUserModel> AddUserAsync(UserModel user, CancellationToken cancellationToken)
        {
            var mapUser = _mapper.Map<TblUser>(user);
            var tblUser = await _userDataAdapter.AddAsync(mapUser, cancellationToken);
            return _mapper.Map<ViewUserModel>(tblUser);

        }

        public async Task<bool> DeleteUserAsync(string v, string value, CancellationToken cancellationToken)
        {
            var tblUser = await _userDataAdapter.GetUserByProperty(v, value, cancellationToken);
            return await _userDataAdapter.DeleteEntity(tblUser.Id.ToString(), cancellationToken);
        }

        public async Task<IEnumerable<ViewUserModel>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            var DBUsers = await _userDataAdapter.GetAllUser(cancellationToken);
            var mapUser = _mapper.Map<IEnumerable<ViewUserModel>>(DBUsers);
            return mapUser;
        }

        public async Task<ViewUserModel?> GetUserByIdAsync(string userId, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(userId);
            string property = "Id";
            var tblUser = await _userDataAdapter.GetUserByProperty(property, userId, cancellationToken);
            var user = _mapper.Map<ViewUserModel?>(tblUser);
            if (user == null)
            {
                return null;
            }
            return user;

        }

        public async Task<ViewUserModel?> GetUserByUsername(string username, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(username);
            string property = "username";
            var tblUser = await _userDataAdapter.GetUserByProperty(property, username, cancellationToken);
            var user = _mapper.Map<ViewUserModel?>(tblUser);
            if (user == null)
            {
                return null;
            }
            return user;
        }
    }
}
