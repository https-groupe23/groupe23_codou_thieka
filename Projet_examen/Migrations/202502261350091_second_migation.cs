namespace Projet_examen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second_migation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notes", "IdEtudiant", "dbo.Etudiants");
            DropForeignKey("dbo.Notes", "IdMatiere", "dbo.Matieres");
            DropForeignKey("dbo.Etudiants", "IdClasse", "dbo.Classes");
            DropForeignKey("dbo.OTPCodes", "IdUtilisateur", "dbo.Utilisateurs");
            AddForeignKey("dbo.Notes", "IdEtudiant", "dbo.Etudiants", "Id");
            AddForeignKey("dbo.Notes", "IdMatiere", "dbo.Matieres", "Id");
            AddForeignKey("dbo.Etudiants", "IdClasse", "dbo.Classes", "Id");
            AddForeignKey("dbo.OTPCodes", "IdUtilisateur", "dbo.Utilisateurs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OTPCodes", "IdUtilisateur", "dbo.Utilisateurs");
            DropForeignKey("dbo.Etudiants", "IdClasse", "dbo.Classes");
            DropForeignKey("dbo.Notes", "IdMatiere", "dbo.Matieres");
            DropForeignKey("dbo.Notes", "IdEtudiant", "dbo.Etudiants");
            AddForeignKey("dbo.OTPCodes", "IdUtilisateur", "dbo.Utilisateurs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Etudiants", "IdClasse", "dbo.Classes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Notes", "IdMatiere", "dbo.Matieres", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Notes", "IdEtudiant", "dbo.Etudiants", "Id", cascadeDelete: true);
        }
    }
}
