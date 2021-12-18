using System;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Interfaces.HumanResources;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;

namespace PipefittersSupplyCompany.Infrastructure.Application.Commands.HumanResources
{
    public class EmployeeUpdateCommand
    {
        public static async Task Execute(EditEmployeeInfo model, IEmployeeAggregateRepository repo, IUnitOfWork unitOfWork)
        {
            var employee = await repo.GetByIdAsync(model.Id) ??
                    throw new InvalidOperationException($"An employee with id '{model.Id}' could not be found!");

            if (model.Status == RecordStatus.Modified)
            {
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
            }

            foreach (var address in model.Addresses)
            {
                if (address.Status == RecordStatus.New)
                {
                    employee.AddAddress(0, AddressVO.Create(address.AddressLine1, address.AddressLine2, address.City, address.StateCode, address.Zipcode));
                }
                else if (address.Status == RecordStatus.Modified)
                {
                    employee.UpdateAddress(address.AddressId, AddressVO.Create(address.AddressLine1, address.AddressLine2, address.City, address.StateCode, address.Zipcode));
                }
                else if (address.Status == RecordStatus.Deleted)
                {
                    employee.DeleteAddress(address.AddressId);
                }
            }

            await unitOfWork.Commit();
        }
    }
}