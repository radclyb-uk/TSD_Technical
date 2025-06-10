namespace Orders.Application.Common
{
    public class ValidationError
    {
        public string PropertyName { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        public ValidationError(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }

        public ValidationError(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public override string ToString() => string.IsNullOrWhiteSpace(PropertyName) ? ErrorMessage : $"{PropertyName}: {ErrorMessage}";
    }
}
