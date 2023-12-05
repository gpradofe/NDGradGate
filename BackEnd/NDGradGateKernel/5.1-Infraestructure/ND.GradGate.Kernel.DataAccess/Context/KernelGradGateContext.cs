using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ND.GradGate.Kernel.DataAccess.Persistent.Extensions;
using Microsoft.Extensions.Logging;

namespace ND.GradGate.Kernel.DataAccess.Context
{
    public class KernelGradGateContext : DbContext
    {
        #region Attributes
        public static string SCHEMA => "gradgate";
        #endregion
        #region Constructor
        public KernelGradGateContext(DbContextOptions<KernelGradGateContext> options) : base(options)
        {

        }
        #endregion
        #region Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddDefaultSchema(SCHEMA);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            base.OnConfiguring(optionsBuilder);
        }
        #endregion
    }
}
