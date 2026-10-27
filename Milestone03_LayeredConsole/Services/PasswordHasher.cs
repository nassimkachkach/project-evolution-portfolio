using System.Security.Cryptography;
using System.Text;

namespace Milestone03_LayeredConsole.Services;

// Turns a password into a SHA-256 hash so the real password is never saved.
// (Real applications use slower algorithms like BCrypt, but this is enough for learning.)
public static class PasswordHasher
{
    public static string Hash(string password)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(bytes);
    }

    public static bool Verify(string password, string hash)
    {
        return Hash(password) == hash;
    }
}
