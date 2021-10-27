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
    public class EmployeeContactWriteCommands
    {
        public static Task Execute(IWriteModel model, IEmployeeAggregateRepository repo, IUnitOfWork unitOfWork) =>
            model switch
            {
                CreateEmployeeContactInfo createModel => HandleCreate(createModel, repo, unitOfWork),
                EditEmployeeContactInfo updateModel => HandleUpdate(updateModel, repo, unitOfWork),
                DeleteEmployeeContactInfo deleteModel => HandleDelete(deleteModel, repo, unitOfWork),
                _ => throw new ArgumentOutOfRangeException("Unknown employee contact write command.", nameof(model))
            };

        private static async Task HandleCreate(CreateEmployeeContactInfo model, IEmployeeAggregateRepository repo, IUnitOfWork unitOfWork)
        {
            Employee employee = await repo.GetByIdAsync(model.EmployeeId);

            if (employee is null)
            {
                throw new InvalidOperationException($"Create contact person failed, no employee with id '{model.EmployeeId}' found!");
            }

            employee.AddContactPerson(0, PersonName.Create(model.FirstName, model.LastName, model.MiddleInitial), PhoneNumber.Create(model.Telephone), model.Notes);

            repo.Update(employee);
            await unitOfWork.Commit();

            var mostRecent = employee.ContactPersons().Where(c => c.ContactName.FirstName == model.FirstName &&
                                                                  c.ContactName.LastName == model.LastName &&
                                                                  c.ContactName.MiddleInitial == model.MiddleInitial &&
                                                                  c.Telephone == model.Telephone &&
                                                                  c.Agent.Id == model.EmployeeId)
                                                        .FirstOrDefault();

            model.PersonId = mostRecent.Id;
        }

        private static async Task HandleUpdate(EditEmployeeContactInfo model, IEmployeeAggregateRepository repo, IUnitOfWork unitOfWork)
        {
            Employee employee = await repo.GetByIdAsync(model.EmployeeId);

            if (employee is null)
            {
                throw new InvalidOperationException($"Update contact person failed, no employee with id '{model.EmployeeId}' found!");
            }

            employee.UpdateContactPerson(model.PersonId, PersonName.Create(model.FirstName, model.LastName, model.MiddleInitial), PhoneNumber.Create(model.Telephone), model.Notes);

            repo.Update(employee);
            await unitOfWork.Commit();
        }

        private static async Task HandleDelete(DeleteEmployeeContactInfo model, IEmployeeAggregateRepository repo, IUnitOfWork unitOfWork)
        {
            Employee employee = await repo.GetByIdAsync(model.EmployeeId);

            if (employee is null)
            {
                throw new InvalidOperationException($"Delete contact person failed, no employee with id '{model.EmployeeId}' found!");
            }

            employee.DeleteContactPerson(model.PersonId);

            repo.Update(employee);
            await unitOfWork.Commit();
        }
    }
}