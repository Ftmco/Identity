﻿using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;

namespace Identity.Service.Tools.Crypto;

public class CryptoEngine
{
    public static string Encrypt(string input, string key)
    {
        byte[] inputArray = Encoding.UTF8.GetBytes(input);
        Aes tripleDES = Aes.Create();
        tripleDES.Key = Encoding.UTF8.GetBytes(key);
        tripleDES.Mode = CipherMode.ECB;
        tripleDES.Padding = PaddingMode.PKCS7;
        ICryptoTransform cTransform = tripleDES.CreateEncryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
        tripleDES.Clear();
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    public static string Decrypt(string input, string key)
    {
        byte[] inputArray = Convert.FromBase64String(input);
        Aes tripleDES = Aes.Create();
        tripleDES.Key = Encoding.UTF8.GetBytes(key);
        tripleDES.Mode = CipherMode.ECB;
        tripleDES.Padding = PaddingMode.PKCS7;
        ICryptoTransform cTransform = tripleDES.CreateDecryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
        tripleDES.Clear();
        return Encoding.UTF8.GetString(resultArray);
    }
}