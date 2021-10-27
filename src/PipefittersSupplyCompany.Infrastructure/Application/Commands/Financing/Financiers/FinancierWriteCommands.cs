using System;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Interfaces.Financing;
using PipefittersSupplyCompany.Core.Financing.FinancierAggregate;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;

namespace PipefittersSupplyCompany.Infrastructure.Application.Commands.Financing.Financiers
{
    public class FinancierWriteCommands
    {
        public static Task Execute(IWriteModel model, IFinancierAggregateRepository repo, IUnitOfWork unitOfWork) =>
            model switch
            {
                CreateFinancierInfo createModel => HandleCreate(createModel, repo, unitOfWork),
                EditFinancierInfo updateModel => HandleUpdate(updateModel, repo, unitOfWork),
                DeleteFinancierInfo deleteModel => HandleDelete(deleteModel, repo, unitOfWork),
                _ => Task.CompletedTask
            };

        private static async Task HandleCreate(CreateFinancierInfo model, IFinancierAggregateRepository repo, IUnitOfWork unitOfWork)
        {
            if (await repo.Exists(model.Id))
            {
                throw new InvalidOperationException($"This financier already exists!");
            }

            Financier financier = new Financier
            (
                new ExternalAgent(model.Id, AgentType.Financier),
                OrganizationName.Create(model.FinancierName),
                PhoneNumber.Create(model.Telephone),
                IsActive.Create(model.IsActive),
                model.UserId
            );


            await repo.AddAsync(financier);
            await unitOfWork.Commit();
            model.Id = financier.Id;
        }

        private static async Task HandleUpdate(EditFinancierInfo model, IFinancierAggregateRepository repo, IUnitOfWork unitOfWork)
        {
            var financier = await repo.GetByIdAsync(model.Id);

            if (financier == null)
            {
                throw new InvalidOperationException($"Unable to update financier info, a financier with id {model.Id} could not be found!");
            }

            financier.UpdateFinancierName(OrganizationName.Create(model.FinancierName));
            financier.UpdateTelephone(PhoneNumber.Create(model.Telephone));
            financier.UpdateLastModifiedDate();

            if (model.IsActive)
            {
                financier.Activate();
            }
            else if (!model.IsActive)
            {
                financier.Deactivate();
            }

            financier.UpdateUserId(model.UserId);

            await unitOfWork.Commit();
        }

        private static async Task HandleDelete(DeleteFinancierInfo model, IFinancierAggregateRepository repo, IUnitOfWork unitOfWork)
        {
            var financier = await repo.GetByIdAsync(model.Id);

            if (financier == null)
            {
                throw new InvalidOperationException($"Unable to delete financier info, a financier with id {model.Id} could not be found!");
            }

            repo.Delete(financier);
            await unitOfWork.Commit();
        }
    }
}
