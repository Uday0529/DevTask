using DevTask2.DataAdapters.DBContext;
using DevTask2.DataAdapters.DBModels;
using DevTask2.DataAdapters.IDataAdapter;
using DevTask2.Mapping_Repository.Repository;

namespace DevTask2.DataAdapters
{
    public class TaskDataAdapter(ApplicationDBContext dbContext) : Repository<TblTask>(dbContext), ITaskDataAdapter
    {
    }
}
