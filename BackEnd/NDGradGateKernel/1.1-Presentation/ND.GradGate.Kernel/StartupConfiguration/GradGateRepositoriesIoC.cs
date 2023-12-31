﻿using ND.GradGate.Kernel.DataAccess.Repositories;
using ND.GradGate.Kernel.DataAccess.Repositories.Interfaces;

namespace ND.GradGate.Kernel.StartupConfiguration
{
    #region Methods
    public static class GradGateRepositoriesIoC
    {
        #region Methods
        public static void AddGradGateRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IApplicantRepository), typeof(ApplicantRepository));
            services.AddTransient(typeof(IFacultyRepository), typeof(FacultyRepository));
            services.AddTransient(typeof(ISettingRepository), typeof(SettingRepository));
            services.AddTransient(typeof(ICommentRepository), typeof(CommentRepository));

        }
        #endregion
    }
    #endregion

}
