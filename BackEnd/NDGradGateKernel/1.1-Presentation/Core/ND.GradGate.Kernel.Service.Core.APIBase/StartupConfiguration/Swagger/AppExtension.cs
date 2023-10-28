using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace ND.GradGate.Kernel.Service.Core.APIBase.StartupConfiguration.Swagger
{
    public static class AppExtensions
    {
        #region Methods
        public static IApplicationBuilder AddSwaggers(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(opt =>
            {
                opt.RoutePrefix = string.Empty;

                var basePath = string.IsNullOrEmpty(opt.RoutePrefix) ? "." : "..";

                foreach (var desc in provider.ApiVersionDescriptions)
                {
                    opt.SwaggerEndpoint($"{basePath}/swagger/{desc.GroupName}/swagger.json", $"v{desc.GroupName}");
                }
                opt.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);

            });
            return app;
        }
        #endregion

    }
}
