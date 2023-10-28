using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ND.GradGate.Kernel.DataAccess.Persistent.Extensions
{
    public static class ModelBuilderExtensions
    {
        #region Mapping
        public static void AddAllMapsConfiguration(this ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        public static void AddAllMapsConfiguration(this ModelBuilder modelBuilder, Assembly assembly)
            => modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        #endregion
        #region Schema
        public static void AddDefaultSchema(this ModelBuilder modelBuilder, string contextSchema)
            => modelBuilder.HasDefaultSchema(contextSchema);
        public static void AddSchema(this ModelBuilder modelBuilder, string schema)
            => modelBuilder.HasDefaultSchema(schema);

        #endregion
        #region Config
        public static void AddDefaultConfiguration(this ModelBuilder modelBuilder)
        {
            SetDecimalTypesPrecision();
            void SetDecimalTypesPrecision()
            {
                foreach (var property in modelBuilder.Model
                         .GetEntityTypes()
                         .SelectMany(t => t.GetProperties()
                         .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?))))
                {
                    property.SetColumnType("decimal(18,6)");
                }
            }
        }
        #endregion
    }
}
