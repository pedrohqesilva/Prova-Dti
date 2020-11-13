using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories.Bases
{
    public interface IReadRepository<T>
    {
        ValueTask<T> FindAsync(CancellationToken cancellationToken, params object[] keys);

        Task<List<T>> SearchAsync(ISpecification<T> specification, CancellationToken cancellationToken);

        Task<List<T>> SearchAsync(ISpecification<T> specification, int pageNumber, int pageSize, CancellationToken cancellationToken);

        IQueryable<T> Where(ISpecification<T> specification);

        Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken);

        Task<bool> AnyAsync(ISpecification<T> specification, CancellationToken cancellationToken);

        Task<T> FirstOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken);

        IQueryable<T> AsQueryable();

        Task<bool> AllAsync(ISpecification<T> specification, CancellationToken cancellationToken);

        string GetProviderName();
    }
}