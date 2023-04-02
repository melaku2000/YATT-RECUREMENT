using System.Security.Cryptography;

namespace IdentityService.Handlers
{
    public static class PasswordHasher
    {
        // 24 = 192 bits
        private const int SaltByteSize = 24;
        private const int HashByteSize = 24;
        private const int HasingIterationsCount = 10101;

        public static void GeneratePasswordHasing(string password, out byte[] salt, out byte[] hash)
        {
            salt = GenerateSalt();
            hash = ComputeHash(password, salt);
        }
        public static byte[] ComputeHash(string password, byte[] salt)
        {
            Rfc2898DeriveBytes hashGenerator = new Rfc2898DeriveBytes(password, salt);
            hashGenerator.IterationCount = HasingIterationsCount;
            return hashGenerator.GetBytes(HashByteSize);
        }

        public static byte[] GenerateSalt(int saltByteSize = SaltByteSize)
        {
            RNGCryptoServiceProvider saltGenerator = new RNGCryptoServiceProvider();
            byte[] salt = new byte[saltByteSize];
            saltGenerator.GetBytes(salt);
            return salt;
        }

        public static async Task<bool> VerifyPassword(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            byte[] computedHash = ComputeHash(password, passwordSalt);
            return await Task.FromResult(AreHashesEqual(computedHash, passwordHash));
        }

        private static bool AreHashesEqual(byte[] firstHash, byte[] secondHash)
        {
            int minHashLenght = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
            var xor = firstHash.Length ^ secondHash.Length;
            for (int i = 0; i < minHashLenght; i++)
                xor |= firstHash[i] ^ secondHash[i];
            return 0 == xor;
        }
    }
}
