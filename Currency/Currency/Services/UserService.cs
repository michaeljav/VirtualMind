
using Currency.Helper;
using Currency.Interface;
using Currency.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Currency.Data;

namespace Currency.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly CurrencyContext _context;


        public UserService(IOptions<AppSettings> appSettings, CurrencyContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            AuthenticateResponse authenticateResponse = new AuthenticateResponse();
            try
            {                
                    var user = await _context.Users.FirstOrDefaultAsync(u => u.UseUserName == model.Username && u.UsePassword == model.Password);
                    // return null if user not found
                    if (user == null) return null;
                    // authentication successful so generate jwt token
                     var token = generateJwtToken(user);
                     authenticateResponse = new AuthenticateResponse(token);

                    return authenticateResponse;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<User> GetById(int id)
        {
            
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UseId == id);
                // return null if user not found
                if (user == null) return null;                

                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /// <summary>
        /// Generate JWT
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("UseId", user.UseId.ToString()) }),              
               // Expires = DateTime.UtcNow.AddMinutes(20), //in 20 minutes expires                
                Expires = DateTime.UtcNow.AddDays(20), //in 20 minutes expires                
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
