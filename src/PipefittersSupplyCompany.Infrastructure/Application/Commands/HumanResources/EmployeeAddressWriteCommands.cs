using System;
using System.Linq;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Interfaces.HumanResources;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;

namespace PipefittersSupplyCompany.Infrastructure.Application.Commands.HumanResources
{
    public class EmployeeAddressWriteCommands
    {
        public static Task Execute(IWriteModel model, IEmployeeAggregateRepository repo, IUnitOfWork unitOfWork) =>
            model switch
            {
                CreateEmployeeAddressInfo createModel => HandleCreate(createModel, repo, unitOfWork),
                EditEmployeeAddressInfo updateModel => HandleUpdate(updateModel, repo, unitOfWork),
                DeleteEmployeeAddressInfo deleteModel => HandleDelete(deleteModel, repo, unitOfWork),
                _ => throw new ArgumentOutOfRangeException("Unknown employee address write command.", nameof(model))
            };

        private static async Task HandleCreate(CreateEmployeeAddressInfo model, IEmployeeAggregateRepository repo, IUnitOfWork unitOfWork)
        {
            Employee employee = await repo.GetByIdAsync(model.EmployeeId);

            if (employee is null)
            {
                throw new InvalidOperationException($"Update address failed, no employee with id '{model.EmployeeId}' found!");
            }

            employee.AddAddress(0, AddressVO.Create(model.AddressLine1, model.AddressLine2, model.City, model.StateCode, model.Zipcode));

            repo.Update(employee);
            await unitOfWork.Commit();

            var mostRecent = employee.Addresses().Where(a => a.AddressDetails.AddressLine1 == model.AddressLine1 &&
                                                             a.AddressDetails.AddressLine2 == model.AddressLine2 &&
                                                             a.AddressDetails.City == model.City &&
                                                             a.AddressDetails.StateCode == model.StateCode &&
                                                             a.AddressDetails.Zipcode == model.Zipcode)
                                                    .FirstOrDefault();

            model.AddressId = mostRecent.Id;
        }

        private static async Task HandleUpdate(EditEmployeeAddressInfo model, IEmployeeAggregateRepository repo, IUnitOfWork unitOfWork)
        {
            Employee employee = await repo.GetByIdAsync(model.EmployeeId);

            if (employee is null)
            {
                throw new InvalidOperationException($"Update address failed, no employee with id '{model.EmployeeId}' found!");
            }

            employee.UpdateAddress(model.AddressId, AddressVO.Create(model.AddressLine1, model.AddressLine2, model.City, model.StateCode, model.Zipcode));

            repo.Update(employee);
            await unitOfWork.Commit();
        }

        private static async Task HandleDelete(DeleteEmployeeAddressInfo model, IEmployeeAggregateRepository repo, IUnitOfWork unitOfWork)
        {
            Employee employee = await repo.GetByIdAsync(model.EmployeeId);

            if (employee is null)
            {
                throw new InvalidOperationException($"Delete address failed, no employee with id '{model.EmployeeId}' found!");
            }

            employee.DeleteAddress(model.AddressId);

            repo.Update(employee);
            await unitOfWork.Commit();
        }
    }
}