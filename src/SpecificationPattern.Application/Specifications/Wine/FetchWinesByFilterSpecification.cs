﻿using SpecificationPattern.Application.Commands.WineCommands;
using SpecificationPattern.Domain.Entities;

namespace SpecificationPattern.Domain.Specifications
{
    public class FetchWinesByFilterSpecification : BaseSpecification<Wine, WineOutput>
    {
        public FetchWinesByFilterSpecification(
            string? wineryName,
            string? label,
            string? grapeName,
            string? regionName,
            string? countryName,
            string? sort,
            uint? skip,
            ushort? take)
        {
            if (!string.IsNullOrWhiteSpace(wineryName))
                WhereExpressions.Add(w => w.Winery.Name.StartsWith(wineryName));
            if (!string.IsNullOrWhiteSpace(label))
                WhereExpressions.Add(w => w.Label.StartsWith(label));
            if (!string.IsNullOrWhiteSpace(grapeName))
                WhereExpressions.Add(w => w.Grapes.Any(g => g.Name.StartsWith(grapeName)));
            if (!string.IsNullOrEmpty(regionName))
                WhereExpressions.Add(w => w.Region.Name.StartsWith(regionName));
            if (!string.IsNullOrEmpty(countryName))
                WhereExpressions.Add(w => w.Region.Country.Name.StartsWith(countryName));

            
            IncludeExpressions.Add(i => i.Grapes);
            IncludeExpressions.Add(i => i.Region.Country);
            IncludeExpressions.Add(i => i.Winery);

            Skip = skip;
            Take = take;

            SelectExpression = wine => WineOutput.FromEntity(wine);
        }
    }
}
