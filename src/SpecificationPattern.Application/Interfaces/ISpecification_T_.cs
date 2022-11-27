using SpecificationPattern.Domain.Entities;
using System.Linq.Expressions;

namespace SpecificationPattern.Application.Interfaces
{
    public interface ISpecification<T>: ISpecification<T, T> where T : BaseEntity { }

    public interface ISpecification<TEntity, TProjection> where TEntity : BaseEntity
    {
        IList<Expression<Func<TEntity, bool>>> WhereExpressions { get; }

        IList<Expression<Func<TEntity, object>>> IncludeExpressions { get; }

        IList<Expression<Func<TProjection, object>>> OrderByExpressions { get; }

        Expression<Func<TEntity, TProjection>>? SelectExpression { get; }

        ushort? Take { get; }

        uint? Skip { get; }
    }
}
