using SpecificationPattern.Domain.Entities;

namespace SpecificationPattern.Domain.Specifications
{
	public class FetchRegionByFilterSpecification : BaseSpecification<Region>
	{
		public FetchRegionByFilterSpecification(
			string? name,
			string? countryName,
			string? sort,
            uint? skip,
            ushort? take)
		{
            if (!string.IsNullOrWhiteSpace(name))
                WhereExpressions.Add(w => w.Name.StartsWith(name));
            if (!string.IsNullOrWhiteSpace(countryName))
                WhereExpressions.Add(w => w.Country.Name.StartsWith(countryName));
            IncludeExpressions.Add(i => i.Country);
            Skip = skip;
            Take = take;
        }
	}
}
