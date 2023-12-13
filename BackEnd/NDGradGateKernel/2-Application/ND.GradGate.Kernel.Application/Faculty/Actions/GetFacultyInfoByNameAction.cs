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
        public async Task<List<FacultyDto>> GetFacultyInfoAsync(string name)
        {
            try
            {
                _logger.LogInformation($"Fetching information for Facultys with names matching: {name}");

                var Facultys = await _FacultyRepository.GetByFacultyNameAsync(name);

                var response = Facultys.Select(Faculty => new FacultyDto
                {
                    Name = Faculty.Name,
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
