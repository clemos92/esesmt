using ESESMT.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESESMT.Infra.Data.Mapping
{
    public class ChecklistItemMap : IEntityTypeConfiguration<ChecklistItem>
    {
        public void Configure(EntityTypeBuilder<ChecklistItem> builder)
        {
            builder.ToTable("ChecklistItem");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(255).IsRequired();
            builder.Property(x => x.IsActive).HasColumnName("IsActive").IsRequired().HasDefaultValue(true);
            builder.Property(x => x.ChecklistId).HasColumnName("ChecklistId").IsRequired();
            builder.HasOne(x => x.Checklist).WithMany(x => x.ChecklistItems).HasForeignKey("ChecklistId").IsRequired();
        }
    }
}
