﻿using ND.GradGate.Kernel.Application.Applicants;
using ND.GradGate.Kernel.Application.Applicants.Actions;
using ND.GradGate.Kernel.Application.Applicants.Interfaces;
using ND.GradGate.Kernel.Application.Applicants.Interfaces.Actions;

namespace ND.GradGate.Kernel.StartupConfiguration
{
    public static class ApplicationIoC
    {
        public static void AddGradGateApplications(this IServiceCollection services, IConfiguration configuration)
        {
            #region External Services
            #endregion

            #region Http

            #endregion
            #region Actions
            services.AddSingleton<IGetApplicantsInfoByNameAction, GetApplicantsInfoByNameAction>();
            services.AddSingleton<IGetApplicantInfoByIdAction, GetApplicantInfoByIdAction>();
            services.AddSingleton<IGetAllApplicantsAction, GetAllApplicantsAction>();
            services.AddSingleton<IUpdateApplicantInfoByIdAction, UpdateApplicantInfoByIdAction>();
            services.AddSingleton<ICreateApplicantInfoAction, CreateApplicantInfoAction>();
            services.AddSingleton<IDeleteApplicantInfoByIdAction, DeleteApplicantInfoByIdAction>();



            #endregion
            #region Application
            services.AddSingleton<IApplicantApplication, ApplicantApplication>();
            #endregion
        }

    }
}
