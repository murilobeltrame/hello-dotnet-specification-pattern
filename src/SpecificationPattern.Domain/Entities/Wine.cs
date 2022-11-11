namespace SpecificationPattern.Domain.Entities
{
    public class Wine : BaseEntity
    {
        public Winery Winery { get; private set; }
        public string Label { get; private set; }
        public Region Region { get; set; }
        public IEnumerable<Grape> Grapes { get; set; }

        public Wine(Winery winery, string label, Region region, IEnumerable<Grape> grapes)
        {
            Winery = winery;
            Label = label;
            Region = region;
            Grapes = grapes;
        }

        private Wine() { }
    }
}
