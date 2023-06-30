using JwtTokenSample.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtTokenSample.Models.Mappers
{
    internal static class DataRecordExtensions
    {
        internal static Utilisateur ToUtilisateur(this IDataRecord dataRecord)
        {
            return new Utilisateur(
                id:(int)dataRecord["Id"], 
                nom:(string)dataRecord["Nom"], 
                prenom:(string)dataRecord["Prenom"], 
                email:(string)dataRecord["Email"]
            );
        }
    }
}
