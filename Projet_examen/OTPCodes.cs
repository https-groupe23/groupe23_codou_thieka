 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_examen
{
    internal class OTPCodes
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime DateExpiration { get; set; }
        public int IdUtilisateur { get; set; }
        public virtual Utilisateurs utilisateur { get; set; }
    }
}
