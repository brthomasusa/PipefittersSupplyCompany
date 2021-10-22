using System;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Interfaces.HumanResources;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;
using static PipefittersSupplyCompany.Infrastructure.Application.Commands.HumanResources.EmployeeAggregateWriteModels;

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
                CreateEmployeeInfo model =>
                    HandleCreate(model),
                EditEmployeeInfo model =>
                    HandleUpdate(model),
                DeleteEmployeeInfo model =>
                    HandleDelete(model),
                ActivateEmployee model =>
                    HandleActivate(model),
                CreateEmployeeAddressInfo model =>
                    HandleCreateAddress(model),
                EditEmployeeAddressInfo model =>
                    HandleUpdateAddress(model),
                DeleteEmployeeAddressInfo model =>
                    HandleDeleteAddress(model),
                CreateEmployeeContactInfo model =>
                    HandleCreateContact(model),
                EditEmployeeContactInfo model =>
                    HandleUpdateContact(model),
                DeleteEmployeeContactInfo model =>
                    HandleDeleteContact(model),
                _ => Task.CompletedTask
            };

        private async Task HandleCreate(CreateEmployeeInfo model)
        {
            if (await _employeeRepo.Exists(model.Id))
            {
                throw new InvalidOperationException($"This employee already exists!");
            }

            Employee employee = new Employee
            (
                new ExternalAgent(model.Id, AgentType.Employee),
                SupervisorId.Create(model.SupervisorId),
                PersonName.Create(model.FirstName, model.LastName, model.MiddleInitial),
                SSN.Create(model.SSN),
                PhoneNumber.Create(model.Telephone),
                MaritalStatus.Create(model.MaritalStatus),
                TaxExemption.Create(model.Exemptions),
                PayRate.Create(model.PayRate),
                StartDate.Create(model.StartDate),
                IsActive.Create(model.IsActive)
            );

            await _employeeRepo.AddAsync(employee);
            await _unitOfWork.Commit();
        }

        private async Task HandleUpdate(EditEmployeeInfo model)
        {
            var employee = await _employeeRepo.GetByIdAsync(model.Id);

            if (employee == null)
            {
                throw new InvalidOperationException($"An employee with id {model.Id} could not be found!");
            }

            employee.UpdateSupervisorId(SupervisorId.Create(model.SupervisorId));
            employee.UpdateEmployeeName(PersonName.Create(model.FirstName, model.LastName, model.MiddleInitial));
            employee.UpdateSSN(SSN.Create(model.SSN));
            employee.UpdateTelephone(PhoneNumber.Create(model.Telephone));
            employee.UpdateMaritalStatus(MaritalStatus.Create(model.MaritalStatus));
            employee.UpdateTaxExemptions(TaxExemption.Create(model.Exemptions));
            employee.UpdatePayRate(PayRate.Create(model.PayRate));
            employee.UpdateLastModifiedDate();

            if (model.IsActive)
            {
                employee.Activate();
            }
            else if (!model.IsActive)
            {
                employee.Deactivate();
            }

            await _unitOfWork.Commit();
        }

        private async Task HandleDelete(DeleteEmployeeInfo model)
        {
            var employee = await _employeeRepo.GetByIdAsync(model.Id);

            if (employee == null)
            {
                throw new InvalidOperationException($"An employee with id {model.Id} could not be found!");
            }

            _employeeRepo.Delete(employee);
            await _unitOfWork.Commit();
        }

        private async Task HandleActivate(ActivateEmployee model)
        {
            var employee = await _employeeRepo.GetByIdAsync(model.Id);

            if (employee == null)
            {
                throw new InvalidOperationException($"An employee with id {model.Id} could not be found!");
            }

            employee.Activate();
            await _unitOfWork.Commit();
        }

        private async Task HandleDeactivate(DeactivateEmployee model)
        {
            var employee = await _employeeRepo.GetByIdAsync(model.Id);

            if (employee == null)
            {
                throw new InvalidOperationException($"An employee with id {model.Id} could not be found!");
            }

            employee.Deactivate();
            await _unitOfWork.Commit();
        }

        private async Task HandleCreateAddress(CreateEmployeeAddressInfo model)
        {
            Employee employee = await _employeeRepo.GetByIdAsync(model.EmployeeId);

            if (employee is null)
            {
                throw new InvalidOperationException($"Update address failed, no employee with id '{model.EmployeeId}' found!");
            }

            employee.AddAddress(0, AddressVO.Create(model.AddressLine1, model.AddressLine2, model.City, model.StateCode, model.Zipcode));

            _employeeRepo.Update(employee);
            await _unitOfWork.Commit();
        }

        private async Task HandleUpdateAddress(EditEmployeeAddressInfo model)
        {
            Employee employee = await _employeeRepo.GetByIdAsync(model.EmployeeId);

            if (employee is null)
            {
                throw new InvalidOperationException($"Update address failed, no employee with id '{model.EmployeeId}' found!");
            }

            employee.UpdateAddress(model.AddressId, AddressVO.Create(model.AddressLine1, model.AddressLine2, model.City, model.StateCode, model.Zipcode));

            _employeeRepo.Update(employee);
            await _unitOfWork.Commit();
        }

        private async Task HandleDeleteAddress(DeleteEmployeeAddressInfo model)
        {
            Employee employee = await _employeeRepo.GetByIdAsync(model.EmployeeId);

            if (employee is null)
            {
                throw new InvalidOperationException($"Delete address failed, no employee with id '{model.EmployeeId}' found!");
            }

            employee.DeleteAddress(model.AddressId);

            _employeeRepo.Update(employee);
            await _unitOfWork.Commit();
        }

        private async Task HandleCreateContact(CreateEmployeeContactInfo model)
        {
            Employee employee = await _employeeRepo.GetByIdAsync(model.EmployeeId);

            if (employee is null)
            {
                throw new InvalidOperationException($"Create contact person failed, no employee with id '{model.EmployeeId}' found!");
            }

            employee.AddContactPerson(0, PersonName.Create(model.FirstName, model.LastName, model.MiddleInitial), PhoneNumber.Create(model.Telephone), model.Notes);

            _employeeRepo.Update(employee);
            await _unitOfWork.Commit();
        }

        private async Task HandleUpdateContact(EditEmployeeContactInfo model)
        {
            Employee employee = await _employeeRepo.GetByIdAsync(model.EmployeeId);

            if (employee is null)
            {
                throw new InvalidOperationException($"Update contact person failed, no employee with id '{model.EmployeeId}' found!");
            }

            employee.UpdateContactPerson(model.PersonId, PersonName.Create(model.FirstName, model.LastName, model.MiddleInitial), PhoneNumber.Create(model.Telephone), model.Notes);

            _employeeRepo.Update(employee);
            await _unitOfWork.Commit();
        }

        private async Task HandleDeleteContact(DeleteEmployeeContactInfo model)
        {
            Employee employee = await _employeeRepo.GetByIdAsync(model.EmployeeId);

            if (employee is null)
            {
                throw new InvalidOperationException($"Delete contact person failed, no employee with id '{model.EmployeeId}' found!");
            }

            employee.DeleteContactPerson(model.PersonId);

            _employeeRepo.Update(employee);
            await _unitOfWork.Commit();
        }
    }
}