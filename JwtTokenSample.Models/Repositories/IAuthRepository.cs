using JwtTokenSample.Models.Entities;
using JwtTokenSample.Models.Quieries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Queries;

namespace JwtTokenSample.Models.Repositories
{
    public interface IAuthRepository :
        IQueryHandler<LoginQuery, Utilisateur?>
    {
    }
}
