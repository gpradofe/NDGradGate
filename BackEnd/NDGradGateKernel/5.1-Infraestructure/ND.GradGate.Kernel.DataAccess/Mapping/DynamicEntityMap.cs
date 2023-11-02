using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ND.GradGate.Kernel.Domain.EAV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.DataAccess.Mapping
{
    public class DynamicEntityMap : IEntityTypeConfiguration<DynamicEntity>
    {
        public void Configure(EntityTypeBuilder<DynamicEntity> builder)
        {
            builder.ToTable("dynamic_entities");

            builder.HasKey(de => de.Id).HasName("pk_dynamic_entities");
            builder.Property(de => de.Id).HasColumnName("entity_id").ValueGeneratedOnAdd();

            builder.Property(de => de.EntityName).HasColumnName("entity_name").IsRequired();

            builder.HasMany(de => de.Attributes)
                   .WithOne(a => a.DynamicEntity)
                   .HasForeignKey(a => a.EntityId)
                   .HasConstraintName("fk_attributes_dynamic_entities");
        }
    }
}
