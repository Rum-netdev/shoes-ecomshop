using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShoesEShop.Core.Const.Auth;
using ShoesEShop.Core.Options.Configurations;
using ShoesEShop.Handler.Services.Interfaces;
using ShoesEShop.Handler.Shared.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShoesEShop.Handler.Services
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly JwtConfigurationOption _jwtConfig;

        public JwtAuthenticationManager(IOptions<JwtConfigurationOption> jwtConfig)
            => _jwtConfig = jwtConfig.Value;

        public string GenerateToken(AppUserDto user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = Encoding.ASCII.GetBytes(_jwtConfig.SecurityKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtConfig.Issuer,
                Audience = _jwtConfig.Audience,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(IdentityClaimConst.Email, user.Email),
                    new Claim(IdentityClaimConst.FullName, $"{user.FirstName} {user.LastName}"),
                    new Claim(IdentityClaimConst.Dob, user.Dob.ToString("ddMMyyyy")),
                    new Claim(IdentityClaimConst.PhoneNumber, user.PhoneNumber),
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(securityToken), SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.Now.AddHours(1)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
