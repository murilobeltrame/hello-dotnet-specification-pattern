namespace SpecificationPattern.Application.Queries
{
	public class WinesQuery
	{
        public uint? Page { get; set; } = 0;
        public ushort? Size { get; set; } = 10;
        public string? Sort { get; }
        public string? WineryName { get; }
        public string? Label { get; }
        public string? GrapeName { get; }
        public string? RegionName { get; }
        public string? CountryName { get; }
    }
}
