namespace TechLibrary.Api.Infrastructure.Security.Cryptography
{
    public class BCryptAlgorithm
    { 
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
