using System;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;
using static PipefittersSupplyCompany.Infrastructure.Application.Commands.HumanResources.EmployeeAggregateCommand;

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

            await _employeeRepo.AddAsync(employee);
            await _unitOfWork.Commit();
        }

        private async Task HandleUpdate(V1.UpdateEmployee cmd)
        {
            var employee = await _employeeRepo.GetByIdAsync(cmd.Id);

            if (employee == null)
            {
                throw new InvalidOperationException($"An employee with id {cmd.Id} could not be found!");
            }

            employee.UpdateSupervisorId(SupervisorId.Create(cmd.SupervisorId));
            employee.UpdateEmployeeName(PersonName.Create(cmd.FirstName, cmd.LastName, cmd.MiddleInitial));
            employee.UpdateSSN(SSN.Create(cmd.SSN));
            employee.UpdateTelephone(PhoneNumber.Create(cmd.Telephone));
            employee.UpdateMaritalStatus(MaritalStatus.Create(cmd.MaritalStatus));
            employee.UpdateTaxExemptions(TaxExemption.Create(cmd.Exemptions));
            employee.UpdatePayRate(PayRate.Create(cmd.PayRate));

            if (cmd.IsActive)
            {
                employee.Activate();
            }
            else if (!cmd.IsActive)
            {
                employee.Deactivate();
            }

            await _unitOfWork.Commit();
        }

        private async Task HandleActivate(V1.ActivateEmployee cmd)
        {
            var employee = await _employeeRepo.GetByIdAsync(cmd.Id);

            if (employee == null)
            {
                throw new InvalidOperationException($"An employee with id {cmd.Id} could not be found!");
            }

            employee.Activate();
            await _unitOfWork.Commit();
        }

        private async Task HandleDeactivate(V1.DeactivateEmployee cmd)
        {
            var employee = await _employeeRepo.GetByIdAsync(cmd.Id);

            if (employee == null)
            {
                throw new InvalidOperationException($"An employee with id {cmd.Id} could not be found!");
            }

            employee.Deactivate();
            await _unitOfWork.Commit();
        }
    }
}