using Microsoft.Extensions.Logging;
using ND.GradGate.Kernel.Application.Facultys.Interfaces.Actions;
using ND.GradGate.Kernel.DataAccess.Repositories.Interfaces;
using ND.GradGate.Kernel.Domain.Contracts.Faculty;
using ND.GradGate.Kernel.Domain.Contracts.Faculty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Application.Facultys.Actions
{
    public class GetFacultysInfoByNameAction : IGetFacultysInfoByNameAction
    {
        #region Attributes
        private readonly ILogger<GetFacultysInfoByNameAction> _logger;
        private readonly IFacultyRepository _FacultyRepository;
        #endregion

        #region Constructor
        public GetFacultysInfoByNameAction(ILogger<GetFacultysInfoByNameAction> logger,
                                    IFacultyRepository FacultyRepository)
        {
            _logger = logger;
            _FacultyRepository = FacultyRepository;
        }
        #endregion

        #region Methods
        public async Task<List<FacultyDto>> GetFacultyInfoAsync(string firstName, string lastName)
        {
            try
            {
                _logger.LogInformation($"Fetching information for Facultys with names matching: FirstName: {firstName}, LastName: {lastName}");

                var Facultys = await _FacultyRepository.GetByFacultyNameAsync(firstName, lastName);

                var response = Facultys.Select(Faculty => new FacultyDto
                {
                    Ref = Faculty.Ref,
                    FullName = $"{Faculty.FirstName} {Faculty.LastName}",
                }).ToList();

                return response;
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
