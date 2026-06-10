using DevTask2.DataAdapters.DBContext;
using Microsoft.EntityFrameworkCore;

namespace DevTask2.Mapping_Repository.Repository
{
    public class Repository<T>(ApplicationDBContext context) where T : class
    {
        private readonly ApplicationDBContext _context = context;

       public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
       {
            return await _context.Set<T>().ToListAsync(cancellationToken);
       }

       public async Task<T> GetById(string id, CancellationToken cancellationToken)
       {
            return await _context.Set<T>().FindAsync(Int32.Parse(id), cancellationToken) ?? 
                throw new KeyNotFoundException(nameof(id));
       }

       public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
       {
            var createdOn = entity.GetType().GetProperty("CreatedOn");
            createdOn?.SetValue(entity, DateTime.Now);
            await _context.Set<T>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
       }

       public async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken)
       {
            var updatedOn = entity.GetType().GetProperty("UpdateAt");
            updatedOn?.SetValue(entity, DateTime.Now);
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;

       }

       public async Task<bool> DeleteEntity(string Id, CancellationToken cancellationToken)
       {
             var getEntity = await _context.Set<T>().FindAsync([Int32.Parse(Id), cancellationToken], cancellationToken) ?? throw new KeyNotFoundException(nameof(Id));
            _context.Set<T>().Remove(getEntity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;

       }

    }
}
