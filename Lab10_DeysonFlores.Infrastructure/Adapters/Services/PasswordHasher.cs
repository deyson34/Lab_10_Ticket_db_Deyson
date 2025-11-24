// filepath: e:\PROYECTOS RIDER\Lab10_DeysonFlores\Lab10_DeysonFlores.Aplication\Services\PasswordHasher.cs
using System;
using System.Security.Cryptography;

namespace Lab10_DeysonFlores.Application.Services;

public class PasswordHasher : IPasswordHasher
{
    private const int Iterations = 100_000;
    private const int SaltSize = 16; // bytes
    private const int KeySize = 32; // bytes

    public string Hash(string password)
    {
        using var rng = RandomNumberGenerator.Create();
        var salt = new byte[SaltSize];
        rng.GetBytes(salt);

        using var deriveBytes = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
        var key = deriveBytes.GetBytes(KeySize);

        var parts = new byte[1 + SaltSize + KeySize];
        parts[0] = 0; // version
        Buffer.BlockCopy(salt, 0, parts, 1, SaltSize);
        Buffer.BlockCopy(key, 0, parts, 1 + SaltSize, KeySize);

        return Convert.ToBase64String(parts);
    }

    public bool Verify(string hash, string password)
    {
        if (string.IsNullOrEmpty(hash)) return false;
        try
        {
            var parts = Convert.FromBase64String(hash);
            if (parts.Length != 1 + SaltSize + KeySize) return false;
            var salt = new byte[SaltSize];
            Buffer.BlockCopy(parts, 1, salt, 0, SaltSize);
            var key = new byte[KeySize];
            Buffer.BlockCopy(parts, 1 + SaltSize, key, 0, KeySize);

            using var deriveBytes = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            var candidate = deriveBytes.GetBytes(KeySize);

            return CryptographicOperations.FixedTimeEquals(candidate, key);
        }
        catch
        {
            return false;
        }
    }
}

