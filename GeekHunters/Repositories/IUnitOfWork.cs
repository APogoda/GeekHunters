using System.Threading.Tasks;

namespace GeekHunters.Repositories
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}