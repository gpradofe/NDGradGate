using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ND.GradGate.Kernel.DataAccess.Context;
using ND.GradGate.Kernel.DataAccess.Persistent.Repositories;
using ND.GradGate.Kernel.DataAccess.Repositories.Interfaces;
using ND.GradGate.Kernel.Domain.FacultyData;

namespace ND.GradGate.Kernel.DataAccess.Repositories
{
    public class FacultyRepository : AbstractRepository<Faculty, int>, IFacultyRepository
    {
        #region Attributes
        private readonly ILogger<FacultyRepository> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        #endregion

        #region Constructor
        public FacultyRepository(ILogger<FacultyRepository> logger,
                                            IServiceScopeFactory scopeFactory) : base(logger, scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }
        #endregion

        #region Methods
        public async Task<Faculty> GetByFacultyIdAsync(int Id)
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    KernelGradGateContext dbContext = scope.ServiceProvider.GetRequiredService<KernelGradGateContext>();
                    DbSet<Faculty> dataset = dbContext.Set<Faculty>();

                    var Faculty = await dataset
                                        .FirstOrDefaultAsync(a => a.Ref == Id);

                    return Faculty;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }


        public async Task<List<Faculty>> GetByFacultyNameAsync(string firstName, string lastName)
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    KernelGradGateContext dbContext = scope.ServiceProvider.GetRequiredService<KernelGradGateContext>();
                    DbSet<Faculty> dataset = dbContext.Set<Faculty>();

                    IQueryable<Faculty> FacultyQuery = dataset;

                    if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
                    {
                        FacultyQuery = FacultyQuery.Where(a => a.FirstName == firstName && a.LastName == lastName);
                    }
                    else if (!string.IsNullOrEmpty(firstName))
                    {
                        FacultyQuery = FacultyQuery.Where(a => a.FirstName == firstName);
                    }
                    else if (!string.IsNullOrEmpty(lastName))
                    {
                        FacultyQuery = FacultyQuery.Where(a => a.LastName == lastName);
                    }

                    var Facultys = await FacultyQuery.ToListAsync();

                    return Facultys;
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
