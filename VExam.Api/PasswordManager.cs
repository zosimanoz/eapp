using BCrypt.Net;

namespace VExam.Api
{
    public static class PasswordManager
    {
        public static bool ValidateBcrypt(string emailAddress, string plainPassword, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(plainPassword)  || string.IsNullOrWhiteSpace(hashedPassword))
            {
                return false;
            }
          
            bool isValid = BCrypt.Net.BCrypt.Verify(emailAddress + plainPassword, hashedPassword);

            if (!isValid)
            {
                isValid = BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
            }

            return isValid;
        }

        public static string GetHashedPassword(string emailAddress, string plainPassword)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            return BCrypt.Net.BCrypt.HashPassword(emailAddress + plainPassword, salt);
        }
}
}