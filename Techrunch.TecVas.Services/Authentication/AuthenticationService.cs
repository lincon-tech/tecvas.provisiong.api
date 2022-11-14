﻿using BCrypt.Net;
using Techrunch.TecVas.Data;
using Techrunch.TecVas.Entities;
using Techrunch.TecVas.Entities.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BCrypt;
using Newtonsoft.Json.Linq;

namespace Techrunch.TecVas.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IRepository<User> _userRepo;
        private readonly IConfiguration _config;
        private const int SaltSize = 32;
        public AuthenticationService(ILogger<AuthenticationService> logger,
        IConfiguration configuration,
        IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userRepo = unitOfWork.GetRepository<User>();
            _config = configuration;
        }
        public User Authenticate(UserLogin userLogin)
        {

            var userObj = _userRepo.GetQueryable()
                .Where(a => a.EmailAddress.ToLower() == userLogin.Username.ToLower() && a.IsActive == true).FirstOrDefault();

            //_logger.LogInformation($"Get user info from Database : {userObj}");
            if (userObj != null)
            {
                return userObj;
            }
            //if (userObj != null) 
            //{
            //    //string salt = userObj.PasswordSalt;
            //    //var inputpasswordHash = Convert.ToBase64String(ComputeHMAC_SHA256(Encoding.UTF8.GetBytes(userLogin.Password), Encoding.UTF8.GetBytes(salt)));
            //    //var userpasswordhash = userObj.Password;
            //    var isveriified = BCrypt.Net.BCrypt.Verify(userLogin.Password, userObj.Password);
            //    return userObj;
            //}
            return null;

        }
        public string GenerateToken(User userModel)
        {
            string secret = _config.GetSection("JWT:Secret").Value;
            string issuer = _config.GetSection("JWT:ValidIssuer").Value;
            string audience = _config.GetSection("JWT:ValidAudience").Value;
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userModel.Username),
                new Claim(ClaimTypes.Email, userModel.EmailAddress),
                new Claim(ClaimTypes.GivenName, userModel.Username),
                new Claim(ClaimTypes.Surname, userModel.Lastname),
                //new Claim(ClaimTypes.Role, userModel.Role)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            //var token = new JwtSecurityToken(issuer,
            //    audience,
            //    claims,
            //    expires: DateTime.Now.AddMinutes(15),
            //    signingCredentials: credentials);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                     new Claim(ClaimTypes.NameIdentifier, userModel.Username),
                    new Claim(ClaimTypes.Email, userModel.EmailAddress),
                    new Claim(ClaimTypes.GivenName, userModel.Username),
                    new Claim(ClaimTypes.Surname, userModel.Lastname),
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

            //return new Tokens { Token = tokenHandler.WriteToken(token) };

            //return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public static byte[] GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[SaltSize];

                rng.GetBytes(randomNumber);

                return randomNumber;

            }
        }
        public static byte[] ComputeHMAC_SHA256(byte[] data, byte[] salt)
        {
            using (var hmac = new HMACSHA256(salt))
            {
                return hmac.ComputeHash(data);
            }
        }
    }
}
