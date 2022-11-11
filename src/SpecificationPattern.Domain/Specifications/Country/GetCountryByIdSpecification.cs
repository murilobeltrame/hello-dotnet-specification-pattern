using SpecificationPattern.Domain.Entities;

namespace SpecificationPattern.Domain.Specifications
{
    public class GetCountryByIdSpecification : BaseSpecification<Country>
	{
		public GetCountryByIdSpecification(Guid id)
		{
			WhereExpressions.Add(c => c.Id == id);
		}
	}
}
