using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ND.GradGate.Kernel.Service.Core.APIBase.StartupConfiguration.DatabaseConfiguration
{
    public static class Database
    {
        public static void InitializeSQLDatabase<TContext>(this IServiceCollection services, IConfiguration configuration)
            where TContext : DbContext
        {
            string connectionString = GetConnectionString(configuration);
            services.AddDbContext<TContext>(options => options.UseNpgsql(connectionString));

            // Add this line to register TContext as a DbContext
            services.AddScoped<DbContext, TContext>();
        }
        internal static string GetConnectionString(IConfiguration configuration)
        {
            var dbSettings = configuration.GetSection("DbSettings");
            string server = dbSettings["Server"];
            int port = dbSettings.GetValue<int>("Port");
            string username = dbSettings["Username"];
            string password = dbSettings["Password"];
            string database = dbSettings["Database"];

            string connectionString = $"Host={server};Database={database};Username={username};Password={password};Port={port}";
            return connectionString;
        }


    }
}
