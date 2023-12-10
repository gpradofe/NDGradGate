using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ND.GradGate.Kernel.DataAccess.Context;
using ND.GradGate.Kernel.DataAccess.Persistent.Repositories;
using ND.GradGate.Kernel.DataAccess.Repositories.Interfaces;
using ND.GradGate.Kernel.Domain.Faculty;
using System.Xml.Linq;

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
                                        .FirstOrDefaultAsync(a => a.Id == Id);

                    return Faculty;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }


        public async Task<List<Faculty>> GetByFacultyNameAsync(string name)
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    KernelGradGateContext dbContext = scope.ServiceProvider.GetRequiredService<KernelGradGateContext>();
                    DbSet<Faculty> dataset = dbContext.Set<Faculty>();

                    IQueryable<Faculty> FacultyQuery = dataset;

                    if (!string.IsNullOrEmpty(name))
                    {
                        FacultyQuery = FacultyQuery.Where(a => a.Name == name);
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

        public async Task<List<Faculty>> GetAllFacultyAsync()
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    KernelGradGateContext dbContext = scope.ServiceProvider.GetRequiredService<KernelGradGateContext>();
                    DbSet<Faculty> dataset = dbContext.Set<Faculty>();

                    IQueryable<Faculty> FacultyQuery = dataset.Include(c => c.Comments)
                                                              .Include(r => r.ReviewerAssignments)
                                                              .Include(p => p.PotentialAdvisors);
                        


                    List<Faculty> listFaculty = await FacultyQuery.ToListAsync();

                    return listFaculty;
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
