using Microsoft.AspNetCore.Http;
using System.Text;

namespace Identity.Service.Tools.Crypto;

public static class Encode
{
    public static string Encrypt(this string text, string key, bool revert = false)
    {
        string newString = "";
        for (int i = 0; i < text.Length; i++)
            newString += (char)(text[i] + (revert ? key[(Math.Abs(key.Length - i) % key.Length)] : key[(i % key.Length)]));

        return newString;
    }

    public static string Decrypt(this string text, string key, bool revert = false)
    {
        string newString = "";

        for (int i = 0; i < text.Length; i++)
        {
            int code = (text[i] - (revert ? key[(Math.Abs(key.Length - i) % key.Length)] : key[(i % key.Length)]));
            newString += (char)code;
        }

        return newString;
    }

    public static string KeyMaker(this string path)
    {
        byte[]? bytes = Encoding.UTF8.GetBytes(path);
        string? base64 = Convert.ToBase64String(bytes);
        return base64.ToString();
    }

    public static string KeyMaker(this HttpContext httpContext)
        => httpContext.Request.Path.ToString().KeyMaker();
}
