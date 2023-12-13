using Microsoft.Extensions.Logging;
using ND.GradGate.Kernel.Application.Faculty.Interfaces.Actions;
using ND.GradGate.Kernel.Application.Facultys.Actions;
using ND.GradGate.Kernel.Application.Facultys.Interfaces;
using ND.GradGate.Kernel.Application.Facultys.Interfaces.Actions;
using ND.GradGate.Kernel.Domain.Contracts.Applicant;
using ND.GradGate.Kernel.Domain.Contracts.Faculty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Application.Facultys
{
    public class FacultyApplication : IFacultyApplication
    {
        #region Attributes
        private readonly ILogger<FacultyApplication> _logger;
        private readonly IGetFacultyInfoByIdAction _getFacultyInfoByIdAction;
        private readonly IGetFacultysInfoByNameAction _getFacultysInfoByNameAction;
        private readonly IGetAllFacultyAction _getAllFacultyAction;
        private readonly IGetAssignedApplicationsForReviewerAction _getAssignedApplicationsForReviewerAction;
        private readonly IAssignPotentialAdvisorsAndAddCommentsAction _assignPotentialAdvisorsAndAddCommentsAction;
        private readonly ICreateOrUpdateFacultyAction _createOrUpdateFacultyAction;
        #endregion

        #region Constructors
        public FacultyApplication(ILogger<FacultyApplication> logger,
                                    IGetFacultyInfoByIdAction getFacultyInfoByIdAction,
                                    IGetFacultysInfoByNameAction getFacultysInfoByNameAction,
                                    IGetAllFacultyAction getAllFacultyAction,
                                    IGetAssignedApplicationsForReviewerAction getAssignedApplicationsForReviewerAction,
                                    IAssignPotentialAdvisorsAndAddCommentsAction assignPotentialAdvisorsAndAddCommentsAction,
                                    ICreateOrUpdateFacultyAction createOrUpdateFacultyAction)
        {
            _logger = logger;
            _getFacultyInfoByIdAction = getFacultyInfoByIdAction;
            _getFacultysInfoByNameAction = getFacultysInfoByNameAction;
            _getAllFacultyAction = getAllFacultyAction;
            _getAssignedApplicationsForReviewerAction = getAssignedApplicationsForReviewerAction;
            _assignPotentialAdvisorsAndAddCommentsAction = assignPotentialAdvisorsAndAddCommentsAction;
            _createOrUpdateFacultyAction = createOrUpdateFacultyAction;
        }
        #endregion

        #region Methods
        public async Task<FacultyDto> GetFacultyByIdAsync(int refId)
        {
            try
            {
                _logger.LogInformation($"Search for Faculty data with RefID @{refId}.");

                FacultyDto Faculty = await _getFacultyInfoByIdAction.GetFacultyInfoAsync(refId);

                return Faculty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        public async Task<List<FacultyDto>> GetAllFaculty()
        {
            try
            {
                _logger.LogInformation($"Fetching All Faculty.");

                List<FacultyDto> response = await _getAllFacultyAction.GetAllFaculty();

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        public async Task<List<FacultyDto>> GetFacultysByNameAsync(string name)
        {
            try
            {
                _logger.LogInformation($"Search for Facultys with names matching: {name}.");

                List<FacultyDto> Facultys = await _getFacultysInfoByNameAction.GetFacultyInfoAsync(name);

                return Facultys;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        public async Task<List<int>> GetAsssinedApplicantionsByReviewerIdAsync(int reviewerId)
        {
            try
            {
                _logger.LogInformation($"Search for applicants with reviewer id: {reviewerId}.");

                List<int> facultyIds = await _getAssignedApplicationsForReviewerAction.GetAsssinedApplicantionsByReviewerIdAsync(reviewerId);

                return facultyIds;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        public async Task<bool> AssignPotentialAdvisorsAndAddCommentsAsync(List<AssignFacultyAndAddComment> assignFacultyAndAddComments)
        {
            try
            {
                _logger.LogInformation($"Updating faculty and comment data @{assignFacultyAndAddComments}.");

                var updatedApplicant = await _assignPotentialAdvisorsAndAddCommentsAction.AssignPotentialAdvisorsAndAddCommentsAsync(assignFacultyAndAddComments);

                if (updatedApplicant == null)
                {
                    _logger.LogError($"Failed to update comments and faculty  {assignFacultyAndAddComments}.");
                }

                return updatedApplicant;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        public async Task<List<FacultyDto>> SaveOrUpdateFacultyAsync(List<FacultyDto> facultyDtos)
        {
            try
            {
                _logger.LogInformation($"Updating faculty  data @{facultyDtos}.");

                var updatedApplicant = await _createOrUpdateFacultyAction.CreateOrUpdateFacultyAsync(facultyDtos);

                if (updatedApplicant == null)
                {
                    _logger.LogError($"Failed to update faculty data {facultyDtos}.");
                }

                return updatedApplicant;
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
