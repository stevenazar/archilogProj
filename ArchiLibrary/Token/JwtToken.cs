using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiLibrary.Token
{
    public static class JwtToken
    {
        private const string SECRET_KEY = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static readonly SymmetricSecurityKey SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));
        

        public static string GenerateJwtToken()
        {
            var credentials = new SigningCredentials(SIGNING_KEY, SecurityAlgorithms.HmacSha256);

            // creation of the token
            var header = new JwtHeader(credentials);

            // token will expiry after 1 minutes 
            DateTime Expiry = DateTime.UtcNow.AddMinutes(30);
            int ts = (int)(Expiry - new DateTime(1970, 1, 1)).TotalSeconds;

            // some payload that contain information about the customer
            var payload = new JwtPayload
            {
                {"sub", "ArchilogSubject" },
                {"Name", "Steven" },
                {"email", "steven.azark@outlook.fr" },
                {"exp", ts},
                {"iss", "https://localhost:44385" }, // Issuer : the party generating the JWT token
                {"aud", "https://localhost:44385" } // AUdience : the adress of the ressource 
            };

            var secToken = new JwtSecurityToken(header, payload);

            var handler = new JwtSecurityTokenHandler();

            var tokenString = handler.WriteToken(secToken);

            Console.WriteLine(tokenString);
            Console.WriteLine("consume token");

            return tokenString;


        }
    }
}
