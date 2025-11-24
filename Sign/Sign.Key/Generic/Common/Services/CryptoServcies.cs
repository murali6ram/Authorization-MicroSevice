using System.Security.Cryptography;
using System.Text;

namespace Common.Services;

public static class CryptoServices
{
    #region readonly properties
    private static readonly int PasswordSize = 16; // Password size in bytes
    private static readonly int SaltSize = 16; // Salt size in bytes
    private static readonly int Iterations = 100_000; // Iteration count
    private static readonly int KeySize = 256; // Key size in bits
    #endregion

    #region Private Methods
    private static Aes CreateAesCryptoServiceProvider(string key, string iv)
    {
        byte[] keyBytes = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(key));
        byte[] ivBytes = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(iv));

        var aes = Aes.Create();
        aes.Key = keyBytes;
        aes.IV = ivBytes;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        return aes;
    }

    private static byte[] Encrypt(this byte[] bytes, string key, string iv)
    {
        ICryptoTransform cryptoTransform = CreateAesCryptoServiceProvider(key, iv).CreateEncryptor();
        return cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
    }

    private static byte[] Decrypt(this byte[] bytes, string key, string iv)
    {
        ICryptoTransform transform = CreateAesCryptoServiceProvider(key, iv).CreateDecryptor();
        return transform.TransformFinalBlock(bytes, 0, bytes.Length);
    }
    #endregion

    public static string GetRandomString(int length)
    {
        char[] chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
        byte[] data = new byte[length];

        using var rng = RandomNumberGenerator.Create();
        rng.GetNonZeroBytes(data);

        StringBuilder result = new(length);
        foreach (byte b in data)
            result.Append(chars[b % (chars.Length)]);
        return result.ToString();
    }

    public static string Encrypt(this string plainText, string key, string iv) => Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText).Encrypt(key, iv));

    public static string Decrypt(string cipherText, string key, string iv) => Encoding.UTF8.GetString(Convert.FromBase64String(cipherText).Decrypt(key, iv));

    public static string Decrypt(string value)
    {
        byte[] combinedBytes = Convert.FromBase64String(value);

        using Aes aes = Aes.Create();
        aes.Key = DeriveKeyFromPassword(combinedBytes[..PasswordSize],
                                        combinedBytes[PasswordSize..(PasswordSize + SaltSize)]);
        aes.IV = combinedBytes[(PasswordSize + SaltSize)..(PasswordSize + SaltSize + 16)];

        return Encoding.UTF8.GetString(PerformCryptoTransform(combinedBytes[(PasswordSize + SaltSize + 16)..],
                                                              aes.CreateDecryptor()));
    }

#pragma warning disable SYSLIB0041
    private static byte[] DeriveKeyFromPassword(byte[] password, byte[] salt)
    {
        // PBKDF2
        using Rfc2898DeriveBytes rfc2898DeriveBytes = new(password,
                                                          salt,
                                                          Iterations);
        return rfc2898DeriveBytes.GetBytes(KeySize / 8);
    }
#pragma warning restore SYSLIB0041

    private static byte[] PerformCryptoTransform(byte[] input, ICryptoTransform transform)
    {
        using MemoryStream memoryStream = new();
        using CryptoStream cryptoStream = new(memoryStream,
                                              transform,
                                              CryptoStreamMode.Write);
        cryptoStream.Write(input,
                           0,
                           input.Length);
        cryptoStream.FlushFinalBlock();
        return memoryStream.ToArray();
    }
}
