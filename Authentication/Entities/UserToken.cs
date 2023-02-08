using System;

namespace Authentication.Entities
{
    public class UserToken
    {
        public bool Authenticated {get; set;}
        public DateTime Expiration {get; set;}
        public string Token {get; set;}
        //public string RefreshToken {get; set;}
        public string UserName {get; set;}
        public string Action {get; set;}
        
    }
}