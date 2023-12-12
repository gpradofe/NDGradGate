using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ND.GradGate.Kernel.DataAccess.Context;
using ND.GradGate.Kernel.DataAccess.Persistent.Repositories;
using ND.GradGate.Kernel.DataAccess.Repositories.Interfaces;
using ND.GradGate.Kernel.Domain.ApplicantData;

namespace ND.GradGate.Kernel.DataAccess.Repositories
{
    public class ApplicantRepository : AbstractRepository<Applicant, int>, IApplicantRepository
    {
        #region Attributes
        private readonly ILogger<ApplicantRepository> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        #endregion

        #region Constructor
        public ApplicantRepository(ILogger<ApplicantRepository> logger,
                                            IServiceScopeFactory scopeFactory) : base(logger, scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }
        #endregion

        #region Methods
        public async Task<Applicant> GetByApplicantIdAsync(int Id)
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    KernelGradGateContext dbContext = scope.ServiceProvider.GetRequiredService<KernelGradGateContext>();
                    DbSet<Applicant> dataset = dbContext.Set<Applicant>();

                    var applicantQuery = await dataset
                                            .Include(a => a.AcademicHistories)
                                            .Include(a => a.ReviewerAssignments)
                                            .Include(a => a.PotentialAdvisors)
                                            .Include(a => a.ApplicantAttributes)
                                            .FirstOrDefaultAsync(a => a.Id == Id);


                    return applicantQuery;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        public async Task<List<Applicant>> GetByApplicantIdAsync(List<int> ids)
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    KernelGradGateContext dbContext = scope.ServiceProvider.GetRequiredService<KernelGradGateContext>();
                    DbSet<Applicant> dataset = dbContext.Set<Applicant>();

                    var applicantQuery = await dataset
                                            .Include(a => a.AcademicHistories)
                                            .Include(a => a.ReviewerAssignments)
                                            .Include(a => a.PotentialAdvisors)
                                            .Include(a => a.ApplicantAttributes)
                                                            .Where(a => ids.Contains(a.Id)).ToListAsync();


                    return applicantQuery;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<List<Applicant>> GetByApplicantNameAsync(string firstName, string lastName)
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    KernelGradGateContext dbContext = scope.ServiceProvider.GetRequiredService<KernelGradGateContext>();
                    DbSet<Applicant> dataset = dbContext.Set<Applicant>();

                    IQueryable<Applicant> applicantQuery = dataset
                                            .Include(a => a.AcademicHistories)
                                            .Include(a => a.ReviewerAssignments)
                                            .Include(a => a.PotentialAdvisors)
                                            .Include(a => a.ApplicantAttributes);


                    if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
                    {
                        applicantQuery = applicantQuery.Where(a => a.FirstName == firstName && a.LastName == lastName);
                    }
                    else if (!string.IsNullOrEmpty(firstName))
                    {
                        applicantQuery = applicantQuery.Where(a => a.FirstName == firstName);
                    }
                    else if (!string.IsNullOrEmpty(lastName))
                    {
                        applicantQuery = applicantQuery.Where(a => a.LastName == lastName);
                    }

                    var applicants = await applicantQuery.ToListAsync();

                    return applicants;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        public async Task<List<Applicant>> GetAllApplicants()
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    KernelGradGateContext dbContext = scope.ServiceProvider.GetRequiredService<KernelGradGateContext>();
                    DbSet<Applicant> dataset = dbContext.Set<Applicant>();

                    IQueryable<Applicant> applicantQuery = dataset
                                            .Include(a => a.AcademicHistories)
                                            .Include(a => a.ReviewerAssignments)
                                            .Include(a => a.PotentialAdvisors)
                                            .Include(a => a.ApplicantAttributes);


                    var applicants = await applicantQuery.ToListAsync();

                    return applicants;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        public async Task UpdateApplicantAndRelatedEntitiesAsync(List<Applicant> applicantsToUpdate)
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    KernelGradGateContext dbContext = scope.ServiceProvider.GetRequiredService<KernelGradGateContext>();
                    var applicantIds = applicantsToUpdate.Select(a => a.Id).ToList();
                    DbSet<Applicant> dataset = dbContext.Set<Applicant>();

                    // Load all applicants with their related entities in one query
                    var existingApplicants = await dataset
                                            .Include(a => a.AcademicHistories)
                                            .Include(a => a.ReviewerAssignments)
                                            .Include(a => a.PotentialAdvisors)
                                            .Include(a => a.ApplicantAttributes)
                                                            .Where(a => applicantIds.Contains(a.Id)).ToListAsync();

                    foreach (var applicant in applicantsToUpdate)
                    {
                        var existingApplicant = existingApplicants.FirstOrDefault(a => a.Id == applicant.Id);

                        if (existingApplicant != null)
                        {
                            dbContext.Entry(existingApplicant).CurrentValues.SetValues(applicant);

                            foreach (var reviewerAssignment in applicant.ReviewerAssignments)
                            {
                                var existingAssignment = existingApplicant.ReviewerAssignments
                                    .FirstOrDefault(ra => ra.FacultyId == reviewerAssignment.FacultyId && ra.ApplicantId == reviewerAssignment.ApplicantId);

                                if (existingAssignment != null)
                                {
                                    dbContext.Entry(existingAssignment).CurrentValues.SetValues(reviewerAssignment);
                                }
                                else
                                {
                                    existingApplicant.ReviewerAssignments.Add(reviewerAssignment);
                                }
                            }

                        }
                    }

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
