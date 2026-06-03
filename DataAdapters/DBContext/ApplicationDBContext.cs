using DevTask2.DataAdapters.DBModels;
using Microsoft.EntityFrameworkCore;
namespace DevTask2.DataAdapters.DBContext
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options) { }

        public DbSet<TblTask> Tasks { get; set; }
        public DbSet<TblUser> Users { get; set; }  
    }
}
