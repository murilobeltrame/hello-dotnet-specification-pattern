using System;

namespace SpecificationPattern.Application.Commands.WineCommands
{
    public class CreateWineCommand
    {
        public string WineryName { get; set; }
        public string Label { get; set; }
        public string RegionName { get; set; }
        public string CountryName { get; set; }
        public IEnumerable<string> GrapeNames { get; set; }
    }
}

