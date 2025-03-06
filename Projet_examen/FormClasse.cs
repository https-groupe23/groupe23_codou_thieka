using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.Entity;

namespace Projet_examen
{
    public partial class FormClasse : Form
    {
        public FormClasse()
        {
            InitializeComponent();
            
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtnom.Text) || clbCours.CheckedItems.Count == 0)
            {
                MessageBox.Show("Veuillez saisir le nom de la classe et sélectionner au moins un cours.",
                    "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var db = new DBScolaireContext())
                {
                    // Vérifier si la classe existe déjà
                    var classe = db.classes.Include(c => c.cours).FirstOrDefault(c => c.NomClasse == txtnom.Text);

                    if (classe == null)
                    {
                        classe = new Classes
                        {
                            NomClasse = txtnom.Text,
                            cours = new List<Cours>() // Initialisation de la liste des cours
                        };

                        db.classes.Add(classe);
                    }
                    else
                    {
                        // Si la classe existe, on vide d'abord sa liste de cours pour éviter les doublons
                        classe.cours.Clear();
                    }

                    // Associer les cours sélectionnés
                    foreach (var item in clbCours.CheckedItems)
                    {
                        if (item is Cours selectedCours)
                        {
                            var cours = db.cours.Find(selectedCours.Id); // Récupérer l'objet existant
                            if (cours != null && !classe.cours.Contains(cours))
                            {
                                classe.cours.Add(cours);
                            }
                        }
                    }

                    db.SaveChanges();
                    refresh();
                    Clear();

                    MessageBox.Show("Classe ajoutée avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur: " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void refresh()
        {
            using (var db = new DBScolaireContext())
            {
                var classes = db.classes
                    .Include(c => c.cours) // Inclure les cours liés
                    .ToList() // Charger les données en mémoire
                    .Select(c => new
                    {
                        c.Id,
                        c.NomClasse,
                        Cours = string.Join(", ", c.cours.Select(co => co.NomCours)) // Concaténer les noms des cours après récupération
                    })
                    .ToList();

                dataGridViewClasse.DataSource = classes;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtnom.Text) || clbCours.CheckedItems.Count == 0)
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (!int.TryParse(txtIdClasse.Text, out int classeId))
                {
                    MessageBox.Show("ID de la classe invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var db = new DBScolaireContext())
                {
                    var classe = db.classes.Include(c => c.cours).FirstOrDefault(c => c.Id == classeId);

                    if (classe != null)
                    {
                        classe.NomClasse = txtnom.Text;

                        // Mise à jour des cours associés
                        classe.cours.Clear(); // Supprimer les anciennes associations

                        foreach (var item in clbCours.CheckedItems)
                        {
                            if (item is Cours selectedCours)
                            {
                                var cours = db.cours.Find(selectedCours.Id); // Récupérer l'objet depuis la base
                                if (cours != null)
                                {
                                    classe.cours.Add(cours);
                                }
                            }
                        }

                        db.SaveChanges();
                        refresh();
                        MessageBox.Show("Classe modifiée avec succès.", "Modification réussie", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear();
                    }
                    else
                    {
                        MessageBox.Show("Classe non trouvée.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void Clear()
        {

            txtIdClasse.Clear();
            txtnom.Clear();

            for (int i = 0; i < clbCours.Items.Count; i++)
            {
                clbCours.SetItemChecked(i, false);
            }
        }

        private void dataGridViewClasse_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridViewClasse.Rows[e.RowIndex];
                txtIdClasse.Text = selectedRow.Cells["Id"].Value.ToString();
                txtnom.Text = selectedRow.Cells["NomClasse"].Value.ToString();

                using (var db = new DBScolaireContext())
                {
                    int classeId = int.Parse(txtIdClasse.Text);
                    var classe = db.classes
                        .Where(c => c.Id == classeId)
                        .Include(c => c.cours)
                        .FirstOrDefault();

                    if (classe != null)
                    {
                        // Décocher toutes les cases d'abord
                        for (int i = 0; i < clbCours.Items.Count; i++)
                        {
                            clbCours.SetItemChecked(i, false);
                        }

                        // Cocher uniquement les cours associés à la classe
                        foreach (var cours in classe.cours)
                        {
                            for (int i = 0; i < clbCours.Items.Count; i++)
                            {
                                var item = (Cours)clbCours.Items[i];
                                if (item.Id == cours.Id)
                                {
                                    clbCours.SetItemChecked(i, true);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Voulez-vous vraiment supprimer cette classe ?", "Confirmation de suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                if (dataGridViewClasse.CurrentRow != null)
                {
                    int selectedId = (int)dataGridViewClasse.CurrentRow.Cells["Id"].Value;

                    using (var db = new DBScolaireContext())
                    {
                        var classe = db.classes.FirstOrDefault(c => c.Id == selectedId);

                        if (classe != null)
                        {
                            db.classes.Remove(classe);
                            db.SaveChanges();

                            refresh();
                            Clear();

                            MessageBox.Show("Classe supprimée avec succès.", "Suppression réussie", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                }
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void FormClasse_Load(object sender, EventArgs e)
        {
            refresh();
            using (var db = new DBScolaireContext())
            {
                // Charger la liste des cours disponibles dans le CheckedListBox
                clbCours.DataSource = db.cours.ToList();
                clbCours.DisplayMember = "NomCours"; // Afficher le nom des cours
                clbCours.ValueMember = "Id"; // Stocker l'ID des cours
            }
        }
    }
}



