using Core.Entities.Concrete;
using Core.Utilities.Security.Encyption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Core.Extensions;
using System.Linq;

namespace Core.Utilities.Security.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration _configuration { get; }
        private TokenOptions _tokenOptions;
        DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }
        public AccessToken CreateToken(AppUser user, List<AppRole> userRoles)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SignignCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, userRoles);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                ExpireDate = _accessTokenExpiration,
            };
        }
        public AccessToken CreateToken(AppUser user)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SignignCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                ExpireDate = _accessTokenExpiration,
            };
        }
        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, AppUser user, SigningCredentials signignCredentials, List<AppRole> userRoles)
        {
            var jwt = new JwtSecurityToken
                (
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, userRoles),
                signingCredentials: signignCredentials
                );

            return jwt;
        }
        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, AppUser user, SigningCredentials signignCredentials)
        {
            var jwt = new JwtSecurityToken
                (
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user),
                signingCredentials: signignCredentials
                );

            return jwt;
        }
        private IEnumerable<Claim> SetClaims(AppUser user)
        {
            var claims = new List<Claim>();
            claims.AddEmail(user.Email);
            //claims.AddName(user.Name);
            claims.AddIdentifier(user.Id.ToString());

            return claims;
        }
        private IEnumerable<Claim> SetClaims(AppUser user, List<AppRole> userRoles)
        {
            var claims = new List<Claim>();
            claims.AddEmail(user.Email);
            claims.AddName(user.Name);
            claims.AddIdentifier(user.Id.ToString());
            claims.AddRoles(userRoles.Select(t => t.Name).ToArray());

            return claims;
        }
    }
}
