namespace SpecificationPattern.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base("Record Not Found") { }
    }
}
