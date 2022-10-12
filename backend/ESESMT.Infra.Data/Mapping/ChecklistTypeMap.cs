using ESESMT.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESESMT.Infra.Data.Mapping
{
    public class ChecklistTypeMap : IEntityTypeConfiguration<ChecklistType>
    {
        public void Configure(EntityTypeBuilder<ChecklistType> builder)
        {
            builder.ToTable("ChecklistType");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(255).IsRequired();
            builder.Property(x => x.IsActive).HasColumnName("IsActive").IsRequired().HasDefaultValue(true);
            builder.HasMany(p => p.Checklists).WithOne(p => p.ChecklistType);
        }
    }
}
