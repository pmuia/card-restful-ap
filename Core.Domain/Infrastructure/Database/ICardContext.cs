

namespace Core.Domain.Infrastructure.Database
{
    public interface ICardContext
    {
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
