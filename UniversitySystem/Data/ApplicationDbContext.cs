using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreTemplate.Data.Common.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Models;
using UniversitySystem.ViewModels;

namespace UniversitySystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Discipline> Disciplines { get; set; }

        public DbSet<Score> Scores { get; set; }

        public DbSet<Semester> Semesters { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<StudentSemester> StudentSemesters { get; set; }

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<StudentSemester>().HasKey(s => new { s.StudentId, s.SemesterId });

            this.ConfigureUserIdentityRelations(builder);

            var entityTypes = builder.Model.GetEntityTypes().ToList();

            // Disable cascade delete
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            //Some default data
            builder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    FirstName = "Pesho",
                    LastName = "Peshev",
                    DateOfBirth = new DateTime(1992, 07, 26),
                    CreatedOn = new DateTime(2021, 01, 21),
                });
            builder.Entity<Semester>().HasData(
                new Semester
                {
                    Id = 1,
                    CreatedOn = new DateTime(2021, 01, 21),
                    SemesterName = "Semester 1",
                    StartDate = new DateTime(2019, 09, 20),
                    EndDate = new DateTime(2020, 03, 16),
                },
                new Semester
                {
                    Id = 2,
                    CreatedOn = new DateTime(2021, 01, 21),
                    SemesterName = "Semester 2",
                    StartDate = new DateTime(2020, 03, 16),
                    EndDate = new DateTime(2020, 06, 16),
                });

            builder.Entity<StudentSemester>().HasData(
                new StudentSemester
                {
                    StudentId = 1,
                    SemesterId = 1,
                },
                new StudentSemester
                {
                    StudentId = 1,
                    SemesterId = 2,
                });
            builder.Entity<Discipline>().HasData(
                new Discipline
                {
                    Id = 1,
                    CreatedOn = new DateTime(2021, 01, 31),
                    DisciplineName = "Math",
                    ProfessorName = "Newton",
                    SemesterId = 1,
                },
                new Discipline
                {
                    Id = 2,
                    CreatedOn = new DateTime(2021, 01, 31),
                    DisciplineName = "Programming",
                    ProfessorName = "Mustain",
                    SemesterId = 1,
                });
            builder.Entity<Score>().HasData(
                new Score
                {
                    Id = 1,
                    ScoreNumber = 5,
                    DisciplineId = 1,
                },
                new Score
                {
                    Id = 2,
                    ScoreNumber = 6,
                    DisciplineId = 2,
                });
        }

        // Applies configurations
        private void ConfigureUserIdentityRelations(ModelBuilder builder)
             => builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
