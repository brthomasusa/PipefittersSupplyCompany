using System;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Interfaces.HumanResources;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;

namespace PipefittersSupplyCompany.Infrastructure.Application.Commands.HumanResources
{
    public class EmployeeWriteCommands
    {
        public static Task Execute(IWriteModel model, IEmployeeAggregateRepository repo, IUnitOfWork unitOfWork) =>
            model switch
            {
                CreateEmployeeInfo createModel => EmployeeCreateCommand.Execute(createModel, repo, unitOfWork),
                EditEmployeeInfo updateModel => EmployeeUpdateCommand.Execute(updateModel, repo, unitOfWork),
                DeleteEmployeeInfo deleteModel => EmployeeDeleteCommand.Execute(deleteModel, repo, unitOfWork),
                _ => throw new ArgumentOutOfRangeException("Unknown employee write command.", nameof(model))
            };

        private static async Task HandleUpdate(EditEmployeeInfo model, IEmployeeAggregateRepository repo, IUnitOfWork unitOfWork)
        {
            var employee = await repo.GetByIdAsync(model.Id);

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

            await unitOfWork.Commit();
        }

        private static async Task HandleDelete(DeleteEmployeeInfo model, IEmployeeAggregateRepository repo, IUnitOfWork unitOfWork)
        {
            var employee = await repo.GetByIdAsync(model.Id);

            if (employee == null)
            {
                throw new InvalidOperationException($"An employee with id {model.Id} could not be found!");
            }

            repo.Delete(employee);
            await unitOfWork.Commit();
        }

        private static async Task HandleActivate(ActivateEmployee model, IEmployeeAggregateRepository repo, IUnitOfWork unitOfWork)
        {
            var employee = await repo.GetByIdAsync(model.Id);

            if (employee == null)
            {
                throw new InvalidOperationException($"An employee with id {model.Id} could not be found!");
            }

            employee.Activate();
            await unitOfWork.Commit();
        }

        private static async Task HandleDeactivate(DeactivateEmployee model, IEmployeeAggregateRepository repo, IUnitOfWork unitOfWork)
        {
            var employee = await repo.GetByIdAsync(model.Id);

            if (employee == null)
            {
                throw new InvalidOperationException($"An employee with id {model.Id} could not be found!");
            }

            employee.Deactivate();
            await unitOfWork.Commit();
        }
    }
}