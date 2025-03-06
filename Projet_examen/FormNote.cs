using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Projet_examen
{
    public partial class FormNote : Form
    {
        public FormNote()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cbEtudiant.SelectedItem == null || cbMatiere.SelectedItem == null || string.IsNullOrWhiteSpace(txtNote.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                int etudiantId = (int)cbEtudiant.SelectedValue;
                int matiereId = (int)cbMatiere.SelectedValue;
                float note;

                if (!float.TryParse(txtNote.Text, out note))
                {
                    MessageBox.Show("Veuillez entrer une note valide.", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var db = new DBScolaireContext())
                {
                    var noteExistante = db.notes.FirstOrDefault(n => n.IdEtudiant == etudiantId && n.IdMatiere == matiereId);

                    if (noteExistante != null)
                    {
                        noteExistante.Note = note;
                        db.SaveChanges();
                        MessageBox.Show("La note a été mise à jour.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        var nouvelleNote = new Notes
                        {
                            IdEtudiant = etudiantId,
                            IdMatiere = matiereId,
                            Note = note
                        };

                        db.notes.Add(nouvelleNote);
                        db.SaveChanges();
                        MessageBox.Show("La note a été attribuée avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                // Rafraîchir le formulaire après ajout
                ClearFields();
                refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur: " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void FormNote_Load(object sender, EventArgs e)
        {
            refresh();
            using (var db = new DBScolaireContext())
            {
                // Charger les étudiants dans le ComboBox
                var etudiants = db.etudiants.ToList();
                cbEtudiant.DisplayMember = "Nom";
                cbEtudiant.ValueMember = "Id";
                cbEtudiant.DataSource = etudiants;

                // Charger les matières dans le ComboBox
                var matieres = db.matieres.ToList();
                cbMatiere.DisplayMember = "NomMatiere";
                cbMatiere.ValueMember = "Id";
                cbMatiere.DataSource = matieres;
            }

        }
        private void ClearFields()
        {
            txtNote.Clear();
            cbEtudiant.SelectedIndex = -1;
            cbMatiere.SelectedIndex = -1;
        }
        public void refresh()
        {
            dataGridView1.DataSource = null;
            using (var db = new DBScolaireContext())
            {
                var classes = db.classes.ToList();
                //le e represente une ligne de la base de donnees

                dataGridView1.DataSource = db.notes.Select(e => new Noteview
                {
                    Id = e.Id,
                    Etudiant = e.etudiant.Nom,
                    Matiere = e.matiere.NomMatiere,
                    Note = e.Note,
                }).ToList();
            }
        }
    }
}
