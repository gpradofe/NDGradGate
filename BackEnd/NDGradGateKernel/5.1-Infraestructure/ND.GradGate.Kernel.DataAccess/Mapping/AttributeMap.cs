using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ND.GradGate.Kernel.DataAccess.Mapping
{
    public class AttributeMap : IEntityTypeConfiguration<Domain.EAV.Attribute>
    {
        public void Configure(EntityTypeBuilder<Domain.EAV.Attribute> builder)
        {
            builder.ToTable("attributes");

            // Key
            builder.HasKey(a => a.Id).HasName("pk_attributes");
            builder.Property(a => a.Id).HasColumnName("attribute_id").ValueGeneratedOnAdd();

            // Fields
            builder.Property(a => a.EntityId).HasColumnName("entity_id").IsRequired();
            builder.Property(a => a.AttributeName).HasColumnName("attribute_name").IsRequired();
            builder.Property(a => a.AttributeType).HasColumnName("attribute_type").IsRequired();

            // Relationships
            builder.HasMany(a => a.ApplicationDataValues)
                   .WithOne(adv => adv.Attribute)
                   .HasForeignKey(adv => adv.AttributeId)
                   .HasConstraintName("fk_application_data_values_attributes");
        }
    }

}
