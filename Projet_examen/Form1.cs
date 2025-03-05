using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet_examen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void etToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCours cours = new FormCours();
            cours.Show();
        }

        private void classesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormClasse classes = new FormClasse();
            classes.Show();
            classes.MdiParent = this;

        }

        private void coursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEtudiant etudiant = new FormEtudiant();
            etudiant.Show();
            etudiant.MdiParent = this;

        }

        private void matieresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMatiere formMatiere = new FormMatiere();
            formMatiere.Show();
            formMatiere.MdiParent = this;
        }

        private void noteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormNote note = new FormNote();
            note.Show();
            note.MdiParent = this;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
          
        }

        private void professeursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormProfesseur formProfesseur = new FormProfesseur();
            formProfesseur.Show();
            formProfesseur.MdiParent = this;

        }

        private void utilisateursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUtilisateur formUtilisateur = new FormUtilisateur();
            formUtilisateur.Show();
            formUtilisateur.MdiParent = this;
        }

        private void oTPcodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOTPcode formOTPcode = new FormOTPcode();
            formOTPcode.Show();
            formOTPcode.MdiParent = this;
        }
    }
}
