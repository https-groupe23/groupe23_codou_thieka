using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_examen
{
    internal class Notes
    {
        public int Id { get; set; }
        public int IdMatiere { get; set; }
        public int IdEtudiant { get; set; }
        public float Note { get; set; }
        public virtual Etudiants etudiant { get; set; }
        public virtual Matieres matiere { get; set; }
    }
}
