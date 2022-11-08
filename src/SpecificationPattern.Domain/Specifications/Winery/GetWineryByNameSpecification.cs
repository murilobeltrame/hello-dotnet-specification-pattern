using SpecificationPattern.Domain.Entities;

namespace SpecificationPattern.Domain.Specifications
{
    public class GetWineryByNameSpecification : BaseSpecification<Winery>
    {
        public GetWineryByNameSpecification(string wineryName)
        {
            WhereExpressions.Add(w => w.Name == wineryName);
        }
    }
}
