using Microsoft.AspNetCore.Mvc.ApiExplorer;
using ND.GradGate.Kernel.DataAccess.Context;
using ND.GradGate.Kernel.Service.Core.APIBase.StartupConfiguration;
using ND.GradGate.Kernel.Service.Core.APIBase.StartupConfiguration.DatabaseConfiguration;
using ND.GradGate.Kernel.Service.Core.APIBase.StartupConfiguration.Swagger;
using ND.GradGate.Kernel.StartupConfiguration;

namespace ND.GradGate.Kernel
{
    public class Startup
    {
        #region Constructor
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        #endregion
        #region Attributes
        public IConfiguration _configuration { get; }
        #endregion
        #region Methods
        //This method gets called by the runtime. Use this method to add services to the container.

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApiVersioning(ver =>
            {
                ver.ReportApiVersions = true;
                ver.AssumeDefaultVersionWhenUnspecified = true;
            });
            services.AddVersionedApiExplorer(ver =>
            {
                ver.SubstituteApiVersionInUrl = true;
            });
            services.AddExtensions(_configuration);

            services.AddMvc(setupAction =>
            {
                setupAction.EnableEndpointRouting = false;
            }).AddJsonOptions(jsonOptions =>
            {
                jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            services.InitializeSQLDatabase<KernelGradGateContext>(_configuration);
            services.AddGradGateRepositories();
            services.AddGradGateApplications(_configuration);
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                    });
            });

        }

        //This method gets called by the runtime. Use this method to add services to the container.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHealthChecks("/healthycheck");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.AddSwaggers(provider);
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());
        }
        #endregion

    }

}
