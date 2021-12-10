using System.Security.Cryptography;
using System.Text;

namespace Identity.Tools.Hash;

public static class HashHelper
{
    public static string CreateSHA256(this string text)
    {
        using SHA256 sha256Hash = SHA256.Create();

        byte[]? bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text));
        StringBuilder stringBuilder = new();
        foreach (byte bt in bytes)
            stringBuilder.Append(bt.ToString("x2"));
        return stringBuilder.ToString();
    }

    public static async Task<string> CreateSHA256Async(this string text)
     => await Task.Run(() => text.CreateSHA256());
}