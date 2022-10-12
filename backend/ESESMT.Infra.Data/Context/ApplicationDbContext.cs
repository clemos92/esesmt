using ESESMT.Infra.Shared.Notifications;
using Microsoft.EntityFrameworkCore;
using ESESMT.Domain.Entities;
using ESESMT.Infra.Data.Mapping;
using System.Linq;
using System.Reflection;

namespace ESESMT.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ChecklistType> ChecklistTypes { get; set; }
        public DbSet<Checklist> Checklists { get; set; }
        public DbSet<ChecklistItem> ChecklistItems { get; set; }
        public DbSet<CompletedChecklist> CompletedChecklists { get; set; }
        public DbSet<CompletedChecklistItem> CompletedChecklistItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ChecklistType>(new ChecklistTypeMap().Configure);
            modelBuilder.Entity<Checklist>(new ChecklistMap().Configure);
            modelBuilder.Entity<ChecklistItem>(new ChecklistItemMap().Configure);
            modelBuilder.Entity<CompletedChecklist>(new CompletedChecklistMap().Configure);
            modelBuilder.Entity<CompletedChecklistItem>(new CompletedChecklistItemMap().Configure);

            var entites = Assembly
                .Load("ESESMT.Domain")
                .GetTypes()
                .Where(w => w.Namespace == "ESESMT.Domain.Entities" && w.BaseType.BaseType == typeof(Notifiable));

            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            foreach (var item in entites)
            {
                modelBuilder.Entity(item).Ignore(nameof(Notifiable.Invalid));
                modelBuilder.Entity(item).Ignore(nameof(Notifiable.Valid));
                modelBuilder.Entity(item).Ignore(nameof(Notifiable.ValidationResult));
            }
        }
    }
}