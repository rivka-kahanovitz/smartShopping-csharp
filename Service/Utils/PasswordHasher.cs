using System.Security.Cryptography;
using System.Text;


public static class PasswordHasher
{
    public static string Hash(string input)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToHexString(hash); // מחזיר מחרוזת מוצפנת
    }
}
