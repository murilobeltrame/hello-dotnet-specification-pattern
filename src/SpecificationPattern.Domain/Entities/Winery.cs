namespace SpecificationPattern.Domain.Entities
{
    public class Winery : BaseEntity
    {
        public string Name { get; private set; }

        public Winery(string name)
        {
            Name = name;
        }
    }
}