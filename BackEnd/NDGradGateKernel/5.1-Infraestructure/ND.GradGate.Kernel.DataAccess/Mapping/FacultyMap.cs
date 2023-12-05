using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ND.GradGate.Kernel.Domain.Faculty;

namespace ND.GradGate.Kernel.DataAccess.Mapping
{
    public class FacultyMap : IEntityTypeConfiguration<Faculty>
    {
        public void Configure(EntityTypeBuilder<Faculty> builder)
        {
            builder.ToTable("faculty");

            // Key
            builder.HasKey(f => f.Id).HasName("pk_faculty");
            builder.Property(f => f.Id).HasColumnName("faculty_id").ValueGeneratedOnAdd();

            // Fields
            builder.Property(f => f.Email).HasColumnName("email");
            builder.Property(f => f.Name).HasColumnName("name").IsRequired();
            builder.Property(f => f.IsAdmin).HasColumnName("is_admin").IsRequired();
            builder.Property(f => f.IsReviewer).HasColumnName("is_reviewer").IsRequired();
            builder.Property(f => f.Field).HasColumnName("field");

            // Relationships
            builder.HasMany(f => f.PotentialAdvisors)
                   .WithOne(pa => pa.Faculty)
                   .HasForeignKey(pa => pa.FacultyId);

            builder.HasMany(f => f.ReviewerAssignments)
                   .WithOne(ra => ra.Faculty)
                   .HasForeignKey(ra => ra.FacultyId);

            builder.HasMany(f => f.Comments)
                   .WithOne(c => c.Faculty)
                   .HasForeignKey(c => c.FacultyId);
        }
    }

}
