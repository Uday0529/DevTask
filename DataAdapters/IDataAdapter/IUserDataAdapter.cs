using DevTask2.DataAdapters.DBModels;
using DevTask2.Mapping_Repository.IRepository;

namespace DevTask2.DataAdapters.IDataAdapter
{
    public interface IUserDataAdapter : IRepository<TblUser>
    {
        Task<IEnumerable<TblUser>> GetAllUser(CancellationToken cancellationToken);
        Task<TblUser> GetUserByProperty(string property, string value, CancellationToken cancellationToken);
    }
}
