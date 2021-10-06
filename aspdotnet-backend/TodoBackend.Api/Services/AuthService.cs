using System;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using CryptoHelper;
using TodoBackend.Api.Data.ViewModels;
using TodoBackend.Api.Interfaces;
using System.Collections.Generic;

namespace TodoBackend.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly string _secretKey;
        private readonly int _tokenLifeSpan;
        private readonly string _issuer;
        private readonly string _audience;

        public AuthService(IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _secretKey = configuration.GetValue<string>("Auth:JWTSecretKey");
            _tokenLifeSpan = configuration.GetValue<int>("Auth:JWTLifespan");
            _issuer = configuration.GetValue<string>("Auth:Issuer");
            _audience = configuration.GetValue<string>("Auth:Audience");

            if (_secretKey == null || _tokenLifeSpan == 0)
            {
                throw new Exception("Secrets have not been setup!");
            }
        }

        public UserView RegisterUser(RegisterView registerView)
        {
            var user = _mapper.Map<UserView>(registerView);

            user.Hash = Crypto.HashPassword(registerView.Password);

            return user;
        }

        private string GenerateToken(Guid guid)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secretKey));
            var tokenDescriptor = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                expires: DateTime.UtcNow.AddMinutes(_tokenLifeSpan),
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
            var key = Encoding.ASCII.GetBytes(_secretKey);


            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public AuthDataView CreateAuthData(Guid guid)
        {
            var token = GenerateToken(guid);

            return new AuthDataView()
            {
                UniqueId = guid,
                Token = token,
                TokenExpirationTime = _tokenLifeSpan
            };
        }

        bool IAuthService.VerifyPassword(string hash, string password)
        {
            return hash == Crypto.HashPassword(password);
        }

        UserView IAuthService.UpdatePassword(string hash, string newPassword, string oldPassword)
        {
            throw new NotImplementedException();
        }
    }
}
