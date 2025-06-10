namespace Orders.Application.Common
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; } = true;
        public string? Message { get; set; } = string.Empty;
        public T? Data { get; set; } = default;
        public IList<ValidationError>? Errors { get; set; } = new List<ValidationError>();


        public BaseResponse() { }

        public BaseResponse(T data, string? message = null)
        {
            Data = data;
            Message = message;
        }

        public BaseResponse(string message, List<ValidationError>? errors = null)
        {
            Message = message;
            Errors = errors ?? new List<ValidationError>();
        }

        public BaseResponse(T data, string message, List<ValidationError>? errors = null)
        {
            Data = data;
            Message = message;
            Errors = errors ?? new List<ValidationError>();
        }
    }
}
