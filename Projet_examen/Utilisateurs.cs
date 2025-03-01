using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_examen
{
    internal class Utilisateurs
    {

        public int Id { get; set; }
        public string NomUtilisateur { get; set; }
        public string MotDepasse { get; set; }
        public string Telephone { get; set; }
        public string Role { get; set; }
        public virtual ICollection<OTPCodes> otpcodes { get; set; }
    }
}
