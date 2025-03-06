using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_examen
{
    internal class Professeurs
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public Professeurs()
        {
            matieres = new HashSet<Matieres>(); // Initialiser la collection
        }
        public virtual ICollection<Matieres> matieres { get; set; }
        public virtual ICollection<Classes> classes { get; set; }


    }
    internal class Professeurvieuw
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Matieres { get; set; }

    }
}
