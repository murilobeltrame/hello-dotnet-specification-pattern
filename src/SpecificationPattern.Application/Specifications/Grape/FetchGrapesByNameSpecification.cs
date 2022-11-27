using SpecificationPattern.Domain.Entities;

namespace SpecificationPattern.Domain.Specifications
{
    public class FetchGrapesByNameSpecification : BaseSpecification<Grape>
    {
        public FetchGrapesByNameSpecification(IEnumerable<string> grapeNames)
        {
            WhereExpressions.Add(g => grapeNames.Any(n => g.Name == n));
        }
    }
}
