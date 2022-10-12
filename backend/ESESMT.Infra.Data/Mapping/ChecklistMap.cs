using ESESMT.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESESMT.Infra.Data.Mapping
{
    public class ChecklistMap : IEntityTypeConfiguration<Checklist>
    {
        public void Configure(EntityTypeBuilder<Checklist> builder)
        {
            builder.ToTable("Checklist");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Description).HasColumnName("Description").HasMaxLength(255).IsRequired();
            builder.Property(x => x.IsActive).HasColumnName("IsActive").IsRequired().HasDefaultValue(true);
            builder.Property(x => x.ChecklistTypeId).HasColumnName("ChecklistTypeId").IsRequired();
            builder.HasOne(x => x.ChecklistType).WithMany(x => x.Checklists).HasForeignKey("ChecklistTypeId");
            builder.HasMany(p => p.ChecklistItems).WithOne(p => p.Checklist);
        }
    }
}
