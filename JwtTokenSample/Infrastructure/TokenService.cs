using JwtTokenSample.Models.Dtos;
using JwtTokenSample.Models.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtTokenSample.Infrastructure
{
    public class TokenService : ITokenRepository
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(Utilisateur utilisateur)
        {
            //Récupère les entrées du appsettings.json
            string issuer = _configuration["Jwt:Issuer"];
            string audience = _configuration["Jwt:Audience"];
            byte[] key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            //Construction du Payload
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", utilisateur.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.FamilyName, utilisateur.Nom),
                    new Claim(JwtRegisteredClaimNames.GivenName, utilisateur.Prenom),
                    new Claim(JwtRegisteredClaimNames.Email, utilisateur.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, "GestContactApi_Cqs"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                                             SecurityAlgorithms.HmacSha512Signature)
            };
            //Génération du token
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken securityToken = (JwtSecurityToken)tokenHandler.CreateToken(tokenDescriptor);            
            return tokenHandler.WriteToken(securityToken);
        }

        public UtilisateurDto GetUtilisateur(string token)
        {
            if(token.StartsWith("Bearer "))
                token = token.Replace("Bearer ", "");

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
            JwtPayload payload = securityToken.Payload;

            return new UtilisateurDto(
                id: int.Parse(payload["Id"].ToString()!),
                nom: payload[JwtRegisteredClaimNames.FamilyName].ToString()!,
                prenom: payload[JwtRegisteredClaimNames.GivenName].ToString()!,
                email: payload[JwtRegisteredClaimNames.Email].ToString()!,
                token: token);
        }
    }
}
