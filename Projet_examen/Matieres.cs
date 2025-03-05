using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_examen
{
    internal class Matieres
    {
        public int Id { get; set; }
        public string NomMatiere { get; set; }
        public virtual ICollection<Notes> note { get; set; }
        public virtual ICollection<Cours> cours { get; set; }
        public virtual ICollection<Professeurs> professeurs { get; set; }
        public Matieres()
        {
            cours = new HashSet<Cours>(); // Initialiser la collection
        }
    }
    internal class Matieresvieuw
    {
        public int Id { get; set; }
        public string NomMatiere { get; set; }
        public string cours { get; set; }


    }
}
