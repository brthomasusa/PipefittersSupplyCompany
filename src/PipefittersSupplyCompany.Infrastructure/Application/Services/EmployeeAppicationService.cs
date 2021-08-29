using System;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Commands.HumanResources;
using PipefittersSupplyCompany.Infrastructure.Persistence;
using PipefittersSupplyCompany.Infrastructure.Persistence.Repositories.HumanResources;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;
using static PipefittersSupplyCompany.Infrastructure.Application.Commands.HumanResources.EmployeeAggregateCommand;

namespace PipefittersSupplyCompany.Infrastructure.Application.Services
{
    public class EmployeeAppicationService : IApplicationService
    {
        private readonly IEmployeeAggregateRepository _employeeRepo;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeAppicationService(IEmployeeAggregateRepository repo, IUnitOfWork unitOfWork)
        {
            _employeeRepo = repo;
            _unitOfWork = unitOfWork;
        }

        public Task Handle(ICommand command) =>
            command switch
            {
                V1.CreateEmployee cmd =>
                    HandleCreate(cmd),
                V1.UpdateEmployee cmd =>
                    HandleUpdate(cmd),
                V1.ActivateEmployee cmd =>
                    HandleActivate(cmd),
                _ => Task.CompletedTask
            };

        private async Task HandleActivate(V1.ActivateEmployee cmd)
        {
            var employee = await _employeeRepo.Load(cmd.Id);

            if (employee == null)
            {
                throw new InvalidOperationException($"An employee with id {cmd.Id} could not be found!");
            }

            employee.Activate();
            await _unitOfWork.Commit();
        }

        private async Task HandleDeactivate(V1.DeactivateEmployee cmd)
        {
            var employee = await _employeeRepo.Load(cmd.Id);

            if (employee == null)
            {
                throw new InvalidOperationException($"An employee with id {cmd.Id} could not be found!");
            }

            employee.Deactivate();
            await _unitOfWork.Commit();
        }

        private async Task HandleCreate(V1.CreateEmployee cmd)
        {
            if (await _employeeRepo.Exists(cmd.Id))
            {
                throw new InvalidOperationException($"This employee already exists!");
            }

            Employee employee = new Employee
            (
                new ExternalAgent(cmd.Id, AgentType.Employee),
                SupervisorId.Create(cmd.SupervisorId),
                PersonName.Create(cmd.FirstName, cmd.LastName, cmd.MiddleInitial),
                SSN.Create(cmd.SSN),
                PhoneNumber.Create(cmd.Telephone),
                MaritalStatus.Create(cmd.MaritalStatus),
                TaxExemption.Create(cmd.Exemptions),
                PayRate.Create(cmd.PayRate),
                StartDate.Create(cmd.StartDate),
                IsActive.Create(cmd.IsActive)
            );

            await _employeeRepo.Add(employee);
            await _unitOfWork.Commit();
        }

        private async Task HandleUpdate(V1.UpdateEmployee cmd)
        {
            var employee = await _employeeRepo.Load(cmd.Id);

            if (employee == null)
            {
                throw new InvalidOperationException($"An employee with id {cmd.Id} could not be found!");
            }

            // employee.UpdateEmployee
            // (
            //     EmployeeTypeIdentifier.FromInterger(cmd.EmployeeTypeId),
            //     new EmployeeId(cmd.SupervisorId),
            //     EmployeeLastName.FromString(cmd.LastName),
            //     EmployeeFirstName.FromString(cmd.FirstName),
            //     EmployeeMiddleInitial.FromString(cmd.MiddleInitial),
            //     EmployeeSSN.FromString(cmd.SSN),
            //     AddressLine1.FromString(cmd.AddressLine1),
            //     AddressLine2.FromString(cmd.AddressLine2),
            //     City.FromString(cmd.City),
            //     StateProvinceCode.FromString(cmd.StateProvinceCode, _stateCodeLkup),
            //     Zipcode.FromString(cmd.Zipcode),
            //     Telephone.FromString(cmd.Telephone),
            //     MaritalStatus.FromString(cmd.MaritalStatus),
            //     TaxExemption.FromInterger(cmd.Exemptions),
            //     EmployeePayRate.FromDecimal(cmd.PayRate),
            //     EmployeeStartDate.FromDateTime(cmd.StartDate),
            //     IsActive.FromBoolean(cmd.IsActive)
            // );

            await _unitOfWork.Commit();
        }
    }
}