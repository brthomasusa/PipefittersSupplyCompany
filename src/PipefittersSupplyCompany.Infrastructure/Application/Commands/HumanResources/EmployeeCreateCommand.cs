using System;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Interfaces.HumanResources;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;

namespace PipefittersSupplyCompany.Infrastructure.Application.Commands.HumanResources
{
    public class EmployeeCreateCommand
    {
        public static async Task Execute(CreateEmployeeInfo model, IEmployeeAggregateRepository repo, IUnitOfWork unitOfWork)
        {
            if (await repo.Exists(model.Id))
            {
                throw new InvalidOperationException($"This employee ({model.FirstName} {model.MiddleInitial ?? ""} {model.LastName}) already exists!");
            }

            ExternalAgent agent = new(model.Id, AgentType.Employee);

            Employee employee = new Employee
            (
                agent,
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

            if (model.Addresses != null && model.Addresses.Count > 0)
            {
                foreach (var address in model.Addresses)
                {
                    employee.AddAddress(
                        0,
                        AddressVO.Create
                        (
                            address.AddressLine1,
                            address.AddressLine2,
                            address.City,
                            address.StateCode,
                            address.Zipcode
                        )
                    );
                }
            }

            if (model.Contacts != null && model.Contacts.Count > 0)
            {
                foreach (var contact in model.Contacts)
                {
                    employee.AddContactPerson
                    (
                        0,
                        PersonName.Create(contact.FirstName, contact.LastName, contact.MiddleInitial),
                        PhoneNumber.Create(contact.Telephone),
                        contact.Notes
                    );
                }
            }

            await repo.AddAsync(employee);
            await unitOfWork.Commit();
            model.Id = employee.Id;
        }
    }
}