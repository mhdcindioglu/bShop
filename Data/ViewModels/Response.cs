namespace bShop.Data.ViewModels;

public class Response : Response<object> { }

public class Response<T> where T : class
{
    public Response() { }

    public List<ApiError> Errors { get; set; } = [];

    public T? Data { get; set; }

    public bool IsSuccess => Errors.Count == 0;
    public bool HasErrors(ApiErrorType type) => Errors.Exists(x => x.ApiErrorType == type);
    public string ErrorsMessage => string.Join("\r\n", Errors.Select(x => $"- {x.Message}"));

    public void AddError(ApiError error) => Errors.Add(error);
    public void AddError(ApiError[] errors) => Errors.AddRange(errors);
    public void AddError(string message) => Errors.Add(new ApiError { ApiErrorType = ApiErrorType.Failed, Message = message });
    public void AddError(ApiErrorType type = ApiErrorType.Failed, string? message = null, string? objectName = null) =>
        Errors.Add(new ApiError { ApiErrorType = type, Message = message ?? type.ToString(), ObjectName = objectName });
}

public class ApiError
{
    public ApiErrorType ApiErrorType { get; set; }
    public string? Message { get; set; }
    public string? ObjectName { get; set; }
}

public enum ApiErrorType
{
    Failed,
    NotFound,
    SerializationError,
    AddedBefore,
    SlugAddedBefore,
    MappingFailed,
    Locked,
    InvalidCode,
    ExpiredCode,
    NoEmail,
}