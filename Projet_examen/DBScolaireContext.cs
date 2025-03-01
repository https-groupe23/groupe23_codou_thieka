using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;


namespace Projet_examen
{
    internal class DBScolaireContext : DbContext
    {
        public DBScolaireContext() : base("ecoleConnect")
        {

        }

        public DbSet<Etudiants> etudiants { get; set; }
        public DbSet<Classes> classes { get; set; }
        public DbSet<Notes> notes { get; set; }
        public DbSet<Matieres> matieres { get; set; }
        public DbSet<Cours> cours { get; set; }
        public DbSet<Professeurs> professeurs { get; set; }
        public DbSet<Utilisateurs> utilisateurs { get; set; }
        public DbSet<OTPCodes> otpcodes { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Classes>()
               .HasMany(c => c.cours)
               .WithMany(c => c.classes)
               .Map(m =>
               {
                   m.ToTable("ClassesCours");
                   m.MapLeftKey("IdClasse");
                   m.MapRightKey("IdCours");
               });

            modelBuilder.Entity<Professeurs>()
                .HasMany(p => p.matieres)
                .WithMany(m => m.professeurs)
                .Map(m =>
                {
                    m.ToTable("ProfesseursMatieres");
                    m.MapLeftKey("IdProfesseur");
                    m.MapRightKey("IdMatiere");
                });

           
            modelBuilder.Entity<Professeurs>()
                .HasMany(p => p.classes)
                .WithMany(c => c.professeurs)
                .Map(m =>
                {
                    m.ToTable("ProfesseursClasses");
                    m.MapLeftKey("IdProfesseur"); 
                    m.MapRightKey("IdClasse");
                });

            modelBuilder.Entity<Cours>()
                .HasMany(c => c.matieres)
                .WithMany(m => m.cours)
                .Map(m =>
                {
                    m.ToTable("CoursMatieres");
                    m.MapLeftKey("IdCours");
                    m.MapRightKey("IdMatiere");
                });

            modelBuilder.Entity<Etudiants>()
                .HasRequired(e => e.classe)
                .WithMany()
                .HasForeignKey(e => e.IdClasse)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OTPCodes>()
                .HasRequired(e => e.utilisateur)
                .WithMany()
                .HasForeignKey(e => e.IdUtilisateur)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Notes>()
                .HasRequired(e => e.matiere)
                .WithMany()
                .HasForeignKey(e => e.IdMatiere)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Notes>()
                .HasRequired(e => e.etudiant)
                .WithMany()
                .HasForeignKey(e => e.IdEtudiant)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }


    }
}
