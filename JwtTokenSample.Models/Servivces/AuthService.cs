using JwtTokenSample.Models.Entities;
using JwtTokenSample.Models.Mappers;
using JwtTokenSample.Models.Quieries;
using JwtTokenSample.Models.Repositories;
using System.Data.Common;
using Tools.Ado;

namespace JwtTokenSample.Models.Servivces
{
    public class AuthService : IAuthRepository
    {
        private readonly DbConnection _dbConnection;

        public AuthService(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Utilisateur? Execute(LoginQuery query)
        {
            using (_dbConnection)
            {
                _dbConnection.Open();
                return _dbConnection.ExecuteReader("CSP_Login", (dr) => dr.ToUtilisateur(), true, query).SingleOrDefault();
            }
        }
    }
}
