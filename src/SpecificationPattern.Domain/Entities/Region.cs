namespace SpecificationPattern.Domain.Entities
{
    public class Region : BaseEntity
    {
        public string Name { get; private set; }
        public Country Country { get; private set; }

        public Region(string name, Country country)
        {
            Name = name;
            Country = country;
        }

        private Region() { }
    }
}