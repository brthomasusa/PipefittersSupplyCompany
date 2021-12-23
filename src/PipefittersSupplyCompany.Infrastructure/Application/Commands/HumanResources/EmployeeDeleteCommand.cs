using System;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Interfaces.HumanResources;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;

namespace PipefittersSupplyCompany.Infrastructure.Application.Commands.HumanResources
{
    public class EmployeeDeleteCommand
    {
        public static async Task Execute(DeleteEmployeeInfo model, IEmployeeAggregateRepository repo, IUnitOfWork unitOfWork)
        {
            var employee = await repo.GetByIdAsync(model.Id) ??
                    throw new InvalidOperationException($"An employee with id '{model.Id}' could not be found!");

            if (employee.Addresses().Count > 0)
            {
                foreach (Address address in employee.Addresses())
                {
                    employee.DeleteAddress(address.Id);
                }
            }

            if (employee.ContactPersons().Count > 0)
            {
                foreach (ContactPerson contact in employee.ContactPersons())
                {
                    employee.DeleteContactPerson(contact.Id);
                }
            }

            repo.Delete(employee);
            await unitOfWork.Commit();
        }
    }
}