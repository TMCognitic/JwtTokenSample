namespace JwtTokenSample.Models.Dtos
{
    public class UtilisateurDto
    {
        public int Id { get; init; }
        public string Nom { get; init; }
        public string Prenom { get; init; }
        public string Email { get; init; }
        public string Token { get; init; }

        internal UtilisateurDto(int id, string nom, string prenom, string email, string token)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            Email = email;
            Token = token;
        }
    }
}
