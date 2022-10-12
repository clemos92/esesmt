using ESESMT.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESESMT.Infra.Data.Mapping
{
    public class CompletedChecklistMap : IEntityTypeConfiguration<CompletedChecklist>
    {
        public void Configure(EntityTypeBuilder<CompletedChecklist> builder)
        {
            builder.ToTable("CompletedChecklist");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.CreationDate).HasColumnType("datetime").HasColumnName("CreationDate").IsRequired();
            builder.Property(x => x.ChecklistId).HasColumnName("ChecklistId").IsRequired();
            builder.HasOne(x => x.Checklist).WithMany(x => x.CompletedChecklists).HasForeignKey("ChecklistId");
            builder.HasMany(p => p.CompletedChecklistItems).WithOne(p => p.CompletedChecklist);
        }
    }
}
