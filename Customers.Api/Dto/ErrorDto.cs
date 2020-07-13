namespace Customers.Api.Dto
{
    /// <summary>
    /// Error detail
    /// </summary>
    public class ErrorDto
    {
        public string Code { get; private set; }
        public string Message { get; private set; }

        public ErrorDto(string code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        public override string ToString()
        {
            return $"{this.Code}: {this.Message}";
        }
    }

    /// <summary>
    /// Request errors
    /// </summary>
    public class ErrorResponseDto
    {
        public ErrorDto[] Errors { get; set; }
    }
}