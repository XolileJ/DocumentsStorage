using DocumentsStorage.Service.Interfaces;
using DocumentsStorage.Service.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace DocumentsStorage.Services
{

    public class UserService : IUserService
    {
        private const string SecurityKey = "bhvbOLH892432JKm234OH0324gsdk";
        public static readonly SymmetricSecurityKey SIGNING_KEY = new
                      SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));

        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" },
            new User { Id = 2, FirstName = "Admin", LastName = "User", Username = "admin", Password = "admin" }
        };

        public string Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = GenerateToken(username);

            return token;
        }

        public void Dispose()
        {
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        // helper methods

        private string GenerateToken(string username)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, username),
            };

            if (username == "admin")
            {
                claims.Add(new Claim("admin", username));
            }

            var token = new JwtSecurityToken(
                claims: claims,
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(DateTime.Now.AddMinutes(60)).DateTime,
                signingCredentials: new SigningCredentials(SIGNING_KEY,
                                                    SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}