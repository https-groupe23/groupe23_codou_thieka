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
    public partial class FormUtilisateur : Form
    {
        public FormUtilisateur()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbRole.Text) || string.IsNullOrEmpty(txtNom.Text) || string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtConfirme.Text) || string.IsNullOrEmpty(txtTelephone.Text))
            {
                MessageBox.Show("Veillez remplir tous les champs", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (txtPassword.Text == txtConfirme.Text)
                {
                    try
                    {
                        using (var db = new DBScolaireContext())
                        {
                            Utilisateurs user = new Utilisateurs();
                            user.NomUtilisateur = txtNom.Text;
                            user.MotDepasse = txtPassword.Text;
                            //user.MotDepasse = BCrypt.Net.BCrypt.HashPassword(txtPassword.Text);
                            user.Telephone = txtTelephone.Text;
                            user.Role = cbRole.Text;
                            db.utilisateurs.Add(user);
                            db.SaveChanges();
                            RefreshUser();
                            Clear();
                            MessageBox.Show("Utilisateur ajouté avec succes !");

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur lors de l'ajout : " + ex.InnerException?.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {

                }

            }
        }
        private void RefreshUser()
        {
            dataGridView1.DataSource = null;
            using (var db = new DBScolaireContext())
            {
                dataGridView1.DataSource = db.utilisateurs.Select(u => new Userview
                {
                    Id = u.Id,
                    NomUtilisateur = u.NomUtilisateur,
                    //MotDepasse = u.MotDepasse,
                    Telephone = u.Telephone,
                    Role = u.Role,
                }).ToList();
            }
        }
        private void Clear()
        {
            txtConfirme.Clear();
            txtNom.Clear();
            txtPassword.Clear();
            cbRole.Text = "";
            txtTelephone.Clear();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = checkBox1.Checked;
            txtPassword.UseSystemPasswordChar = !isChecked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = checkBox2.Checked;
            txtConfirme.UseSystemPasswordChar = !isChecked;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                try
                {
                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
                    DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                    txtNom.Text = selectedRow.Cells["NomUtilisateur"].Value.ToString();
                    // txtPassword.Text = selectedRow.Cells["MotDePasse"].Value.ToString();
                    txtIdUser.Text = selectedRow.Cells["Id"].Value.ToString();

                    txtTelephone.Text = selectedRow.Cells["Telephone"].Value.ToString();
                    cbRole.Text = selectedRow.Cells["Role"].Value.ToString();
                    btnAdd.Enabled = false;


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Impossible de charger les données: " + ex.InnerException?.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbRole.Text) || string.IsNullOrEmpty(txtNom.Text) || string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtConfirme.Text) || string.IsNullOrEmpty(txtTelephone.Text))
            {
                MessageBox.Show("Veillez remplir tous les champs", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    using (var db = new DBScolaireContext())
                    {
                        int Id = int.Parse(txtIdUser.Text);
                        Utilisateurs user = db.utilisateurs.FirstOrDefault(u => u.Id == Id);
                        user.NomUtilisateur = txtNom.Text;
                        user.MotDepasse = txtPassword.Text;
                        user.Role = cbRole.Text;
                        user.Telephone = txtTelephone.Text;
                        db.SaveChanges();
                        RefreshUser();
                        MessageBox.Show("l'utilisateur a été modifier avec succes !");
                        Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de la modification : " + ex.InnerException?.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Voulez vous vraiment supprimer cet utilisateur ? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    using (var db = new DBScolaireContext())
                    {
                        int id = int.Parse(txtIdUser.Text);
                        var user = db.utilisateurs.FirstOrDefault(t => t.Id == id);
                        if (user != null)
                        {
                            db.utilisateurs.Remove(user);
                            db.SaveChanges();
                            RefreshUser();
                            Clear();
                            MessageBox.Show("L'utilisateur a été supprimer");


                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur de suppression: {ex.Message}");
                }

            }
        }

        private void FormUtilisateur_Load(object sender, EventArgs e)
        {
            RefreshUser();
        }

        private void txtConfirme_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirme.Text != txtPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(label6, "mot de passe non conforme");
            }
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
