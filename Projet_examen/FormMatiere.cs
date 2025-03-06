using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace Projet_examen
{
    public partial class FormMatiere : Form
    {
        public FormMatiere()
        {
            InitializeComponent();
            refresh();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNom.Text) || clbCours.CheckedItems.Count == 0)
            {
                MessageBox.Show("Veuillez saisir toutes les informations et sélectionner au moins un cours.", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    using (var db = new DBScolaireContext())
                    {
                        // Vérifier si la matière existe déjà
                        Matieres matiere = db.matieres.FirstOrDefault(m => m.NomMatiere == txtNom.Text);

                        if (matiere == null) // Si la matière n'existe pas, on la crée
                        {
                            matiere = new Matieres
                            {
                                NomMatiere = txtNom.Text,
                            };

                            db.matieres.Add(matiere);
                            db.SaveChanges();
                        }

                        // Ajouter les cours sélectionnés à la matière
                        foreach (var item in clbCours.CheckedItems)
                        {
                            Cours cours = item as Cours;
                            if (cours != null && !matiere.cours.Contains(cours))
                            {
                                matiere.cours.Add(cours);
                            }
                        }

                        db.SaveChanges();

                        // Afficher un message de confirmation après l'ajout
                        MessageBox.Show("Matière et cours associés ajoutés avec succès!", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Rafraîchir l'affichage
                        refresh();
                        clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur: " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        public void refresh()
        {
            dataGridView1.DataSource = null;
            using (var db = new DBScolaireContext())
            {
                var matiereList = db.matieres
                    .Include(m => m.cours) // Charger les cours associés
                    .ToList() // Charger les données en mémoire
                    .Select(m => new Matieresvieuw
                    {
                        Id = m.Id,
                        NomMatiere = m.NomMatiere,
                        cours = string.Join(", ", m.cours.Select(c => c.NomCours)) // Utiliser la relation Many-to-Many
                    }).ToList();

                dataGridView1.DataSource = matiereList;
            }
        }

        public void clear()
        {
            txtNom.Clear();

            // Décocher toutes les classes
            for (int i = 0; i < clbCours.Items.Count; i++)
            {
                clbCours.SetItemChecked(i, false);
            }
        }


        private void FormMatiere_Load(object sender, EventArgs e)
        {
            using (var db = new DBScolaireContext())
            {
                clbCours.DataSource = db.cours.ToList();
                clbCours.DisplayMember = "NomCours"; 
                clbCours.ValueMember = "Id";
            }
        }
    }
}
