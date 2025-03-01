using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_examen
{
    internal class Cours
    {
        public int Id { get; set; }
        public string NomCours { get; set; }
        public string Description { get; set; }
        public  virtual ICollection<Classes> classes { get; set; }
        public virtual ICollection<Matieres> matieres { get; set; }


    }
}
