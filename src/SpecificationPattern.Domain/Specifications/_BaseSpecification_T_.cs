using SpecificationPattern.Domain.Entities;
using SpecificationPattern.Domain.Interfaces;
using System.Linq.Expressions;

namespace SpecificationPattern.Domain.Specifications
{
    public abstract class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public IList<Expression<Func<T, bool>>> WhereExpressions { get; protected set; } = new List<Expression<Func<T, bool>>>();

        public IList<Expression<Func<T, object>>> IncludeExpressions { get; protected set; } = new List<Expression<Func<T, object>>>();

        public IList<Expression<Func<T, object>>> OrderByExpressions { get; protected set; } = new List<Expression<Func<T, object>>>();

        public ushort? Take { get; protected set; }

        public uint? Skip { get; protected set; }
    }
}
