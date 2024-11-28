using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CarRentalSystem.Models;
using CarRentalSystem.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace CarRentalSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        
        private const string JwtSecretKey = "YourSuperStrongSecretKeyThatIsAtLeast32BytesLong!!";  

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> RegisterUser(User user)
        {    
            user.Password = HashPassword(user.Password);
            await _userRepository.AddUser(user);
            return user;
        }

        public async Task<string> AuthenticateUser(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email);

            if (user != null && VerifyPassword(password, user.Password))
            {
                return GenerateJwtToken(user);
            }

            return null; 
        }

        private string HashPassword(string password)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPassword(string inputPassword, string storedPassword)
        {

            return storedPassword == HashPassword(inputPassword);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(JwtSecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Audience = "https://localhost:7144",  
                Issuer = "https://localhost:7144",   
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
