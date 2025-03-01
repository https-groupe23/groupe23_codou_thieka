using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_examen
{
    internal class Classes
    {
        public int Id { get; set; }
        public string NomClasse { get; set; }
        public virtual ICollection<Etudiants> etudiants { get; set; }
        public virtual ICollection<Cours> cours { get; set; }
        public virtual ICollection<Professeurs> professeurs { get; set; }


    }
    internal class ViewClasses
    {
        public int Id { get; set; }
        public string NomClasse { get; set; }


    }
 
}
