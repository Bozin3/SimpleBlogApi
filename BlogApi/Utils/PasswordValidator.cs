using BlogApi.Data.Entities;
using System.Security.Cryptography;
using System.Text;

namespace BlogApi.Utils
{
    public static class PasswordValidator
    {
        public static bool CheckValidPassword(string providedPass, User user)
        {
            using (HMACSHA512 hmac = new HMACSHA512(user.PasswordSalt))
            {
                byte[] passwordbytes = Encoding.ASCII.GetBytes(providedPass);
                var computedHash = hmac.ComputeHash(passwordbytes);
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != user.PasswordHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (HMACSHA512 hmac = new HMACSHA512())
            {
                byte[] passwordbytes = Encoding.ASCII.GetBytes(password);
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(passwordbytes);
            }
        }

    }
}
