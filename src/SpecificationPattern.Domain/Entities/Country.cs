namespace SpecificationPattern.Domain.Entities
{
    public class Country : BaseEntity
    {
        public string Name { get; private set; }

        public Country(string name)
        {
            Name = name;
        }

        private Country() { }
    }
}