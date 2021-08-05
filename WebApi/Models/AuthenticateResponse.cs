using System;
using WebApi.Entities;

namespace WebApi.Models
{
    public class AuthenticateResponse
    {
        public string Login { get; set; }
        public bool Authenticate{get;set;}
        public string Token { get; set; }
        public string TimestampExpire { get; set; }

        public AuthenticateResponse(User user, string token, DateTime timestampExpire )
        {
            Login = user.Login;
            Token = token;
            Authenticate = true;
            TimestampExpire = timestampExpire.ToString();
        }
    }
}