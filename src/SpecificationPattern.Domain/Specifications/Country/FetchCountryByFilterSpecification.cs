using SpecificationPattern.Domain.Entities;

namespace SpecificationPattern.Domain.Specifications
{
    public class FetchCountryByFilterSpecification: BaseSpecification<Country>
	{
		public FetchCountryByFilterSpecification(
			string? name,
            string? sort,
            uint? skip,
        ushort? take)
		{
            if (!string.IsNullOrWhiteSpace(name))
                WhereExpressions.Add(w => w.Name.StartsWith(name));

            Skip = skip;
            Take = take;
        }
	}
}

