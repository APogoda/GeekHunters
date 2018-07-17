using System.Threading.Tasks;
using GeekHunters.Persistence;

namespace GeekHunters.Repositories.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GeekHuntersDbContext _dbContext;

        public UnitOfWork(GeekHuntersDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CommitAsync()
        {
          await _dbContext.SaveChangesAsync();
        }
    }
}