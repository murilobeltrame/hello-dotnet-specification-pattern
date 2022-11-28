# Specification Pattern tryout

This repo covers my take on how to implement Specification Pattern using .Net Core and Entity Framework Core.<br />
Specification Pattern is specially useful when expressing business logic throught repository manipulation. More on [Specification Pattern](https://en.wikipedia.org/wiki/Specification_pattern).<br />
I preffer to implement Specification Pattern combined with Repository Pattern, so there`s the highlights:

[Specification generic base class](src/SpecificationPattern.Application/Specifications/_BaseSpecification_T_.cs):

```cs
public abstract class BaseSpecification<TEntity, TProjection> : ISpecification<TEntity, TProjection> where TEntity : BaseEntity
{
    public IList<Expression<Func<TEntity, bool>>> WhereExpressions { get; protected set; } = new List<Expression<Func<TEntity, bool>>>();

    public IList<Expression<Func<TEntity, object>>> IncludeExpressions { get; protected set; } = new List<Expression<Func<TEntity, object>>>();

    public IList<Expression<Func<TProjection, object>>> OrderByExpressions { get; protected set; } = new List<Expression<Func<TProjection, object>>>();

    public ushort? Take { get; protected set; }

    public uint? Skip { get; protected set; }

    public Expression<Func<TEntity, TProjection>>? SelectExpression { get; protected set; }
}
```

[Repository genric base class](src/SpecificationPattern.Infra/Repositories/Repository_T_.cs):

```cs
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
```

## How to run

- Run the DB Server container:

```sh
$ ./src/scripts/db.sh
```

- Run Create Database script

```sh
$ ./src/scripts/database.sh
```

- Set user secret from "API" and "Infra.Migration" projects. To do that run the following command inside `src/SpecificationPattern.Api` **and** `src/SpecificationPattern.Infra.Migration` directory:

```sh
$ dotnet user-secrets set "ConnectionStrings:ApplicationContext" "Host=localhost;Database=WineStore;Username=postgres;Password=mysecretpassword;"
```

- Restore tools. Will restore de Entity Framework Core CLI to run migrations:

```sh
$ dotnet tool restore
```

- Run the migrations command inside `src/SpecificationPattern.Infra.Migration`directory. Will create the database schema:

```sh
$ dotnet ef database update
```

- Run the API project

## Dependencies

- [.NET 6](https://dotnet.microsoft.com/en-us/download)
- [Docker](https://docs.docker.com/get-docker/) (or [Rancher Desktop](https://rancherdesktop.io) running `dockerd`)
