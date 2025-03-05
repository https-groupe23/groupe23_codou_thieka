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
            // Remplir le combobox de tri
            cbSortBy.Items.Add("Nom");
            cbSortBy.Items.Add("Matricule");
            cbSortBy.Items.Add("Résultat");

            if (cbSortBy.Items.Count > 0)
            {
                cbSortBy.SelectedIndex = 0; // Sélectionner le premier élément par défaut
            }

            using (var db = new DBScolaireContext())
            {
                var listeClasses = db.classes.ToList();

                if (listeClasses.Any())
                {
                    cbClasse.DataSource = listeClasses;
                    cbClasse.DisplayMember = "NomClasse";
                    cbClasse.ValueMember = "Id";
                }
                else
                {
                    MessageBox.Show("Aucune classe trouvée dans la base de données.");
                }

                listeClasses.Insert(0, new Classes { Id = 0, NomClasse = "Toutes les classes" });

                cbFilterClasse.DataSource = listeClasses;
                cbFilterClasse.DisplayMember = "NomClasse";
                cbFilterClasse.ValueMember = "Id";
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
            if (string.IsNullOrEmpty(txtNom.Text) || string.IsNullOrEmpty(txtprenom.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtAdresse.Text) || string.IsNullOrEmpty(txtTelephone.Text) || (!radioButton1.Checked && !radioButton2.Checked))
            {
                MessageBox.Show("Veuillez remplir tous les champs", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var db = new DBScolaireContext())
                {
                    // Vérifier si l'étudiant existe déjà par matricule ou téléphone
                    bool etudiantExiste = db.etudiants.Any(ee =>
                        ee.Matricule == txtMatricule.Text ||
                        ee.Telephone == txtTelephone.Text ||
                        ee.Email == txtEmail.Text);

                    if (etudiantExiste)
                    {
                        MessageBox.Show("Cet étudiant existe déjà dans la base de données ou le numéro de téléphone est déjà utilisé.", "Duplication détectée", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Création d'un nouvel étudiant
                    Etudiants etudiants = new Etudiants
                    {
                        Nom = txtNom.Text,
                        Prenom = txtprenom.Text,
                        Email = txtEmail.Text,
                        Telephone = txtTelephone.Text,
                        Adresse = txtAdresse.Text,
                        Matricule = txtMatricule.Text,
                        DateNaissance = dateTimePicker1.Value,
                        IdClasse = (int)cbClasse.SelectedValue,
                        sexe = radioButton1.Checked ? "Masculain" : "Feminin"
                    };

                    var classe = db.classes.FirstOrDefault(c => c.Id == etudiants.IdClasse);
                    if (classe != null)
                    {
                        etudiants.classe = classe;
                    }

                    // Ajouter et sauvegarder
                    db.etudiants.Add(etudiants);
                    db.SaveChanges();
                    refreshEtu();
                    ClearEtu();
                    MessageBox.Show("Étudiant ajouté avec succès !");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'ajout : " + ex.InnerException?.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void refreshEtu()
        {
            using (var db = new DBScolaireContext())
            {
                List<EtudiantVieuw> etudiants;

                // Vérifier si cbFilterClasse.SelectedValue est bien défini
                if (cbFilterClasse.SelectedValue == null)
                {
                    //MessageBox.Show("Veuillez sélectionner une classe valide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int selectedClasseId = (int)cbFilterClasse.SelectedValue;

                if (selectedClasseId == 0) // "Toutes les classes"
                {
                    etudiants = db.etudiants.Select(e => new EtudiantVieuw
                    {
                        Id = e.Id,
                        Matricule = e.Matricule,
                        Prenom = e.Prenom,
                        Nom = e.Nom,
                        sexe = e.sexe,
                        DateNaissance = e.DateNaissance,
                        Adresse = e.Adresse,
                        Telephone = e.Telephone,
                        Email = e.Email,
                        classe = e.classe.NomClasse
                    }).ToList();
                }
                else
                {
                    etudiants = db.etudiants
                        .Where(e => e.IdClasse == selectedClasseId)
                        .Select(e => new EtudiantVieuw
                        {
                            Id = e.Id,
                            Matricule = e.Matricule,
                            Prenom = e.Prenom,
                            Nom = e.Nom,
                            sexe = e.sexe,
                            DateNaissance = e.DateNaissance,
                            Adresse = e.Adresse,
                            Telephone = e.Telephone,
                            Email = e.Email,
                            classe = e.classe.NomClasse
                        }).ToList();
                }

                // Trier selon le critère choisi
                switch (cbSortBy.SelectedItem?.ToString())
                {
                    case "Nom":
                        etudiants = etudiants.OrderBy(e => e.Nom).ToList();
                        break;
                    case "Matricule":
                        etudiants = etudiants.OrderBy(e => e.Matricule).ToList();
                        break;
                    case "Résultat":
                        // etudiants = etudiants.OrderByDescending(e => e.Resultat).ToList(); // Trier par ordre décroissant
                        break;
                }

                dataGridView1.DataSource = etudiants;
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                try
                {
                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
                    DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                    txtprenom.Text = selectedRow.Cells["Prenom"].Value.ToString();
                    txtNom.Text = selectedRow.Cells["Nom"].Value.ToString();
                    txtIdEtudiant.Text = selectedRow.Cells["Id"].Value.ToString();

                    txtMatricule.Text = selectedRow.Cells["Matricule"].Value.ToString();
                    txtEmail.Text = selectedRow.Cells["Email"].Value.ToString();
                    txtAdresse.Text = selectedRow.Cells["Adresse"].Value.ToString();
                    txtTelephone.Text = selectedRow.Cells["Telephone"].Value.ToString();
                    cbClasse.Text = selectedRow.Cells["classe"].Value.ToString();
                    dateTimePicker1.Text = selectedRow.Cells["DateNaissance"].Value.ToString();
                    if (selectedRow.Cells["sexe"].Value.ToString() == "Feminin")
                    {
                        radioButton1.Checked = true;
                    }
                    else
                    {
                        radioButton2.Checked = true;
                    }
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
            if (string.IsNullOrEmpty(txtNom.Text) || string.IsNullOrEmpty(txtprenom.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtAdresse.Text) || string.IsNullOrEmpty(txtTelephone.Text) || (!radioButton1.Checked && !radioButton2.Checked))
            {
                MessageBox.Show("Veuillez remplir tous les champs", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var db = new DBScolaireContext())
                {
                    int idEtudiant = int.Parse(txtIdEtudiant.Text);

                    // Vérifier si un autre étudiant avec les mêmes informations existe
                    bool etudiantExiste = db.etudiants.Any(ee =>
                    ee.Id != idEtudiant && // Exclure l'étudiant en cours de modification
                    ee.Matricule == txtMatricule.Text); // Comparer le matricule uniquement
                    

                    if (etudiantExiste)
                    {
                        MessageBox.Show("Un autre étudiant avec les mêmes informations existe déjà.", "Duplication détectée", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Récupérer l'étudiant à modifier
                    Etudiants etudiant = db.etudiants.FirstOrDefault(t => t.Id == idEtudiant);
                    if (etudiant != null)
                    {
                        etudiant.Nom = txtNom.Text;
                        etudiant.Prenom = txtprenom.Text;
                        etudiant.Email = txtEmail.Text;
                        etudiant.Telephone = txtTelephone.Text;
                        etudiant.Adresse = txtAdresse.Text;
                        etudiant.Matricule = txtMatricule.Text;
                        etudiant.DateNaissance = dateTimePicker1.Value;
                        etudiant.IdClasse = (int)cbClasse.SelectedValue;

                        var classe = db.classes.FirstOrDefault(c => c.Id == etudiant.IdClasse);
                        if (classe != null)
                        {
                            etudiant.classe = classe;
                        }

                        etudiant.sexe = radioButton1.Checked ? "Masculain" : "Feminin";

                        db.SaveChanges();
                        refreshEtu();
                        MessageBox.Show("L'étudiant a été modifié avec succès !");
                        ClearEtu();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la modification : " + ex.InnerException?.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Voulez vous vraiment supprimer cette etudiant? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    using (var db = new DBScolaireContext())
                    {
                        int id = int.Parse(txtIdEtudiant.Text);
                        var etudiant = db.etudiants.FirstOrDefault(t => t.Id == id);
                        if (etudiant != null)
                        {
                            db.etudiants.Remove(etudiant);
                            db.SaveChanges();
                            refreshEtu();
                            ClearEtu();
                            MessageBox.Show("L'etudiant a été supprimer");


                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur de suppression: {ex.Message}");
                }

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

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@(gmail\.com|yahoo\.com|outlook\.com|hotmail\.com)$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text, pattern))
            {
                e.Cancel = true;
                errorProvider2.SetError(txtEmail, "L'adresse e-mail doit être valide (@gmail.com, @yahoo.com, @outlook.com, @hotmail.com)");
            }
            else
            {
                errorProvider2.SetError(txtEmail, "");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearEtu();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim(); // Récupérer le terme de recherche

            if (string.IsNullOrEmpty(searchTerm))
            {
                // Si le champ de recherche est vide, rafraîchir la grille avec toutes les données
                refreshEtu();
            }
            else
            {
                using (var db = new DBScolaireContext())
                {
                    var result = db.etudiants
                                   .Where(ee => ee.Nom.Contains(searchTerm) || // Recherche par nom
                                              ee.Matricule.Contains(searchTerm) || // Recherche par matricule
                                              ee.classe.NomClasse.Contains(searchTerm)) // Recherche par classe
                                   .Select(ee => new EtudiantVieuw
                                   {
                                       Id = ee.Id,
                                       Matricule = ee.Matricule,
                                       Prenom = ee.Prenom,
                                       Nom = ee.Nom,
                                       sexe = ee.sexe,
                                       DateNaissance = ee.DateNaissance,
                                       Adresse = ee.Adresse,
                                       Telephone = ee.Telephone,
                                       Email = ee.Email,
                                       classe = ee.classe.NomClasse
                                   })
                                   .ToList();

                    // Afficher les résultats dans la DataGridView
                    dataGridView1.DataSource = result;
                }
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            btnSearch.PerformClick();
        }

        private void cbFilterClasse_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var db = new DBScolaireContext())
            {
                List<EtudiantVieuw> etudiants;

                if (cbFilterClasse.SelectedIndex == 0) // "Toutes les classes" sélectionnée
                {
                    etudiants = db.etudiants.Select(ee => new EtudiantVieuw
                    {
                        Id = ee.Id,
                        Matricule = ee.Matricule,
                        Prenom = ee.Prenom,
                        Nom = ee.Nom,
                        sexe = ee.sexe,
                        DateNaissance = ee.DateNaissance,
                        Adresse = ee.Adresse,
                        Telephone = ee.Telephone,
                        Email = ee.Email,
                        classe = ee.classe.NomClasse
                    }).ToList();
                }
                else
                {
                    int selectedClasseId = (int)cbFilterClasse.SelectedValue;
                    etudiants = db.etudiants
                        .Where(ee => ee.IdClasse == selectedClasseId)
                        .Select(ee => new EtudiantVieuw
                        {
                            Id = ee.Id,
                            Matricule = ee.Matricule,
                            Prenom = ee.Prenom,
                            Nom = ee.Nom,
                            sexe = ee.sexe,
                            DateNaissance = ee.DateNaissance,
                            Adresse = ee.Adresse,
                            Telephone = ee.Telephone,
                            Email = ee.Email,
                            classe = ee.classe.NomClasse
                        }).ToList();
                }

                dataGridView1.DataSource = etudiants;
            }
        }

        private void cbSortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshEtu();
        }
    }
}
