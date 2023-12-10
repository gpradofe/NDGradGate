using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ND.GradGate.Kernel.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.DataAccess.Mapping
{
    public class SettingMap : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.ToTable("settings");

            builder.HasKey(s => s.Id).HasName("pk_settings");
            builder.Property(s => s.Id).HasColumnName("id").ValueGeneratedOnAdd();

            builder.Property(s => s.SettingKey).HasColumnName("setting_key").IsRequired();
            builder.Property(s => s.SettingValue).HasColumnName("setting_value").IsRequired().HasColumnType("jsonb"); // Ensure correct column type for JSONB
        }
    }
}
