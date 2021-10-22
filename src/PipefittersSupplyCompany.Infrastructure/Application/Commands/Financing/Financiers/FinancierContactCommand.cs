using System;
using System.Linq;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Interfaces.Financing;
using PipefittersSupplyCompany.Core.Financing.FinancierAggregate;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;

namespace PipefittersSupplyCompany.Infrastructure.Application.Commands.Financing.Financiers
{
    public class FinancierContactCommand
    {
        public static Task Execute(IWriteModel model, IFinancierAggregateRepository repo, IUnitOfWork unitOfWork) =>
            model switch
            {
                CreateFinancierContactInfo createModel => HandleCreate(createModel, repo, unitOfWork),
                EditFinancierContactInfo updateModel => HandleUpdate(updateModel, repo, unitOfWork),
                DeleteFinancierContactInfo deleteModel => HandleDelete(deleteModel, repo, unitOfWork),
                _ => Task.CompletedTask
            };

        private static async Task HandleCreate(CreateFinancierContactInfo model, IFinancierAggregateRepository repo, IUnitOfWork unitOfWork)
        {
            Financier financier = await repo.GetByIdAsync(model.FinancierId);

            if (financier is null)
            {
                throw new InvalidOperationException($"Create contact person failed, no financier with id '{model.FinancierId}' found!");
            }

            financier.AddContactPerson(0, PersonName.Create(model.FirstName, model.LastName, model.MiddleInitial), PhoneNumber.Create(model.Telephone), model.Notes);

            repo.Update(financier);
            await unitOfWork.Commit();

            var lastCreated = financier.ContactPersons().OrderByDescending(a => a.CreatedDate).FirstOrDefault();
            model.PersonId = lastCreated.Id;
        }

        private static async Task HandleUpdate(EditFinancierContactInfo model, IFinancierAggregateRepository repo, IUnitOfWork unitOfWork)
        {
            Financier financier = await repo.GetByIdAsync(model.FinancierId);

            if (financier is null)
            {
                throw new InvalidOperationException($"Update contact person failed, no financier with id '{model.FinancierId}' found!");
            }

            financier.UpdateContactPerson(model.PersonId, PersonName.Create(model.FirstName, model.LastName, model.MiddleInitial), PhoneNumber.Create(model.Telephone), model.Notes);

            repo.Update(financier);
            await unitOfWork.Commit();
        }

        private static async Task HandleDelete(DeleteFinancierContactInfo model, IFinancierAggregateRepository repo, IUnitOfWork unitOfWork)
        {
            Financier financier = await repo.GetByIdAsync(model.FinancierId);

            if (financier is null)
            {
                throw new InvalidOperationException($"Delete contact person failed, no financier with id '{model.FinancierId}' found!");
            }

            financier.DeleteContactPerson(model.PersonId);

            repo.Update(financier);
            await unitOfWork.Commit();
        }
    }
}