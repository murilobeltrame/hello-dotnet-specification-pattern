using SpecificationPattern.Domain.Entities;
using System.Linq.Expressions;

namespace SpecificationPattern.Domain.Interfaces
{
    public interface ISpecification<T> where T : BaseEntity
    {
        IList<Expression<Func<T, bool>>> WhereExpressions { get; }

        IList<Expression<Func<T, object>>> IncludeExpressions { get; }

        IList<Expression<Func<T, object>>> OrderByExpressions { get; }

        ushort? Take { get; }

        uint? Skip { get; }
    }
}
