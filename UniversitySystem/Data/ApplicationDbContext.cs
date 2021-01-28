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
                    StudentId = 1,
                },
                new Semester
                {
                    Id = 2,
                    CreatedOn = new DateTime(2021, 01, 21),
                    SemesterName = "Semester 2",
                    StartDate = new DateTime(2020, 03, 16),
                    EndDate = new DateTime(2020, 06, 16),
                    StudentId = 1,
                });
            builder.Entity<Score>().HasData(
                new Score
                {
                    Id = 1,
                    CreatedOn = new DateTime(2021, 01, 21),
                    DisiplineName = "Math",
                    ProfessorName = "Newton",
                    ScoreNumber = 4,
                    SemesterId = 1,
                },
                new Score
                {
                    Id = 2,
                    CreatedOn = new DateTime(2021, 01, 21),
                    DisiplineName = "History",
                    ProfessorName = "Ivanov",
                    ScoreNumber = 5,
                    SemesterId = 1,
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

        public DbSet<UniversitySystem.ViewModels.DisciplineViewModel> DisciplineViewModel { get; set; }
    }
}
