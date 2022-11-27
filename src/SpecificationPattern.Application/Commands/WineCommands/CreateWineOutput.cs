using System;
using SpecificationPattern.Domain.Entities;

namespace SpecificationPattern.Application.Commands.WineCommands
{
    public class WineOutput : CreateWineCommand
    {
        public Guid Id { get; set; }

        public static WineOutput FromEntity(Wine wine)
        {
            return new WineOutput
            {
                CountryName = wine.Region.Country.Name,
                GrapeNames = wine.Grapes.Select(grape => grape.Name),
                Id = wine.Id,
                Label = wine.Label,
                RegionName = wine.Region.Name,
                WineryName = wine.Winery.Name,
            };
        }
    }
}
