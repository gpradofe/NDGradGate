using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ND.GradGate.Kernel.DataAccess.Context;
using ND.GradGate.Kernel.DataAccess.Persistent.Repositories;
using ND.GradGate.Kernel.DataAccess.Repositories.Interfaces;
using ND.GradGate.Kernel.Domain.Settings;

namespace ND.GradGate.Kernel.DataAccess.Repositories
{
    public class SettingRepository : AbstractRepository<Setting, int>, ISettingRepository
    {
        #region Attributes
        private readonly ILogger<SettingRepository> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        #endregion

        #region Constructor
        public SettingRepository(ILogger<SettingRepository> logger,
                                 IServiceScopeFactory scopeFactory) : base(logger, scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }
        #endregion

        #region Methods
        public async Task<List<Setting>> GetAllSettingsAsync()
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    KernelGradGateContext dbContext = scope.ServiceProvider.GetRequiredService<KernelGradGateContext>();
                    DbSet<Setting> dataset = dbContext.Set<Setting>();

                    var settings = await dataset.ToListAsync();
                    return settings;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<Setting> GetSettingByKeyAsync(string settingKey)
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    KernelGradGateContext dbContext = scope.ServiceProvider.GetRequiredService<KernelGradGateContext>();
                    DbSet<Setting> dataset = dbContext.Set<Setting>();

                    var setting = await dataset.FirstOrDefaultAsync(s => s.SettingKey == settingKey);
                    return setting;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        #endregion
    }
}
