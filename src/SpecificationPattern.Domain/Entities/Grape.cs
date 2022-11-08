namespace SpecificationPattern.Domain.Entities
{
    public class Grape : BaseEntity
    {
        public string Name { get; private set; }
        public GrapeColor? Color { get; private set; }

        public Grape(string name, GrapeColor? color)
        {
            Name = name;
            Color = color;
        }
    }
}