using TechLibrary.Api.Domain.Entities;

namespace TechLibrary.Api.Infrastructure.Security.Cryptography
{
    public class BCryptAlgorithm
    { 
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // avoiding use of two parameters of the same type, instead of using "string hashPassword" to
        // verify, you can send User and then get the password hash', it'll prevent confusion with the parameters order
        public bool VerifyPassword(string password, User user)
        {
            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }
    }
}
