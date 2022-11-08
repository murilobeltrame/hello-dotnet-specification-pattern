namespace SpecificationPattern.Domain.Exceptions
{
    public class InvalidDataException : AggregateException
    {
        public override string Message => $"{base.Message}: {string.Join(", ", InnerExceptions.Select(e => e.Message))}";
    }
}
