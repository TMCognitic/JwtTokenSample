using JwtTokenSample.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Queries;

namespace JwtTokenSample.Models.Quieries
{
    public class LoginQuery : IQueryDefinition<Utilisateur?>
    {
        public string Email { get; init; }
        public string Passwd { get; init; }

        public LoginQuery(string email, string passwd)
        {
            Email = email;
            Passwd = passwd;
        }
    }
}
