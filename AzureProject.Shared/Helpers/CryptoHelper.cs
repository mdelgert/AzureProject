//Nuget: https://www.nuget.org/packages/NETCore.Encrypt/
using NETCore.Encrypt;

namespace AzureProject.Shared.Helpers;

public static class CryptoHelper
{
    /// <summary>
    ///     Encrypt the given string using AES.  The string can be decrypted using
    /// </summary>
    /// <param name="plainText">The text to encrypt.</param>
    /// <param name="secret">The 32 character secret value</param>
    /// <param name="salt">The 16 character salt value</param>
    public static Task<string> EncryptAesString(string plainText, string secret, string salt)
    {
        var encrypted = EncryptProvider.AESEncrypt(plainText, secret, salt);
        return Task.FromResult(encrypted);
    }

    /// <summary>
    ///     Decrypt the given string.  Assumes the string was encrypted using
    /// </summary>
    /// <param name="cipherText">The text to decrypt.</param>
    /// <param name="secret">The 32 character secret value</param>
    /// <param name="salt">The 16 character salt value</param>
    public static Task<string> DecryptAesString(string cipherText, string secret, string salt)
    {
        var decrypted = EncryptProvider.AESDecrypt(cipherText, secret, salt);
        return Task.FromResult(decrypted);
    }
}

