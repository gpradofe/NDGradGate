﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ND.GradGate.Kernel.Service.Core.APIBase.StartupConfiguration.Filters;
using ND.GradGate.Kernel.Service.Core.APIBase.StartupConfiguration.Swagger;

namespace ND.GradGate.Kernel.Service.Core.APIBase.StartupConfiguration
{
    public static class StartupExtension
    {
        #region Methods

        public static void AddExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogs(configuration);
            services.AddFilters();
            services.AddSwaggerUI();
            services.AddHealthChecks();

        }
        public static void AddLogs(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
        }
        public static void AddFilters(this IServiceCollection services)
        {
            services.AddScoped<ApiExceptionFilter>();
            services.AddMvcCore(options => options.Filters.Add(new ActionFilter()));
        }
        public static void AddSwaggerUI(this IServiceCollection services)
        {
            services.AddSwaggers();
        }
        #endregion

    }
}
