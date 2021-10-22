using System;
using System.Linq;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Interfaces.Financing;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;

namespace PipefittersSupplyCompany.Infrastructure.Application.Commands.Financing.Financiers
{
    public class FinancierAddressCommand
    {
        public static Task Execute(IWriteModel model, IFinancierAggregateRepository repo, IUnitOfWork unitOfWork) =>
            model switch
            {
                CreateFinancierAddressInfo createModel => HandleCreate(createModel, repo, unitOfWork),
                EditFinancierAddressInfo updateModel => HandleUpdate(updateModel, repo, unitOfWork),
                DeleteFinancierAddressInfo deleteModel => HandleDelete(deleteModel, repo, unitOfWork),
                _ => Task.CompletedTask
            };

        private static async Task HandleCreate(CreateFinancierAddressInfo model, IFinancierAggregateRepository repo, IUnitOfWork unitOfWork)
        {
            var financier = await repo.GetByIdAsync(model.FinancierId);

            if (financier is null)
            {
                throw new InvalidOperationException($"Update address failed, no financier with id '{model.FinancierId}' found!");
            }

            financier.AddAddress(0, AddressVO.Create(model.AddressLine1, model.AddressLine2, model.City, model.StateCode, model.Zipcode));

            repo.Update(financier);
            await unitOfWork.Commit();

            var lastCreated = financier.Addresses().OrderByDescending(a => a.CreatedDate).FirstOrDefault();
            model.AddressId = lastCreated.Id;
        }

        private static async Task HandleUpdate(EditFinancierAddressInfo model, IFinancierAggregateRepository repo, IUnitOfWork unitOfWork)
        {
            var financier = await repo.GetByIdAsync(model.FinancierId);

            if (financier is null)
            {
                throw new InvalidOperationException($"Update address failed, no financier with id '{model.FinancierId}' found!");
            }

            financier.UpdateAddress(model.AddressId, AddressVO.Create(model.AddressLine1, model.AddressLine2, model.City, model.StateCode, model.Zipcode));

            repo.Update(financier);
            await unitOfWork.Commit();
        }

        private static async Task HandleDelete(DeleteFinancierAddressInfo model, IFinancierAggregateRepository repo, IUnitOfWork unitOfWork)
        {
            var financier = await repo.GetByIdAsync(model.FinancierId);

            if (financier is null)
            {
                throw new InvalidOperationException($"Delete address failed, no financier with id '{model.FinancierId}' found!");
            }

            financier.DeleteAddress(model.AddressId);

            repo.Update(financier);
            await unitOfWork.Commit();
        }
    }
}