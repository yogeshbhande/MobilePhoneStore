using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MobilePhoneStore.Data;
using MobilePhoneStore.Interfaces;
using MobilePhoneStore.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MobilePhoneStore.Services
{
    public class LoginServices : ILoginServices
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public LoginServices(IConfiguration configuration, AppDbContext context)
        {
             _configuration = configuration;
            _context = context;
        }

        public async Task<IActionResult> LoginSubmit(string Username, string Password, ControllerBase controllerBase)
        {
            if (Username != null && Password != null)
            {
                var userData = await GetUser(Username, Password);
                var jwt = _configuration.GetSection("jwt").Get<Jwt>();
                if (userData != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        //new Claim("Id", user.UserId.ToString()),
                        new Claim("UserName", Username)
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                                      jwt.Issuer,
                                      jwt.Audience,
                                      claims,
                                      expires: DateTime.Now.AddMinutes(20),
                                      signingCredentials: signIn
                                      );
                    var Token = new JwtSecurityTokenHandler().WriteToken(token);
                    return controllerBase.Ok(new
                    {
                        Message = "Login successful",
                        token = Token
                    });
                }
                else
                {
                    return controllerBase.BadRequest(new
                    {
                        Message = "Invalid Credentials"
                    });
                }
            }
            else
            {
                return controllerBase.BadRequest(new
                {
                    Message = "Invalid Credentials"
                });

            }
        }

        public async Task<User?> GetUser(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }
    }
}
