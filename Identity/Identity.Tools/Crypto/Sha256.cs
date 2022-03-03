using System.Security.Cryptography;
using System.Text;

namespace Tools.Crypto;

public static class CryptoUtils
{
    public static string CreateSHA256(this string str)
    {
        using SHA256 sha256 = SHA256.Create();
        byte[]? bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(str));
        StringBuilder stringBuilder = new();
        bytes.ToList().ForEach((b) => stringBuilder.Append(b.ToString("x2")));
        return stringBuilder.ToString();
    }
}