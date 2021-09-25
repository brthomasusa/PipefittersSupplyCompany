using System;
using System.Linq;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Core.Interfaces;
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
                CreateEmployeeInfo cmd =>
                    HandleCreate(cmd),
                EditEmployeeInfo cmd =>
                    HandleUpdate(cmd),
                DeleteEmployeeInfo cmd =>
                    HandleDelete(cmd),
                ActivateEmployee cmd =>
                    HandleActivate(cmd),
                CreateEmployeeAddressInfo cmd =>
                    HandleCreateAddress(cmd),
                EditEmployeeAddressInfo cmd =>
                    HandleUpdateAddress(cmd),
                DeleteEmployeeAddressInfo cmd =>
                    HandleDeleteAddress(cmd),
                CreateEmployeeContactInfo cmd =>
                    HandleCreateContact(cmd),
                EditEmployeeContactInfo cmd =>
                    HandleUpdateContact(cmd),
                DeleteEmployeeContactInfo cmd =>
                    HandleDeleteContact(cmd),
                _ => Task.CompletedTask
            };

        private async Task HandleCreate(CreateEmployeeInfo cmd)
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

        private async Task HandleUpdate(EditEmployeeInfo cmd)
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

        private async Task HandleDelete(DeleteEmployeeInfo cmd)
        {
            var employee = await _employeeRepo.GetByIdAsync(cmd.Id);

            if (employee == null)
            {
                throw new InvalidOperationException($"An employee with id {cmd.Id} could not be found!");
            }

            _employeeRepo.Delete(employee);
            await _unitOfWork.Commit();
        }

        private async Task HandleActivate(ActivateEmployee cmd)
        {
            var employee = await _employeeRepo.GetByIdAsync(cmd.Id);

            if (employee == null)
            {
                throw new InvalidOperationException($"An employee with id {cmd.Id} could not be found!");
            }

            employee.Activate();
            await _unitOfWork.Commit();
        }

        private async Task HandleDeactivate(DeactivateEmployee cmd)
        {
            var employee = await _employeeRepo.GetByIdAsync(cmd.Id);

            if (employee == null)
            {
                throw new InvalidOperationException($"An employee with id {cmd.Id} could not be found!");
            }

            employee.Deactivate();
            await _unitOfWork.Commit();
        }

        private async Task HandleCreateAddress(CreateEmployeeAddressInfo cmd)
        {
            Employee employee = await _employeeRepo.GetByIdAsync(cmd.EmployeeId);

            if (employee is null)
            {
                throw new InvalidOperationException($"Update address failed, no employee with id '{cmd.EmployeeId}' found!");
            }

            employee.AddAddress(0, AddressVO.Create(cmd.AddressLine1, cmd.AddressLine2, cmd.City, cmd.StateCode, cmd.Zipcode));

            _employeeRepo.Update(employee);
            await _unitOfWork.Commit();
        }

        private async Task HandleUpdateAddress(EditEmployeeAddressInfo cmd)
        {
            Employee employee = await _employeeRepo.GetByIdAsync(cmd.EmployeeId);

            if (employee is null)
            {
                throw new InvalidOperationException($"Update address failed, no employee with id '{cmd.EmployeeId}' found!");
            }

            employee.UpdateAddress(cmd.AddressId, AddressVO.Create(cmd.AddressLine1, cmd.AddressLine2, cmd.City, cmd.StateCode, cmd.Zipcode));

            _employeeRepo.Update(employee);
            await _unitOfWork.Commit();
        }

        private async Task HandleDeleteAddress(DeleteEmployeeAddressInfo cmd)
        {
            Employee employee = await _employeeRepo.GetByIdAsync(cmd.EmployeeId);

            if (employee is null)
            {
                throw new InvalidOperationException($"Delete contact person failed, no employee with id '{cmd.EmployeeId}' found!");
            }

            employee.DeleteAddress(cmd.AddressId);

            _employeeRepo.Update(employee);
            await _unitOfWork.Commit();
        }

        private async Task HandleCreateContact(CreateEmployeeContactInfo cmd)
        {
            Employee employee = await _employeeRepo.GetByIdAsync(cmd.EmployeeId);

            if (employee is null)
            {
                throw new InvalidOperationException($"Create contact person failed, no employee with id '{cmd.EmployeeId}' found!");
            }

            employee.AddContactPerson(0, PersonName.Create(cmd.FirstName, cmd.LastName, cmd.MiddleInitial), PhoneNumber.Create(cmd.Telephone), cmd.Notes);

            _employeeRepo.Update(employee);
            await _unitOfWork.Commit();
        }

        private async Task HandleUpdateContact(EditEmployeeContactInfo cmd)
        {
            Employee employee = await _employeeRepo.GetByIdAsync(cmd.EmployeeId);

            if (employee is null)
            {
                throw new InvalidOperationException($"Update contact person failed, no employee with id '{cmd.EmployeeId}' found!");
            }

            employee.UpdateContactPerson(cmd.PersonId, PersonName.Create(cmd.FirstName, cmd.LastName, cmd.MiddleInitial), PhoneNumber.Create(cmd.Telephone), cmd.Notes);

            _employeeRepo.Update(employee);
            await _unitOfWork.Commit();
        }

        private async Task HandleDeleteContact(DeleteEmployeeContactInfo cmd)
        {
            Employee employee = await _employeeRepo.GetByIdAsync(cmd.EmployeeId);

            if (employee is null)
            {
                throw new InvalidOperationException($"Delete contact person failed, no employee with id '{cmd.EmployeeId}' found!");
            }

            employee.DeleteContactPerson(cmd.PersonId);

            _employeeRepo.Update(employee);
            await _unitOfWork.Commit();
        }
    }
}