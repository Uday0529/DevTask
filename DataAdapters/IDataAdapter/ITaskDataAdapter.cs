using DevTask2.DataAdapters.DBModels;
using DevTask2.Mapping_Repository.IRepository;

namespace DevTask2.DataAdapters.IDataAdapter
{
    public interface ITaskDataAdapter : IRepository<TblTask>
    {
    }
}
