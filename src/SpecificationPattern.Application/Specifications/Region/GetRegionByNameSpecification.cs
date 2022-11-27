using SpecificationPattern.Domain.Entities;

namespace SpecificationPattern.Domain.Specifications
{
    public class GetRegionByNameSpecification : BaseSpecification<Region>
    {
        public GetRegionByNameSpecification(string regionName)
        {
            WhereExpressions.Add(r => r.Name == regionName);
        }
    }
}
