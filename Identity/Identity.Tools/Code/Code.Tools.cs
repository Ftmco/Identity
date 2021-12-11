namespace Identity.Tools.Code;

public static class CodeExtensions
{
    public static string CreateCode(this int length)
           => Guid.NewGuid().GetHashCode().ToString()[..length];
}