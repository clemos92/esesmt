using ESESMT.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESESMT.Infra.Data.Mapping
{
    public class CompletedChecklistItemMap : IEntityTypeConfiguration<CompletedChecklistItem>
    {
        public void Configure(EntityTypeBuilder<CompletedChecklistItem> builder)
        {
            builder.ToTable("CompletedChecklistItem");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Observation).HasColumnName("Observation").HasMaxLength(255);
            builder.Property(x => x.Status).HasColumnName("Status").IsRequired().HasDefaultValue(true);
            builder.Property(x => x.CompletedChecklistId).HasColumnName("CompletedChecklistId").IsRequired();
            builder.HasOne(x => x.CompletedChecklist).WithMany(x => x.CompletedChecklistItems).HasForeignKey("CompletedChecklistId").IsRequired();
            builder.Property(x => x.ChecklistItemId).HasColumnName("ChecklistItemId").IsRequired();
            builder.HasOne(x => x.ChecklistItem).WithMany(x => x.CompletedChecklistItems).HasForeignKey("ChecklistItemId").IsRequired();
        }
    }
}
