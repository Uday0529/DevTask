namespace DevTask2.Mapping_Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(string property, string id, CancellationToken cancellationToken);
        Task<T> GetById(string id,string secondId, CancellationToken cancellationToken);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken);
        Task<bool> DeleteEntity(string id, CancellationToken cancellationToken);
        Task<T> GetValueByTwoProperty(string ptyFir, int valueF, string ptySec, string valueS, CancellationToken cancellationToken);
    }
}
