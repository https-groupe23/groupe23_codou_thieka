using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Projet_examen
{
    public partial class FormEtudiant : Form
    {
        public FormEtudiant()
        {
            InitializeComponent();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void FormEtudiant_Load(object sender, EventArgs e)
        {
            using (var db = new DBScolaireContext())
            {
                
                cbClasse.DataSource = db.classes.ToList();
                cbClasse.DisplayMember = "NomClasse";
                cbClasse.ValueMember = "Id";
            }
            refreshEtu();

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new DBScolaireContext())
                {
                   
                    if (string.IsNullOrWhiteSpace(txtNom.Text) ||
                        string.IsNullOrWhiteSpace(txtprenom.Text) ||
                        string.IsNullOrWhiteSpace(txtTelephone.Text) ||
                        string.IsNullOrWhiteSpace(txtAdresse.Text) ||
                        string.IsNullOrWhiteSpace(txtEmail.Text) ||
                        cbClasse.SelectedIndex == -1 ||
                        (!radioButton1.Checked && !radioButton2.Checked))
                    {
                        MessageBox.Show("Veuillez remplir tous les champs obligatoires.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    
                    Etudiants etudiant = new Etudiants
                    {
                        Nom = txtNom.Text,
                        Prenom = txtprenom.Text,
                        Adresse = txtAdresse.Text,
                        Telephone = txtTelephone.Text,
                        Email = txtEmail.Text,
                        DateNaissance = dateTimePicker1.Value,
                        Matricule = txtMatricule.Text,
                        IdClasse = (int)cbClasse.SelectedValue,
                        classe = (Classes)cbClasse.SelectedItem,
                        sexe = radioButton1.Checked ? "Homme" : "Femme"
                    };

                    // Ajouter l'étudiant à la base de données
                    db.etudiants.Add(etudiant);
                    db.SaveChanges();

                    // Rafraîchir l'affichage et réinitialiser les champs
                    refreshEtu();
                    MessageBox.Show("Étudiant ajouté avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearEtu();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur inattendue est survenue : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void refreshEtu()
        {
            dataGridView1.DataSource = null;
            using (var db = new DBScolaireContext())
            {
                var etudiants = db.etudiants.ToList();

              dataGridView1.DataSource = db.etudiants.Select(e => new EtudiantVieuw {

                  Id = e.Id,
                  Nom = e.Nom,
                  Prenom = e.Prenom,
                  Adresse = e.Adresse,
                  Telephone = e.Telephone,
                  Email = e.Email,
                  DateNaissance = e.DateNaissance,
                  Matricule = e.Matricule,                      
                  sexe = e.sexe,
                  classe = e.classe.NomClasse,

              }).ToList();
            }
        }
        private void ClearEtu()
        {
            // Vider les champs texte
            txtprenom.Clear();
            txtNom.Clear();
            txtAdresse.Clear();
            txtEmail.Clear();
            txtTelephone.Clear();
            txtMatricule.Clear();

            // Réinitialiser le ComboBox
            cbClasse.SelectedIndex = -1;

            // Décocher tous les RadioButton
            radioButton1.Checked = false;
            radioButton2.Checked = false;

            // Réinitialiser la DateTimePicker
            dateTimePicker1.Value = DateTime.Now;


        }

        private void txtTelephone_MouseLeave(object sender, EventArgs e)
        {
            txtMatricule.Text = txtNom.Text + txtTelephone.Text;

        }
    }
}
