namespace Projet_examen
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.classesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.coursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.etToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.matieresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oTPcodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.professeursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.utilisateursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rapportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.genererLesRelevésDeNotesDunEtudiantToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.genererLaListeDesEtudiantsParClasseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.genererLaListeDesEtudiantsParClassesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exporterLesRapportsEnPDFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.classesToolStripMenuItem,
            this.coursToolStripMenuItem,
            this.etToolStripMenuItem,
            this.matieresToolStripMenuItem,
            this.noteToolStripMenuItem,
            this.oTPcodeToolStripMenuItem,
            this.professeursToolStripMenuItem,
            this.utilisateursToolStripMenuItem,
            this.rapportsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // classesToolStripMenuItem
            // 
            this.classesToolStripMenuItem.Name = "classesToolStripMenuItem";
            this.classesToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.classesToolStripMenuItem.Text = "Classes";
            this.classesToolStripMenuItem.Click += new System.EventHandler(this.classesToolStripMenuItem_Click);
            // 
            // coursToolStripMenuItem
            // 
            this.coursToolStripMenuItem.Name = "coursToolStripMenuItem";
            this.coursToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.coursToolStripMenuItem.Text = "Etudiants";
            this.coursToolStripMenuItem.Click += new System.EventHandler(this.coursToolStripMenuItem_Click);
            // 
            // etToolStripMenuItem
            // 
            this.etToolStripMenuItem.Name = "etToolStripMenuItem";
            this.etToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.etToolStripMenuItem.Text = "Cours";
            this.etToolStripMenuItem.Click += new System.EventHandler(this.etToolStripMenuItem_Click);
            // 
            // matieresToolStripMenuItem
            // 
            this.matieresToolStripMenuItem.Name = "matieresToolStripMenuItem";
            this.matieresToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.matieresToolStripMenuItem.Text = "Matieres";
            this.matieresToolStripMenuItem.Click += new System.EventHandler(this.matieresToolStripMenuItem_Click);
            // 
            // noteToolStripMenuItem
            // 
            this.noteToolStripMenuItem.Name = "noteToolStripMenuItem";
            this.noteToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.noteToolStripMenuItem.Text = "Note";
            this.noteToolStripMenuItem.Click += new System.EventHandler(this.noteToolStripMenuItem_Click);
            // 
            // oTPcodeToolStripMenuItem
            // 
            this.oTPcodeToolStripMenuItem.Name = "oTPcodeToolStripMenuItem";
            this.oTPcodeToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.oTPcodeToolStripMenuItem.Text = "OTPcode";
            this.oTPcodeToolStripMenuItem.Click += new System.EventHandler(this.oTPcodeToolStripMenuItem_Click);
            // 
            // professeursToolStripMenuItem
            // 
            this.professeursToolStripMenuItem.Name = "professeursToolStripMenuItem";
            this.professeursToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.professeursToolStripMenuItem.Text = "Professeurs";
            this.professeursToolStripMenuItem.Click += new System.EventHandler(this.professeursToolStripMenuItem_Click);
            // 
            // utilisateursToolStripMenuItem
            // 
            this.utilisateursToolStripMenuItem.Name = "utilisateursToolStripMenuItem";
            this.utilisateursToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.utilisateursToolStripMenuItem.Text = "Utilisateurs";
            this.utilisateursToolStripMenuItem.Click += new System.EventHandler(this.utilisateursToolStripMenuItem_Click);
            // 
            // rapportsToolStripMenuItem
            // 
            this.rapportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.genererLesRelevésDeNotesDunEtudiantToolStripMenuItem,
            this.genererLaListeDesEtudiantsParClasseToolStripMenuItem,
            this.genererLaListeDesEtudiantsParClassesToolStripMenuItem,
            this.exporterLesRapportsEnPDFToolStripMenuItem});
            this.rapportsToolStripMenuItem.Name = "rapportsToolStripMenuItem";
            this.rapportsToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.rapportsToolStripMenuItem.Text = "Rapports";
            // 
            // genererLesRelevésDeNotesDunEtudiantToolStripMenuItem
            // 
            this.genererLesRelevésDeNotesDunEtudiantToolStripMenuItem.Name = "genererLesRelevésDeNotesDunEtudiantToolStripMenuItem";
            this.genererLesRelevésDeNotesDunEtudiantToolStripMenuItem.Size = new System.Drawing.Size(293, 22);
            this.genererLesRelevésDeNotesDunEtudiantToolStripMenuItem.Text = "Generer les relevés de notes d\'un etudiant";
            // 
            // genererLaListeDesEtudiantsParClasseToolStripMenuItem
            // 
            this.genererLaListeDesEtudiantsParClasseToolStripMenuItem.Name = "genererLaListeDesEtudiantsParClasseToolStripMenuItem";
            this.genererLaListeDesEtudiantsParClasseToolStripMenuItem.Size = new System.Drawing.Size(293, 22);
            this.genererLaListeDesEtudiantsParClasseToolStripMenuItem.Text = "Generer la liste des etudiants par classe";
            // 
            // genererLaListeDesEtudiantsParClassesToolStripMenuItem
            // 
            this.genererLaListeDesEtudiantsParClassesToolStripMenuItem.Name = "genererLaListeDesEtudiantsParClassesToolStripMenuItem";
            this.genererLaListeDesEtudiantsParClassesToolStripMenuItem.Size = new System.Drawing.Size(293, 22);
            this.genererLaListeDesEtudiantsParClassesToolStripMenuItem.Text = "Generer la liste des etudiants par classes";
            // 
            // exporterLesRapportsEnPDFToolStripMenuItem
            // 
            this.exporterLesRapportsEnPDFToolStripMenuItem.Name = "exporterLesRapportsEnPDFToolStripMenuItem";
            this.exporterLesRapportsEnPDFToolStripMenuItem.Size = new System.Drawing.Size(293, 22);
            this.exporterLesRapportsEnPDFToolStripMenuItem.Text = "Exporter les rapports en PDF ou en Exel";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 449);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Connexion";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem classesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem coursToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem etToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem matieresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oTPcodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem professeursToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem utilisateursToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rapportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem genererLesRelevésDeNotesDunEtudiantToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem genererLaListeDesEtudiantsParClasseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem genererLaListeDesEtudiantsParClassesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exporterLesRapportsEnPDFToolStripMenuItem;
    }
}

