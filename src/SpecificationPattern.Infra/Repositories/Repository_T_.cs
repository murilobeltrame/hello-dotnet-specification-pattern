using Microsoft.EntityFrameworkCore;
using SpecificationPattern.Application.Interfaces;
using SpecificationPattern.Domain.Entities;
using SpecificationPattern.Domain.Exceptions;
using SpecificationPattern.Domain.Specifications;

namespace SpecificationPattern.Infra.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationContext _db;

        public Repository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<TEntity> CreateAsync(TEntity record, CancellationToken cancellationToken = default) =>
            (await _db.Set<TEntity>().AddAsync(record, cancellationToken)).Entity;

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entitity = await GetAsync(new GetByIdSpecification<TEntity>(id), cancellationToken);
            _db.Set<TEntity>().Remove(entitity);
        }

        public async Task<bool> ExistsAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default) =>
             (await GetAsync(specification, cancellationToken)) != null;

        public async Task<TProjection> GetAsync<TProjection>(ISpecification<TEntity, TProjection> specification, CancellationToken cancellationToken = default)
        {
            var query = ProcessQuery(specification);

            var result = await query.FirstOrDefaultAsync(cancellationToken);
            if (result == null) throw new NotFoundException();
            return result;
        }

        public async Task<IEnumerable<TProjection>> FetchAsync<TProjection>(ISpecification<TEntity, TProjection> specification, CancellationToken cancellationToken = default)
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

        public Task UpdateAsync(TEntity record, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
            await _db.SaveChangesAsync(cancellationToken);

        private IQueryable<TProjection> ProcessQuery<TProjection>(ISpecification<TEntity, TProjection> specification)
        {
            IQueryable<TProjection> projectedQuery;
            var query = _db.Set<TEntity>().AsQueryable();

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

            if (specification.SelectExpression != null)
            {
                projectedQuery = query.Select(specification.SelectExpression);
            } else
            {
                projectedQuery = (IQueryable<TProjection>)query;
            }

            if (specification.OrderByExpressions.Any())
            {
                for (int i = 0; i < specification.OrderByExpressions.Count(); i++)
                {
                    var orderByExpression = specification.OrderByExpressions.ElementAt(i);
                    if (i == 0) projectedQuery = projectedQuery.OrderBy(orderByExpression);
                    else projectedQuery = ((IOrderedQueryable<TProjection>)projectedQuery).ThenBy(orderByExpression);
                }
            }

            return projectedQuery;
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
