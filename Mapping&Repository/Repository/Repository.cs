using DevTask2.DataAdapters.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace DevTask2.Mapping_Repository.Repository
{
    public class Repository<T>(ApplicationDBContext context) where T : class
    {
        private readonly ApplicationDBContext _context = context;

       public async Task<IEnumerable<T>> GetAllAsync(string property, string id,CancellationToken cancellationToken)
       {
            return await _context.Set<T>().Where(t => EF.Property<int>(t, property) == Int32.Parse(id)).ToListAsync(cancellationToken);
       }

       public async Task<T> GetById(string id,string secondId, CancellationToken cancellationToken)
       {
            var result = await _context.Set<T>().Where(t => EF.Property<int>(t, "Id") == Int32.Parse(id) && 
            EF.Property<int>(t,"UserId") == Int32.Parse(secondId)).FirstOrDefaultAsync(cancellationToken);

            return result ?? throw new KeyNotFoundException($"Task with ID {id} not found for this user.");
        }
        public async Task<T> GetValueByTwoProperty(string ptyFir, int valueF, string ptySec, string valueS, CancellationToken cancellationToken)
        {
            var result =  await _context.Set<T>().Where(t => EF.Property<int>(t, ptyFir) == valueF && EF.Property<string>(t, ptySec) == valueS).FirstOrDefaultAsync(cancellationToken);
            return result ?? throw new KeyNotFoundException($"Task not found of this {ptyFir}");
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
