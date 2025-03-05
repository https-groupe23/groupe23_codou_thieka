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
            {
                if (string.IsNullOrWhiteSpace(txtNom.Text) || cbCours.SelectedIndex == -1)
                {
                    MessageBox.Show("Veuillez saisir toutes les informations.", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                            // Récupérer le cours sélectionné
                            int idCours = (int)cbCours.SelectedValue;
                            Cours cours = db.cours.Find(idCours);

                            if (cours != null)
                            {
                                // Ajouter la matière au cours (relation Many-to-Many)
                                if (!matiere.cours.Contains(cours))
                                {
                                    matiere.cours.Add(cours);
                                    db.SaveChanges();
                                   
                                }
                            }

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
            txtNom.Text = "";
            cbCours.SelectedIndex = -1;
        }


        private void FormMatiere_Load(object sender, EventArgs e)
        {
            using (var db = new DBScolaireContext())
            {
                cbCours.DataSource = db.cours.ToList();
                cbCours.DisplayMember = "NomCours";
                cbCours.ValueMember = "Id";
            }
        }
    }
}
