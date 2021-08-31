using PipefittersSupplyCompany.Infrastructure.Interfaces;

namespace PipefittersSupplyCompany.Infrastructure.Application.Services
{
    public class EmployeeAppicationService
    {
        private readonly IEmployeeAggregateRepository _employeeRepo;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeAppicationService(IEmployeeAggregateRepository repo, IUnitOfWork unitOfWork)
        {
            _employeeRepo = repo;
            _unitOfWork = unitOfWork;
        }

    }
}