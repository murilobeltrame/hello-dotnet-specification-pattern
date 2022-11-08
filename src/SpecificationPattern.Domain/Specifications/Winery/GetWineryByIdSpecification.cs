using SpecificationPattern.Domain.Entities;

namespace SpecificationPattern.Domain.Specifications
{
    public class GetWineryByIdSpecification : BaseSpecification<Winery>
    {
        public GetWineryByIdSpecification(Guid id)
        {
            WhereExpressions.Add(w => w.Id == id);
        }
    }
}
