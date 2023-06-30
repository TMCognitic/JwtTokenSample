namespace JwtTokenSample.Models.Entities
{
    public class Utilisateur
    {
        public int Id { get; init; }
        public string Nom { get; init; }
        public string Prenom { get; init; }
        public string Email { get; init; }

        internal Utilisateur(int id, string nom, string prenom, string email)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            Email = email;
        }
    }
}