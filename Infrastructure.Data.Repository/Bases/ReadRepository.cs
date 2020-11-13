using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Infrastructure.Data.Context;
using Domain.Interfaces.Repositories.Bases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository.Bases
{
    public class ReadRepository<T> : IReadRepository<T> where T : class
    {
        private readonly MyContext _contexto;

        public ReadRepository(MyContext context)
        {
            _contexto = context;
        }

        public virtual ValueTask<T> FindAsync(CancellationToken cancellationToken, params object[] keys)
        {
            var entity = _contexto.Set<T>().FindAsync(keys, cancellationToken);
            return entity;
        }

        public virtual Task<List<T>> SearchAsync(ISpecification<T> specification, CancellationToken cancellationToken)
        {
            var result = specification.Prepare(_contexto.Set<T>().AsQueryable())
                .ToListAsync(cancellationToken);

            return result;
        }

        public virtual Task<List<T>> SearchAsync(ISpecification<T> specification, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = specification.Prepare(_contexto.Set<T>().AsQueryable())
                .Skip(pageNumber)
                .Take(pageSize);

            var entities = query.ToListAsync(cancellationToken);
            return entities;
        }

        public virtual IQueryable<T> Where(ISpecification<T> specification)
        {
            var query = _contexto.Set<T>()
                .Where(specification.Predicate);

            return query;
        }

        public virtual Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken)
        {
            var result = _contexto.Set<T>()
                .CountAsync(specification.Predicate, cancellationToken);

            return result;
        }

        public virtual Task<bool> AnyAsync(ISpecification<T> specification, CancellationToken cancellationToken)
        {
            var result = _contexto.Set<T>()
                .AnyAsync(specification.Predicate, cancellationToken);

            return result;
        }

        public virtual async Task<T> FirstOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken)
        {
            return await _contexto.Set<T>()
                .FirstOrDefaultAsync(specification.Predicate, cancellationToken).ConfigureAwait(false);
        }

        public virtual IQueryable<T> AsQueryable()
        {
            return _contexto.Set<T>()
                .AsQueryable();
        }

        public virtual Task<bool> AllAsync(ISpecification<T> specification, CancellationToken cancellationToken)
        {
            var result = _contexto.Set<T>()
                .AllAsync(specification.Predicate, cancellationToken);

            return result;
        }

        public string GetProviderName()
        {
            return _contexto.Database.ProviderName;
        }
    }
}