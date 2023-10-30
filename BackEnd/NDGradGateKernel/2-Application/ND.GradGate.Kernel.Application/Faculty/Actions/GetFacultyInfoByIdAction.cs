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
    public class GetFacultyInfoByIdAction : IGetFacultyInfoByIdAction
    {
        #region Attributes
        private readonly ILogger<GetFacultyInfoByIdAction> _logger;
        private readonly IFacultyRepository _FacultyRepository;
        #endregion

        #region Constructor
        public GetFacultyInfoByIdAction(ILogger<GetFacultyInfoByIdAction> logger,
                                    IFacultyRepository FacultyRepository)
        {
            _logger = logger;
            _FacultyRepository = FacultyRepository;
        }
        #endregion

        #region Methods
        public async Task<FacultyDto> GetFacultyInfoAsync(int refId)
        {
            try
            {
                _logger.LogInformation($"Fetching information for Faculty with RefID {refId}");

                var Faculty = await _FacultyRepository.GetByFacultyIdAsync(refId);

                FacultyDto response = new FacultyDto
                {
                    Ref = Faculty.Ref,
                    LastName = Faculty.LastName,
                    FirstName = Faculty.FirstName,
                };

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
