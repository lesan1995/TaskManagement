namespace TaskManagement.SharedKernel.Results
{
    public class Result
    {
        public ResultStatus Status { get; }
        public bool IsSuccess => Status == ResultStatus.Ok;
        public string? ErrorMessage { get; }
        public IEnumerable<ValidationError> ValidationErrors { get; } = Enumerable.Empty<ValidationError>();

        protected Result(ResultStatus status, string? errorMessage = null, IEnumerable<ValidationError> validationErrors = null)
        {
            Status = status;
            ErrorMessage = errorMessage;
            ValidationErrors = validationErrors;
        }

        public static Result Success() => new(ResultStatus.Ok);
        public static Result NotFound(string? message = null) => new(ResultStatus.NotFound, message ?? "Not Found");
        public static Result Invalid(IEnumerable<ValidationError> validationErrors) => new(ResultStatus.Invalid, validationErrors: validationErrors);
        public static Result Invalid(string errorMessage) => new(ResultStatus.Invalid, errorMessage);
        public static Result Error(string message) => new(ResultStatus.Error, message);
        public static Result Forbidden(string message) => new(ResultStatus.Forbidden, message);
        public static Result Unauthorize() => new(ResultStatus.Unauthorize);
        public static Result Conflict() => new(ResultStatus.Conflict);
    }

    public class Result<T> : Result
    {
        public T? Value { get; set; }

        protected Result(ResultStatus status, T? value = default, string? errorMessage = null, IEnumerable<ValidationError> validationErrors = null) : base(status, errorMessage, validationErrors)
        {
            Value = value;
        }

        public static Result<T> Success(T value) => new(ResultStatus.Ok, value);
        public static new Result<T> NotFound(string? message = null) => new(ResultStatus.NotFound, errorMessage: message ?? "Not Found");
        public static new Result<T> Invalid(IEnumerable<ValidationError> validationErrors) => new(ResultStatus.Invalid, validationErrors: validationErrors);
        public static new Result<T> Invalid(string errorMessage) => new(ResultStatus.Invalid, errorMessage: errorMessage);
        public static new Result<T> Error(string message) => new(ResultStatus.Error, errorMessage: message);
        public static new Result<T> Forbidden(string message) => new(ResultStatus.Forbidden, errorMessage: message);
        public static new Result<T> Unauthorize() => new(ResultStatus.Unauthorize);
        public static new Result<T> Conflict() => new(ResultStatus.Conflict);
    }
}
