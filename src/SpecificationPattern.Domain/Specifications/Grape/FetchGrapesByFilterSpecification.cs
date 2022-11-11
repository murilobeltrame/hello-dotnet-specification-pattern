using System.Reflection.Emit;
using SpecificationPattern.Domain.Entities;

namespace SpecificationPattern.Domain.Specifications
{
    public class FetchGrapesByFilterSpecification:BaseSpecification<Grape>
	{
		public FetchGrapesByFilterSpecification(
			string? name,
			string? colorName,
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
