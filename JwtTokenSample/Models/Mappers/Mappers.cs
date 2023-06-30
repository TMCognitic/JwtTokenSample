using JwtTokenSample.Models.Dtos;
using JwtTokenSample.Models.Entities;

namespace JwtTokenSample.Models.Mappers
{
    internal static class Mappers
    {
        internal static UtilisateurDto ToDto(this Utilisateur utilisateur, string token)
        {
            return new UtilisateurDto(utilisateur.Id, utilisateur.Nom, utilisateur.Prenom, utilisateur.Email, token);
        }
    }
}
