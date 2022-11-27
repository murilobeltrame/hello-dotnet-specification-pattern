using SpecificationPattern.Domain.Entities;

namespace SpecificationPattern.Domain.Specifications
{
    public class GetCountryByNameSpecification : BaseSpecification<Country>
    {
        public GetCountryByNameSpecification(string countryName)
        {
            WhereExpressions.Add(c => c.Name == countryName);
        }
    }
}
