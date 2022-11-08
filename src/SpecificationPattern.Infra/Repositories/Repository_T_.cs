using Microsoft.EntityFrameworkCore;
using SpecificationPattern.Domain.Entities;
using SpecificationPattern.Domain.Exceptions;
using SpecificationPattern.Domain.Interfaces;
using SpecificationPattern.Domain.Specifications;

namespace SpecificationPattern.Infra.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationContext _db;

        public Repository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<T> CreateAsync(T record, CancellationToken cancellationToken = default) =>
            (await _db.Set<T>().AddAsync(record, cancellationToken)).Entity;

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entitity = await GetAsync(new GetByIdSpecification<T>(id), cancellationToken);
            _db.Set<T>().Remove(entitity);
        }

        public async Task<bool> ExistsAsync(ISpecification<T> specification, CancellationToken cancellationToken) =>
             (await GetAsync(specification, cancellationToken)) != null;

        public async Task<T> GetAsync(ISpecification<T> specification, CancellationToken cancellationToken)
        {
            var query = ProcessQuery(specification);

            var result = await query.FirstOrDefaultAsync(cancellationToken);
            if (result == null) throw new NotFoundException();
            return result;
        }

        public async Task<IEnumerable<T>> FetchAsync(ISpecification<T> specification, CancellationToken cancellationToken)
        {
            var query = ProcessQuery(specification);
            if (specification.Skip.HasValue)
            {
                query = query.Skip((int)specification.Skip.Value);
            }
            if (specification.Take.HasValue)
            {
                query = query.Take(specification.Take.Value);
            }
            return await query.ToListAsync(cancellationToken);
        }

        public Task UpdateAsync(T record, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private IQueryable<T> ProcessQuery(ISpecification<T> specification)
        {
            var query = _db.Set<T>().AsNoTracking();

            if (specification.WhereExpressions.Any())
            {
                foreach (var whereExpression in specification.WhereExpressions)
                {
                    query = query.Where(whereExpression);
                }
            }

            if (specification.IncludeExpressions.Any())
            {
                foreach (var includeExpression in specification.IncludeExpressions)
                {
                    query = query.Include(includeExpression);
                }
            }

            if (specification.OrderByExpressions.Any())
            {
                for (int i = 0; i < specification.OrderByExpressions.Count(); i++)
                {
                    var orderByExpression = specification.OrderByExpressions.ElementAt(i);
                    if (i == 0) query = query.OrderBy(orderByExpression);
                    else query = ((IOrderedQueryable<T>)query).ThenBy(orderByExpression);
                }
            }

            return query;
        }
    }

    class GetByIdSpecification<T> : BaseSpecification<T> where T : BaseEntity
    {
        public GetByIdSpecification(Guid id)
        {
            WhereExpressions.Add(w => w.Id == id);
        }
    }
}
