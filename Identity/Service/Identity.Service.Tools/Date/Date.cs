using System.Globalization;

namespace Identity.Service.Tooling.Date;

public static class DateHelper
{
    public static Task<string> ToShamsiAsync(this DateTime date)
    {
        PersianCalendar pc = new();
        return Task.FromResult($"{pc.GetYear(date)}/{pc.GetMonth(date)}/{pc.GetDayOfMonth(date)}");
    }

    public static string ToShamsi(this DateTime date)
    {
        PersianCalendar pc = new();
        return $"{pc.GetYear(date)}/{pc.GetMonth(date)}/{pc.GetDayOfMonth(date)}";
    }
}