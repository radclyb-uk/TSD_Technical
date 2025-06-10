namespace Orders.Application.Common
{
    public class ValidationException : Exception
    {
        public IEnumerable<ValidationError> Errors { get; }

        public ValidationException()
            : base("One or more validation errors have been encountered.")
        {
            Errors = new List<ValidationError>();
        }

        public ValidationException(IEnumerable<ValidationError> errors)
            : this()
        {
            Errors = errors;
        }
    }
}
