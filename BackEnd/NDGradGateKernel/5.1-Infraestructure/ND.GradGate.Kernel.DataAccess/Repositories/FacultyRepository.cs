using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ND.GradGate.Kernel.DataAccess.Context;
using ND.GradGate.Kernel.DataAccess.Persistent.Repositories;
using ND.GradGate.Kernel.DataAccess.Repositories.Interfaces;
using ND.GradGate.Kernel.Domain.ApplicantData;
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
        public async Task<List<Faculty>> SaveOrUpdateMultipleAsync(List<Faculty> faculties)
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    DbContext dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();
                    DbSet<Faculty> dataset = dbContext.Set<Faculty>();

                    foreach (var faculty in faculties)
                    {
                        var id = faculty.Id;
                        var result = await dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
                        if (result == null || string.IsNullOrEmpty(id.ToString()) || id.Equals("0"))
                        {
                            await dataset.AddAsync(faculty);
                        }
                        else
                        {
                            dbContext.Entry(result).CurrentValues.SetValues(faculty);
                        }
                    }

                    await dbContext.SaveChangesAsync();
                    return faculties;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when saving/updating faculties");
                throw;
            }
        }
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

        public async Task<List<Faculty>> GetByFacultyIdAsync(List<int> facultyId)
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    KernelGradGateContext dbContext = scope.ServiceProvider.GetRequiredService<KernelGradGateContext>();
                    DbSet<Faculty> dataset = dbContext.Set<Faculty>();

                    var applicantQuery = await dataset.Include(f => f.PotentialAdvisors)
                                                      .Include(f => f.ReviewerAssignments)
                                                      .Include(f => f.Comments)
                                                      .Where(f => facultyId.Contains(f.Id))
                                                      .ToListAsync();

                    return applicantQuery;
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
        public async Task UpdateFacultyAndRelatedEntitiesAsync(List<Faculty> facultiesToUpdate)
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    KernelGradGateContext dbContext = scope.ServiceProvider.GetRequiredService<KernelGradGateContext>();
                    var facultyIds = facultiesToUpdate.Select(a => a.Id).ToList();
                    DbSet<Faculty> dataset = dbContext.Set<Faculty>();

                    var existingFaculties = await dataset
                                            .Include(a => a.ReviewerAssignments)
                                            .Include(a => a.PotentialAdvisors)
                                            .Include(a => a.Comments)
                                                            .Where(a => facultyIds.Contains(a.Id)).ToListAsync();

                    foreach (var faculty in facultiesToUpdate)
                    {
                        var existingFaculty = existingFaculties.FirstOrDefault(a => a.Id == faculty.Id);

                        if (existingFaculty != null)
                        {
                            dbContext.Entry(existingFaculty).CurrentValues.SetValues(faculty);

                            foreach (var potentialAdvisors in faculty.PotentialAdvisors)
                            {
                                var existingPotentialAdvisors = existingFaculty.PotentialAdvisors
                                    .FirstOrDefault(ra => ra.FacultyId == potentialAdvisors.FacultyId && ra.ApplicantId == potentialAdvisors.ApplicantId);

                                if (existingPotentialAdvisors != null)
                                {
                                    dbContext.Entry(potentialAdvisors).CurrentValues.SetValues(potentialAdvisors);
                                }
                                else
                                {
                                    existingFaculty.PotentialAdvisors.Add(potentialAdvisors);
                                }
                            }
                            foreach (var comment in faculty.Comments)
                            {
                                var existingComment = existingFaculty.Comments
                                    .FirstOrDefault(ra => ra.FacultyId == comment.FacultyId && ra.ApplicantId == comment.ApplicantId);

                                if (existingComment != null)
                                {
                                    dbContext.Entry(comment).CurrentValues.SetValues(comment);
                                }
                                else
                                {
                                    existingFaculty.Comments.Add(comment);
                                }
                            }

                        }
                    }
                    _logger.LogInformation("Updating");
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating applicants and related entities");
                throw;
            }
        }

        #endregion
    }
}
