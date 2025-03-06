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
    public partial class FormProfesseur : Form
    {
        public FormProfesseur()
        {
            InitializeComponent();
            refresh();
        }

        private void FormProfesseur_Load(object sender, EventArgs e)
        {
            using (var db = new DBScolaireContext())
            {
                clbMatiere.DataSource = db.matieres.ToList();
                clbMatiere.DisplayMember = "NomMatiere";
                clbMatiere.ValueMember = "Id";
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNom.Text) || string.IsNullOrWhiteSpace(txtPrenom.Text) ||
               string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtTelephone.Text) ||
               clbMatiere.CheckedItems.Count == 0)
            {
                MessageBox.Show("Veuillez remplir tous les champs et sélectionner au moins une matière.", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    using (var db = new DBScolaireContext())
                    {
                        // Vérifier si le professeur existe déjà
                        Professeurs prof = db.professeurs.FirstOrDefault(p => p.Email == txtEmail.Text);

                        if (prof == null) // Si le professeur n'existe pas, on le crée
                        {
                            prof = new Professeurs
                            {
                                Nom = txtNom.Text,
                                Prenom = txtPrenom.Text,
                                Email = txtEmail.Text,
                                Telephone = txtTelephone.Text,
                                matieres = new List<Matieres>()
                            };

                            db.professeurs.Add(prof);
                            db.SaveChanges();
                        }

                        // Ajouter les matières sélectionnées au professeur
                        foreach (var item in clbMatiere.CheckedItems)
                        {
                            Matieres matiere = item as Matieres;
                            if (matiere != null && !prof.matieres.Contains(matiere))
                            {
                                prof.matieres.Add(db.matieres.Find(matiere.Id));
                            }
                        }

                        db.SaveChanges();

                        // Afficher un message de confirmation
                        MessageBox.Show("Professeur et matières associées ajoutés avec succès!", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                var professeurList = db.professeurs
                    .Include(p => p.matieres)
                    .ToList()
                    .Select(p => new Professeurvieuw
                    {
                        Id = p.Id,
                        Nom = p.Nom,
                        Prenom = p.Prenom,
                        Email = p.Email,
                        Telephone = p.Telephone,
                        Matieres = string.Join(", ", p.matieres.Select(m => m.NomMatiere))
                    }).ToList();

                dataGridView1.DataSource = professeurList;
            }
        }
        public void clear()
        {
            txtNom.Clear();
            txtPrenom.Clear();
            txtEmail.Clear();
            txtTelephone.Clear();

            // Décocher toutes les matières
            for (int i = 0; i < clbMatiere.Items.Count; i++)
            {
                clbMatiere.SetItemChecked(i, false);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id = (int)dataGridView1.Rows[e.RowIndex].Cells["Id"].Value;

                using (var db = new DBScolaireContext())
                {
                    Professeurs professeur = db.professeurs.Include(p => p.matieres).FirstOrDefault(p => p.Id == id);

                    if (professeur != null)
                    {
                        txtNom.Text = professeur.Nom;
                        txtPrenom.Text = professeur.Prenom;
                        txtEmail.Text = professeur.Email;
                        txtTelephone.Text = professeur.Telephone;

                        // Cocher les matières associées
                        for (int i = 0; i < clbMatiere.Items.Count; i++)
                        {
                            Matieres matiere = clbMatiere.Items[i] as Matieres;
                            if (matiere != null && professeur.matieres.Any(m => m.Id == matiere.Id))
                            {
                                clbMatiere.SetItemChecked(i, true);
                            }
                            else
                            {
                                clbMatiere.SetItemChecked(i, false);
                            }
                        }
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un professeur à supprimer.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var db = new DBScolaireContext())
                {
                    int id = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;
                    Professeurs professeur = db.professeurs.Include(p => p.matieres).FirstOrDefault(p => p.Id == id);

                    if (professeur != null)
                    {
                        db.professeurs.Remove(professeur);
                        db.SaveChanges();
                        MessageBox.Show("Suppression réussie !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        refresh();
                        clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur: " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un professeur à modifier.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var db = new DBScolaireContext())
                {
                    int id = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;
                    Professeurs professeur = db.professeurs.Include(p => p.matieres).FirstOrDefault(p => p.Id == id);

                    if (professeur != null)
                    {
                        professeur.Nom = txtNom.Text;
                        professeur.Prenom = txtPrenom.Text;
                        professeur.Email = txtEmail.Text;
                        professeur.Telephone = txtTelephone.Text;

                        // Mettre à jour les matières associées
                        professeur.matieres.Clear();
                        foreach (var item in clbMatiere.CheckedItems)
                        {
                            Matieres matiere = item as Matieres;
                            if (matiere != null)
                            {
                                professeur.matieres.Add(matiere);
                            }
                        }

                        db.SaveChanges();
                        MessageBox.Show("Modification réussie !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        refresh();
                        clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur: " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
  



        }

        private void txtTelephone_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTelephone_Validating(object sender, CancelEventArgs e)
        {
            string pattern = @"^(77|78|76|70)\d{7}$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtTelephone.Text, pattern))
            {
                e.Cancel = true;
                errorProvider2.SetError(txtTelephone, "Numéro invalide. Il doit commencer par 77, 78, 76 ou 70 et contenir 9 chiffres.");
            }
            else
            {
                errorProvider2.SetError(txtTelephone, "");
            }

        }
    }
}
