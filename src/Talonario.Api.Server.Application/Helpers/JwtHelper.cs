using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Talonario.Api.Server.Application.Entities;

namespace Talonario.Api.Server.Application.Helpers
{
    public class JwtHelper
    {
        #region Private Fields

        private static IConfiguration _config;

        #endregion Private Fields

        #region Public Constructors

        public JwtHelper()
        {
            _config = ConfigHelper.Load();
        }

        #endregion Public Constructors

        #region Public Methods

        public string GenerateJwtTokenByHours(UsuarioEntity user, int? hours = 24)
        {
            var claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString(), ClaimValueTypes.Integer),
                new Claim("usuario", user.Usuario, ClaimValueTypes.String),
                new Claim("cpf", user.CPF, ClaimValueTypes.String),
                new Claim("senha", user.Senha, ClaimValueTypes.String),
                new Claim("email", user.Email.ToUpper() , ClaimValueTypes.String)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt").GetSection("SecurityKey").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            DateTime expires = DateTime.Now.AddHours(hours.Value);

            var token = new JwtSecurityToken(
                issuer: _config.GetSection("Jwt").GetSection("Issuer").Value,
                audience: _config.GetSection("Jwt").GetSection("Audience").Value,
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );

            var hashToken = new JwtSecurityTokenHandler().WriteToken(token);
            return hashToken;
        }

        #endregion Public Methods
    }
}