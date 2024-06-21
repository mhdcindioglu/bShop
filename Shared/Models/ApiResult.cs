using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace bShop.Shared.Models;
public class ApiResult : ApiResult<object> { }
public class ApiResult<T> where T : class
{
    public ApiResult() { }

    [JsonPropertyName("errors")]
    public List<ApiResultError> Errors { get; set; } = [];

    [JsonPropertyName("results")]
    public T? Results { get; set; }

    public class Meta
    {
        public class Results
        {
            public int Limit { get; set; }
            public int Skip { get; set; }
            public int Total { get; set; }
        }
    }

    [JsonPropertyName("isSuccess")]
    public bool IsSuccess => Errors.Count == 0;
    public bool HasErrors(ApiResultErrorType type) => Errors.Exists(x => x.ApiErrorType == type);
    [JsonPropertyName("errorsMessage")]
    public string ErrorsMessage => string.Join("\r\n", Errors.Select(x => $"- {x.Message}"));
    public List<string> VadiladtionErrorsMessage(string objectName) => 
        Errors.Where(x => x.ObjectName == objectName && !string.IsNullOrEmpty(x.Message)).Select(x => x.Message ?? "").ToList();

    public void AddError(ApiResultError error) => Errors.Add(error);
    public void AddError(ApiResultError[] errors) => Errors.AddRange(errors);
    public void AddError(string message) => Errors.Add(new ApiResultError { ApiErrorType = ApiResultErrorType.Failed, Message = message });
    public void AddError(ApiResultErrorType type = ApiResultErrorType.Failed, string? message = null, string? objectName = null) =>
        Errors.Add(new ApiResultError { ApiErrorType = type, Message = message ?? type.ToString(), ObjectName = objectName });
}

public class ApiResultError
{
    [JsonPropertyName("apiErrorType")]
    public ApiResultErrorType ApiErrorType { get; set; }
    [JsonPropertyName("message")]
    public string? Message { get; set; }
    [JsonPropertyName("objectName")]
    public string? ObjectName { get; set; }
}

public enum ApiResultErrorType
{
    Failed = 0,
    NotFound = -1,
    AddedBefore = -2,
    ModelStateIsNotValid = -3,
    UnAuthorized = -4,
    UserLocked = -5,
    UserNeedEmailActivation = -6,
    UserNotActive = -7,
    UserRequiresTwoFactor = -8,
    HttpFailed = -9,
    InvalidLink = -10,
    InvalidModel = -11,
    LoginFailed = -12,
}
