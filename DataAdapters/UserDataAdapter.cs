using DevTask2.DataAdapters.DBContext;
using DevTask2.DataAdapters.DBModels;
using DevTask2.DataAdapters.IDataAdapter;
using DevTask2.Mapping_Repository.IRepository;
using DevTask2.Mapping_Repository.Repository;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace DevTask2.DataAdapters
{
    public class UserDataAdapter(ApplicationDBContext dbcontext) : Repository<TblUser>(dbcontext), IUserDataAdapter
    {
        private readonly ApplicationDBContext _dbcontext = dbcontext;


        public async Task<IEnumerable<TblUser>> GetAllUser(CancellationToken cancellationToken)
        {
            return await _dbcontext.Set<TblUser>().ToListAsync(cancellationToken);
        }

        public async Task<TblUser> GetUserByProperty(string property, string value, CancellationToken cancellationToken)
        {

            if (property == "Id")
            {
                return await _dbcontext.Set<TblUser>().Where(t => EF.Property<int>(t, property) == Int32.Parse(value)).FirstOrDefaultAsync(cancellationToken)
                       ?? throw new KeyNotFoundException(nameof(value));
            }
            if (property == "username")
            {
                return await _dbcontext.Set<TblUser>().Where(t => EF.Property<string>(t, property) == value).FirstOrDefaultAsync(cancellationToken)
                    ?? throw new KeyNotFoundException(nameof(value));
            }
            else
            {
                throw new InvalidOperationException(nameof(property));
            }
        }


    }
}
