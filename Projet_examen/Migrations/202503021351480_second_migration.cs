namespace Projet_examen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second_migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomClasse = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cours",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomCours = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Matieres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomMatiere = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdMatiere = c.Int(nullable: false),
                        IdEtudiant = c.Int(nullable: false),
                        Note = c.Single(nullable: false),
                        Etudiants_Id = c.Int(),
                        Matieres_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Etudiants", t => t.Etudiants_Id)
                .ForeignKey("dbo.Etudiants", t => t.IdEtudiant)
                .ForeignKey("dbo.Matieres", t => t.IdMatiere)
                .ForeignKey("dbo.Matieres", t => t.Matieres_Id)
                .Index(t => t.IdMatiere)
                .Index(t => t.IdEtudiant)
                .Index(t => t.Etudiants_Id)
                .Index(t => t.Matieres_Id);
            
            CreateTable(
                "dbo.Etudiants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Matricule = c.String(),
                        Nom = c.String(),
                        Prenom = c.String(),
                        DateNaissance = c.DateTime(nullable: false),
                        sexe = c.String(),
                        Adresse = c.String(),
                        Telephone = c.String(),
                        Email = c.String(),
                        IdClasse = c.Int(nullable: false),
                        Classes_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classes", t => t.IdClasse)
                .ForeignKey("dbo.Classes", t => t.Classes_Id)
                .Index(t => t.IdClasse)
                .Index(t => t.Classes_Id);
            
            CreateTable(
                "dbo.Professeurs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Prenom = c.String(),
                        Email = c.String(),
                        Telephone = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.OTPCodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        DateExpiration = c.DateTime(nullable: false),
                        IdUtilisateur = c.Int(nullable: false),
                        Utilisateurs_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Utilisateurs", t => t.Utilisateurs_Id)
                .ForeignKey("dbo.Utilisateurs", t => t.IdUtilisateur)
                .Index(t => t.IdUtilisateur)
                .Index(t => t.Utilisateurs_Id);
            
            CreateTable(
                "dbo.Utilisateurs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomUtilisateur = c.String(),
                        MotDepasse = c.String(),
                        Telephone = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProfesseursClasses",
                c => new
                    {
                        IdProfesseur = c.Int(nullable: false),
                        IdClasse = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdProfesseur, t.IdClasse })
                .ForeignKey("dbo.Professeurs", t => t.IdProfesseur, cascadeDelete: true)
                .ForeignKey("dbo.Classes", t => t.IdClasse, cascadeDelete: true)
                .Index(t => t.IdProfesseur)
                .Index(t => t.IdClasse);
            
            CreateTable(
                "dbo.ProfesseursMatieres",
                c => new
                    {
                        IdProfesseur = c.Int(nullable: false),
                        IdMatiere = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdProfesseur, t.IdMatiere })
                .ForeignKey("dbo.Professeurs", t => t.IdProfesseur, cascadeDelete: true)
                .ForeignKey("dbo.Matieres", t => t.IdMatiere, cascadeDelete: true)
                .Index(t => t.IdProfesseur)
                .Index(t => t.IdMatiere);
            
            CreateTable(
                "dbo.CoursMatieres",
                c => new
                    {
                        IdCours = c.Int(nullable: false),
                        IdMatiere = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdCours, t.IdMatiere })
                .ForeignKey("dbo.Cours", t => t.IdCours, cascadeDelete: true)
                .ForeignKey("dbo.Matieres", t => t.IdMatiere, cascadeDelete: true)
                .Index(t => t.IdCours)
                .Index(t => t.IdMatiere);
            
            CreateTable(
                "dbo.ClassesCours",
                c => new
                    {
                        IdClasse = c.Int(nullable: false),
                        IdCours = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdClasse, t.IdCours })
                .ForeignKey("dbo.Classes", t => t.IdClasse, cascadeDelete: true)
                .ForeignKey("dbo.Cours", t => t.IdCours, cascadeDelete: true)
                .Index(t => t.IdClasse)
                .Index(t => t.IdCours);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OTPCodes", "IdUtilisateur", "dbo.Utilisateurs");
            DropForeignKey("dbo.OTPCodes", "Utilisateurs_Id", "dbo.Utilisateurs");
            DropForeignKey("dbo.Etudiants", "Classes_Id", "dbo.Classes");
            DropForeignKey("dbo.ClassesCours", "IdCours", "dbo.Cours");
            DropForeignKey("dbo.ClassesCours", "IdClasse", "dbo.Classes");
            DropForeignKey("dbo.CoursMatieres", "IdMatiere", "dbo.Matieres");
            DropForeignKey("dbo.CoursMatieres", "IdCours", "dbo.Cours");
            DropForeignKey("dbo.ProfesseursMatieres", "IdMatiere", "dbo.Matieres");
            DropForeignKey("dbo.ProfesseursMatieres", "IdProfesseur", "dbo.Professeurs");
            DropForeignKey("dbo.ProfesseursClasses", "IdClasse", "dbo.Classes");
            DropForeignKey("dbo.ProfesseursClasses", "IdProfesseur", "dbo.Professeurs");
            DropForeignKey("dbo.Notes", "Matieres_Id", "dbo.Matieres");
            DropForeignKey("dbo.Notes", "IdMatiere", "dbo.Matieres");
            DropForeignKey("dbo.Notes", "IdEtudiant", "dbo.Etudiants");
            DropForeignKey("dbo.Notes", "Etudiants_Id", "dbo.Etudiants");
            DropForeignKey("dbo.Etudiants", "IdClasse", "dbo.Classes");
            DropIndex("dbo.ClassesCours", new[] { "IdCours" });
            DropIndex("dbo.ClassesCours", new[] { "IdClasse" });
            DropIndex("dbo.CoursMatieres", new[] { "IdMatiere" });
            DropIndex("dbo.CoursMatieres", new[] { "IdCours" });
            DropIndex("dbo.ProfesseursMatieres", new[] { "IdMatiere" });
            DropIndex("dbo.ProfesseursMatieres", new[] { "IdProfesseur" });
            DropIndex("dbo.ProfesseursClasses", new[] { "IdClasse" });
            DropIndex("dbo.ProfesseursClasses", new[] { "IdProfesseur" });
            DropIndex("dbo.OTPCodes", new[] { "Utilisateurs_Id" });
            DropIndex("dbo.OTPCodes", new[] { "IdUtilisateur" });
            DropIndex("dbo.Etudiants", new[] { "Classes_Id" });
            DropIndex("dbo.Etudiants", new[] { "IdClasse" });
            DropIndex("dbo.Notes", new[] { "Matieres_Id" });
            DropIndex("dbo.Notes", new[] { "Etudiants_Id" });
            DropIndex("dbo.Notes", new[] { "IdEtudiant" });
            DropIndex("dbo.Notes", new[] { "IdMatiere" });
            DropTable("dbo.ClassesCours");
            DropTable("dbo.CoursMatieres");
            DropTable("dbo.ProfesseursMatieres");
            DropTable("dbo.ProfesseursClasses");
            DropTable("dbo.Utilisateurs");
            DropTable("dbo.OTPCodes");
            DropTable("dbo.Professeurs");
            DropTable("dbo.Etudiants");
            DropTable("dbo.Notes");
            DropTable("dbo.Matieres");
            DropTable("dbo.Cours");
            DropTable("dbo.Classes");
        }
    }
}
