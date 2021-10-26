using System;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Interfaces.HumanResources;

namespace PipefittersSupplyCompany.Infrastructure.Application.Commands.HumanResources
{
    public class EmployeeAggregateCommandHandler : ICommandHandler
    {
        private readonly IEmployeeAggregateRepository _employeeRepo;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeAggregateCommandHandler(IEmployeeAggregateRepository repo, IUnitOfWork unitOfWork)
        {
            _employeeRepo = repo;
            _unitOfWork = unitOfWork;
        }

        public Task Handle(IWriteModel writeModel) =>
            writeModel switch
            {
                CreateEmployeeInfo model => EmployeeWriteCommands.Execute(model, _employeeRepo, _unitOfWork),
                EditEmployeeInfo model => EmployeeWriteCommands.Execute(model, _employeeRepo, _unitOfWork),
                DeleteEmployeeInfo model => EmployeeWriteCommands.Execute(model, _employeeRepo, _unitOfWork),
                ActivateEmployee model => EmployeeWriteCommands.Execute(model, _employeeRepo, _unitOfWork),

                CreateEmployeeAddressInfo model => EmployeeAddressWriteCommands.Execute(model, _employeeRepo, _unitOfWork),
                EditEmployeeAddressInfo model => EmployeeAddressWriteCommands.Execute(model, _employeeRepo, _unitOfWork),
                DeleteEmployeeAddressInfo model => EmployeeAddressWriteCommands.Execute(model, _employeeRepo, _unitOfWork),

                CreateEmployeeContactInfo model => EmployeeContactWriteCommands.Execute(model, _employeeRepo, _unitOfWork),
                EditEmployeeContactInfo model => EmployeeContactWriteCommands.Execute(model, _employeeRepo, _unitOfWork),
                DeleteEmployeeContactInfo model => EmployeeContactWriteCommands.Execute(model, _employeeRepo, _unitOfWork),

                _ => throw new ArgumentOutOfRangeException("Unknown employee write command", nameof(writeModel))
            };
    }
}