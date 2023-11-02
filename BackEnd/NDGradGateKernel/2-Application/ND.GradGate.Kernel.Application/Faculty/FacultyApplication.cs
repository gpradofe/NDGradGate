using Microsoft.Extensions.Logging;
using ND.GradGate.Kernel.Application.Facultys.Actions;
using ND.GradGate.Kernel.Application.Facultys.Interfaces;
using ND.GradGate.Kernel.Application.Facultys.Interfaces.Actions;
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
        #endregion

        #region Constructors
        public FacultyApplication(ILogger<FacultyApplication> logger,
                                    IGetFacultyInfoByIdAction getFacultyInfoByIdAction,
                                    IGetFacultysInfoByNameAction getFacultysInfoByNameAction)
        {
            _logger = logger;
            _getFacultyInfoByIdAction = getFacultyInfoByIdAction;
            _getFacultysInfoByNameAction = getFacultysInfoByNameAction;
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

        public async Task<List<FacultyDto>> GetFacultysByNameAsync(string firstName, string lastName)
        {
            try
            {
                _logger.LogInformation($"Search for Facultys with names matching: FirstName: {firstName}, LastName: {lastName}.");

                List<FacultyDto> Facultys = await _getFacultysInfoByNameAction.GetFacultyInfoAsync(firstName, lastName);

                return Facultys;
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
