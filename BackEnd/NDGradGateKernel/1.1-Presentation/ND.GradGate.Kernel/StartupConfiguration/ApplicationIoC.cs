using ND.GradGate.Kernel.Application.Applicants;
using ND.GradGate.Kernel.Application.Applicants.Actions;
using ND.GradGate.Kernel.Application.Applicants.Interfaces;
using ND.GradGate.Kernel.Application.Applicants.Interfaces.Actions;
using ND.GradGate.Kernel.Application.Faculty.Actions;
using ND.GradGate.Kernel.Application.Faculty.Interfaces.Actions;
using ND.GradGate.Kernel.Application.Facultys;
using ND.GradGate.Kernel.Application.Facultys.Actions;
using ND.GradGate.Kernel.Application.Facultys.Interfaces;
using ND.GradGate.Kernel.Application.Facultys.Interfaces.Actions;
using ND.GradGate.Kernel.Application.Settings;
using ND.GradGate.Kernel.Application.Settings.Actions;
using ND.GradGate.Kernel.Application.Settings.Interfaces;
using ND.GradGate.Kernel.Application.Settings.Interfaces.Actions;

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
            services.AddSingleton<IUpdateApplicantStatusAndReviewerAction, UpdateApplicantStatusAndReviewerAction>();
            services.AddSingleton<ICreateApplicantInfoAction, CreateApplicantInfoAction>();
            services.AddSingleton<IDeleteApplicantInfoByIdAction, DeleteApplicantInfoByIdAction>();

            services.AddSingleton<IGetAllFacultyAction, GetAllFacultyAction>();
            services.AddSingleton<IGetFacultyInfoByIdAction, GetFacultyInfoByIdAction>();
            services.AddSingleton<IGetFacultysInfoByNameAction, GetFacultysInfoByNameAction>();


            services.AddSingleton<IGetSettingByKeyAction, GetSettingByKeyAction>();
            services.AddSingleton<IGetAllSettingsAction, GetAllSettingsAction>();
            services.AddSingleton<IAddOrUpdateSettingAction, AddOrUpdateSettingAction>();



            #endregion
            #region Application
            services.AddSingleton<IApplicantApplication, ApplicantApplication>();
            services.AddSingleton<IFacultyApplication, FacultyApplication>();
            services.AddSingleton<ISettingApplication, SettingApplication>();

            #endregion
        }

    }
}
