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
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("comments");

            // Key
            builder.HasKey(c => c.Id).HasName("pk_comments");
            builder.Property(c => c.Id).HasColumnName("comment_id").ValueGeneratedOnAdd();

            // Fields
            builder.Property(c => c.FacultyId).HasColumnName("faculty_id").IsRequired();
            builder.Property(c => c.ApplicantId).HasColumnName("applicant_id").IsRequired();
            builder.Property(c => c.Content).HasColumnName("content");
            builder.Property(c => c.Date).HasColumnName("date").IsRequired().HasDefaultValueSql("CURRENT_DATE");

            // Relationships
            builder.HasOne(c => c.Faculty)
                   .WithMany(f => f.Comments)
                   .HasForeignKey(c => c.FacultyId);

            builder.HasOne(c => c.Applicant)
                   .WithMany(a => a.Comments)
                   .HasForeignKey(c => c.ApplicantId);
        }
    }

}
