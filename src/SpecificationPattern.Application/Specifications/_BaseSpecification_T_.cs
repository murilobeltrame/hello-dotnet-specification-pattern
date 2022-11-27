using SpecificationPattern.Application.Interfaces;
using SpecificationPattern.Domain.Entities;
using System.Linq.Expressions;

namespace SpecificationPattern.Domain.Specifications
{
    public abstract class BaseSpecification<T> : BaseSpecification<T,T>, ISpecification<T> where T : BaseEntity { }

    public abstract class BaseSpecification<TEntity, TProjection> : ISpecification<TEntity, TProjection> where TEntity : BaseEntity
    {
        public IList<Expression<Func<TEntity, bool>>> WhereExpressions { get; protected set; } = new List<Expression<Func<TEntity, bool>>>();

        public IList<Expression<Func<TEntity, object>>> IncludeExpressions { get; protected set; } = new List<Expression<Func<TEntity, object>>>();

        public IList<Expression<Func<TProjection, object>>> OrderByExpressions { get; protected set; } = new List<Expression<Func<TProjection, object>>>();

        public ushort? Take { get; protected set; }

        public uint? Skip { get; protected set; }

        public Expression<Func<TEntity, TProjection>>? SelectExpression { get; protected set; }
    }
}
