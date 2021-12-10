namespace Identity.ViewModels.Api;

public record ApiResponse(bool Status, short Code, string? Title, string? message, object Result)
{

    public static ApiResponse Success(string title, string? message, object result)
        => new(true, 200, title, message, result);

    public static ApiResponse Excetpion(string? title, string? message)
        => new(false, 500, title, message, new { });

    public static ApiResponse Notfound(string? title, string? message)
        => new(false, 404, title, message, new { });

    public static ApiResponse AccessDenied(string? title, string? message)
        => new(false, 403, title, message, new { });
}
