using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ND.GradGate.Kernel.Domain.Faculty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.DataAccess.Mapping
{
    public class FacultyAdministrationMap : IEntityTypeConfiguration<FacultyAdministration>
    {
        public void Configure(EntityTypeBuilder<FacultyAdministration> builder)
        {
            builder.ToTable("faculty_administration");

            // Key
            builder.HasKey(fa => fa.Id).HasName("pk_faculty_administration");
            builder.Property(fa => fa.Id).HasColumnName("admin_id").ValueGeneratedOnAdd();

            // Fields
            builder.Property(fa => fa.Name).HasColumnName("name").IsRequired();
            builder.Property(fa => fa.Title).HasColumnName("title").IsRequired();
            builder.Property(fa => fa.Email).HasColumnName("email").IsRequired();

            // Relationships
            builder.HasMany(fa => fa.ReviewerAssignments)
                   .WithOne(ra => ra.FacultyAdministration)
                   .HasForeignKey(ra => ra.AdminId)
                   .HasConstraintName("fk_reviewer_assignment_faculty_administration");
        }
    }

}
