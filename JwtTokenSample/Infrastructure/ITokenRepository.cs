using JwtTokenSample.Models.Dtos;
using JwtTokenSample.Models.Entities;

namespace JwtTokenSample.Infrastructure
{
    public interface ITokenRepository
    {
        public string GenerateToken(Utilisateur utilisateur);
        public UtilisateurDto GetUtilisateur(string token);
    }
}
