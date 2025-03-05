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
using System.Data.Entity;


namespace Projet_examen
{
    public partial class FormCours : Form
    {
        public FormCours()
        {
            InitializeComponent();
            refresh();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNom.Text) || string.IsNullOrWhiteSpace(txtDescription.Text) || clbClasses.CheckedItems.Count == 0)
            {
                MessageBox.Show("Veuillez saisir toutes les informations.", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var db = new DBScolaireContext())
                {
                    Cours cours = db.cours.FirstOrDefault(c => c.NomCours == txtNom.Text);

                    if (cours == null)
                    {
                        cours = new Cours
                        {
                            NomCours = txtNom.Text,
                            Description = txtDescription.Text,
                            classes = new List<Classes>() // Initialiser la liste des classes
                        };

                        db.cours.Add(cours);
                    }

                    // Ajouter toutes les classes sélectionnées depuis la base de données
                    foreach (var item in clbClasses.CheckedItems)
                    {
                        if (item is Classes selectedClass)
                        {
                            var classe = db.classes.Find(selectedClass.Id); // Récupérer l'objet existant depuis la base
                            if (classe != null && !cours.classes.Contains(classe))
                            {
                                cours.classes.Add(classe); // Ajouter la classe existante au cours
                            }
                        }
                    }


                    db.SaveChanges();
                    refresh();
                    Clear();
                    MessageBox.Show("Cours ajouté avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur: " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void refresh()
        {
            dataGridView1.DataSource = null;
            using (var db = new DBScolaireContext())
            {
                var coursList = db.cours
                    .Include(c => c.classes)
                    .ToList()
                    .Select(c => new Coursvieuw
                    {
                        Id = c.Id,
                        NomCours = c.NomCours,
                        Description = c.Description,
                        classes = string.Join(", ", c.classes.Select(cl => cl.NomClasse))
                    }).ToList();

                dataGridView1.DataSource = coursList;
            }
        }


        private void FormCours_Load(object sender, EventArgs e)
        {
            refresh();
            using (var db = new DBScolaireContext())
            {
                // Charger toutes les classes disponibles
                clbClasses.DataSource = db.classes.ToList();
                clbClasses.DisplayMember = "NomClasse"; // Afficher le nom des classes
                clbClasses.ValueMember = "Id"; // Stocker l'ID des classes
            }
            
        }
        private void Clear()
        {
            txtIdCours.Clear();
            txtNom.Clear();
            txtDescription.Clear();

            // Décocher toutes les classes
            for (int i = 0; i < clbClasses.Items.Count; i++)
            {
                clbClasses.SetItemChecked(i, false);
            }
        }

        private void cbClasse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }




        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNom.Text) || string.IsNullOrWhiteSpace(txtDescription.Text) || clbClasses.CheckedItems.Count == 0)
            {
                MessageBox.Show("Veuillez saisir toutes les informations.", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (!int.TryParse(txtIdCours.Text, out int selectId))
                {
                    MessageBox.Show("L'ID du cours n'est pas valide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var db = new DBScolaireContext())
                {
                    var cours = db.cours.Include(c => c.classes).FirstOrDefault(c => c.Id == selectId);

                    if (cours != null)
                    {
                        // Mise à jour des informations
                        cours.NomCours = txtNom.Text;
                        cours.Description = txtDescription.Text;

                        // Mettre à jour la relation Many-to-Many
                        cours.classes.Clear(); // Supprime les anciennes relations

                        foreach (var item in clbClasses.CheckedItems)
                        {
                            if (item is Classes selectedClass)
                            {
                                var classe = db.classes.Find(selectedClass.Id); // Récupérer l'objet depuis la base
                                if (classe != null)
                                {
                                    cours.classes.Add(classe); // Ajouter la classe existante
                                }
                            }
                        }

                        db.SaveChanges();
                        refresh();
                        Clear();
                        MessageBox.Show("Cours modifié avec succès.", "Modification réussie", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Cours non trouvé.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur: " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txtIdCours.Text = row.Cells["Id"].Value.ToString();
                txtNom.Text = row.Cells["NomCours"].Value.ToString();
                txtDescription.Text = row.Cells["Description"].Value.ToString();

                using (var db = new DBScolaireContext())
                {
                    int coursId = int.Parse(txtIdCours.Text);
                    var cours = db.cours.Include(c => c.classes).FirstOrDefault(c => c.Id == coursId);

                    if (cours != null)
                    {
                        // Décocher toutes les cases
                        for (int i = 0; i < clbClasses.Items.Count; i++)
                        {
                            clbClasses.SetItemChecked(i, false);
                        }

                        // Cocher uniquement les classes associées au cours
                        foreach (var classe in cours.classes)
                        {
                            for (int i = 0; i < clbClasses.Items.Count; i++)
                            {
                                var item = (Classes)clbClasses.Items[i];
                                if (item.Id == classe.Id)
                                {
                                    clbClasses.SetItemChecked(i, true);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

}
