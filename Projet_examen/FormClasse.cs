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
            if (string.IsNullOrWhiteSpace(txtnom.Text))
            {
                MessageBox.Show("Veillez saisir le nom de la classe.", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    using (var db = new DBScolaireContext())
                    {
                        // Vérifier si la classe existe déjà
                        bool exists = db.classes.Any(c => c.NomClasse == txtnom.Text);

                        if (exists)
                        {
                            MessageBox.Show("Cette classe existe déjà et ne peut pas être ajoutée à nouveau.", "Duplication", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            Classes classes = new Classes();
                            classes.NomClasse = txtnom.Text;
                            db.classes.Add(classes);
                            db.SaveChanges();

                            refresh();
                            Clear();

                            MessageBox.Show("Classe ajoutée avec succès.", "Ajout réussi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
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
            dataGridViewClasse.DataSource = null;
            using (var db = new DBScolaireContext())
            {
                var classes = db.classes.ToList();
 
                dataGridViewClasse.DataSource = db.classes.Select(e => new ViewClasses { Id = e.Id, NomClasse = e.NomClasse }).ToList();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewClasse.CurrentRow != null)
            {
                try
                {
                    int selectId=int.Parse(txtIdClasse.Text);

                    using (var db = new DBScolaireContext())
                    {
                        var classe = db.classes.FirstOrDefault(ee => ee.Id == selectId);
                        if (classe != null)
                        {
                            if (string.IsNullOrWhiteSpace(txtnom.Text))
                            {
                                MessageBox.Show("remplir correctement le champ.", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                            else {
                                classe.NomClasse = txtnom.Text;
                                db.SaveChanges();

                                refresh();

                                MessageBox.Show("Les informations de la classe ont été mises à jour avec succès.", "Modification réussie", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Clear();
                            }
                           
                        }
                        else
                        {
                            MessageBox.Show("classe introuvable.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Une erreur est survenue : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        private void Clear()
        {
            txtnom.Clear();
        }

        private void dataGridViewClasse_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridViewClasse.Rows[e.RowIndex];
                txtIdClasse.Text = selectedRow.Cells["Id"].Value.ToString();
                txtnom.Text = selectedRow.Cells["NomClasse"].Value.ToString();
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
        }
    }
}



