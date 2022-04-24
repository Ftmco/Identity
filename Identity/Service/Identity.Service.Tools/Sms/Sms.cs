namespace Identity.Service.Tools.Sms;

public static class SmsExtensions
{
    public static Task SendSmsAsync(this string mobileNo, string text)
    {
        return Task.CompletedTask;
    }
}