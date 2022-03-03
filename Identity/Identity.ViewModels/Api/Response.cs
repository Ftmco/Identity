namespace Identity.ViewModels.Api;

public record ApiModel(int Code, bool Status, string Title, string Message, object Result);

public record ApiRequest(string Data);

public static class ApiResponse
{
    public static ApiModel Success(string title, string message, object result)
        => new(200, true, title, message, result);

    public static ApiModel ApiException(string title, string message)
            => new(500, false, title, message, new { });

    public static ApiModel Faild(int code, string title, string message)
        => new(code, false, title, message, new { });
}
