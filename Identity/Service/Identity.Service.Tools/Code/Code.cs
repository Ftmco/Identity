namespace Identity.Service.Tools.Code;

public static class CodeExtension
{
    public static string CreateCode(this int length)
    {
        string newString = Guid.NewGuid().GetHashCode().ToString().Replace("-", "");
        while (newString.Length < length)
            newString += Guid.NewGuid().GetHashCode().ToString().Replace("-", "");
        return newString[0..length];
    }

    public static string CreateCode(this short length)
        => ((int)length).CreateCode();
}