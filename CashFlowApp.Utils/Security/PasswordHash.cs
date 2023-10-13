namespace CashFlowApp.Utils.Security;

using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

public static class PasswordHash
{
    public static string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(128 / 8);

        return Convert.ToBase64String(
            KeyDerivation.Pbkdf2(
                password, salt,
                KeyDerivationPrf.HMACSHA256,
                100000,
                256 / 8));
    }
}