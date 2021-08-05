using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApi.Entities;
using WebApi.Models;
using WebApi.Utils;

namespace WebApi.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
    }

    public class UserService : IUserService
    {
        private List<User> _users = new List<User>
        {
            new User{Id = 1, Login = "Admin", Password = "!#123@456#!", Roles = "Administrador"}
        };

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _users.SingleOrDefault(x => x.Login == model.Login && x.Password == model.Password);
            if(user == null) return null;
            var timestampExpire = DateTime.UtcNow.AddMinutes(5);
            var token = GenerateJwtToken(user,timestampExpire);
            return new AuthenticateResponse(user,token,timestampExpire);
        }
      
        private string GenerateJwtToken(User user, DateTime timestampExpire)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.SecretToken);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                { 
                    new Claim("login", user.Login.ToString()),
                    new Claim("roles", user.Roles.ToString()) 
                }),
                Expires = timestampExpire,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) //criando a credencial
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}