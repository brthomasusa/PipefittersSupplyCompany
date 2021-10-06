using System;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Core.Financing.FinancierAggregate;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;
using static PipefittersSupplyCompany.Infrastructure.Application.Commands.Financing.FinancierAggregateWriteModels;

namespace PipefittersSupplyCompany.Infrastructure.Application.Commands.Financing
{
    public class FinancierAggregateCommandHandler : ICommandHandler
    {
        private readonly IFinancierAggregateRepository _financierRepo;
        private readonly IUnitOfWork _unitOfWork;

        public FinancierAggregateCommandHandler(IFinancierAggregateRepository repo, IUnitOfWork unitOfWork)
        {
            _financierRepo = repo;
            _unitOfWork = unitOfWork;
        }

        public Task Handle(IWriteModel writeModel) =>
            writeModel switch
            {
                CreateFinancierInfo model =>
                    HandleCreate(model),
                EditFinancierInfo model =>
                    HandleUpdate(model),
                DeleteFinancierInfo model =>
                    HandleDelete(model),
                CreateFinancierAddressInfo model =>
                    HandleCreateAddress(model),
                EditFinancierAddressInfo model =>
                    HandleUpdateAddress(model),
                DeleteFinancierAddressInfo model =>
                    HandleDeleteAddress(model),
                CreateFinancierContactInfo model =>
                    HandleCreateContact(model),
                EditFinancierContactInfo model =>
                    HandleUpdateContact(model),
                DeleteFinancierContactInfo model =>
                    HandleDeleteContact(model),
                _ => Task.CompletedTask
            };

        private async Task HandleCreate(CreateFinancierInfo model)
        {
            if (await _financierRepo.Exists(model.Id))
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

            await _financierRepo.AddAsync(financier);
            await _unitOfWork.Commit();
        }

        private async Task HandleUpdate(EditFinancierInfo model)
        {
            var financier = await _financierRepo.GetByIdAsync(model.Id);

            if (financier == null)
            {
                throw new InvalidOperationException($"Unable to update financier info, a financier with id {model.Id} could not be found!");
            }

            financier.UpdateFinancierName(OrganizationName.Create(model.FinancierName));
            financier.UpdateTelephone(PhoneNumber.Create(model.Telephone));

            if (model.IsActive)
            {
                financier.Activate();
            }
            else if (!model.IsActive)
            {
                financier.Deactivate();
            }

            financier.UpdateUserId(model.UserId);

            await _unitOfWork.Commit();
        }

        private async Task HandleDelete(DeleteFinancierInfo model)
        {
            var financier = await _financierRepo.GetByIdAsync(model.Id);

            if (financier == null)
            {
                throw new InvalidOperationException($"Unable to delete financier info, a financier with id {model.Id} could not be found!");
            }

            _financierRepo.Delete(financier);
            await _unitOfWork.Commit();
        }

        private async Task HandleCreateAddress(CreateFinancierAddressInfo model)
        {
            var financier = await _financierRepo.GetByIdAsync(model.FinancierId);

            if (financier is null)
            {
                throw new InvalidOperationException($"Update address failed, no financier with id '{model.FinancierId}' found!");
            }

            financier.AddAddress(0, AddressVO.Create(model.AddressLine1, model.AddressLine2, model.City, model.StateCode, model.Zipcode));

            _financierRepo.Update(financier);
            await _unitOfWork.Commit();
        }

        private async Task HandleUpdateAddress(EditFinancierAddressInfo model)
        {
            var financier = await _financierRepo.GetByIdAsync(model.FinancierId);

            if (financier is null)
            {
                throw new InvalidOperationException($"Update address failed, no financier with id '{model.FinancierId}' found!");
            }

            financier.UpdateAddress(model.AddressId, AddressVO.Create(model.AddressLine1, model.AddressLine2, model.City, model.StateCode, model.Zipcode));

            _financierRepo.Update(financier);
            await _unitOfWork.Commit();
        }

        private async Task HandleDeleteAddress(DeleteFinancierAddressInfo model)
        {
            var financier = await _financierRepo.GetByIdAsync(model.FinancierId);

            if (financier is null)
            {
                throw new InvalidOperationException($"Delete address failed, no financier with id '{model.FinancierId}' found!");
            }

            financier.DeleteAddress(model.AddressId);

            _financierRepo.Update(financier);
            await _unitOfWork.Commit();
        }

        private async Task HandleCreateContact(CreateFinancierContactInfo model)
        {
            Financier financier = await _financierRepo.GetByIdAsync(model.FinancierId);

            if (financier is null)
            {
                throw new InvalidOperationException($"Create contact person failed, no financier with id '{model.FinancierId}' found!");
            }

            financier.AddContactPerson(0, PersonName.Create(model.FirstName, model.LastName, model.MiddleInitial), PhoneNumber.Create(model.Telephone), model.Notes);

            _financierRepo.Update(financier);
            await _unitOfWork.Commit();
        }

        private async Task HandleUpdateContact(EditFinancierContactInfo model)
        {
            Financier financier = await _financierRepo.GetByIdAsync(model.FinancierId);

            if (financier is null)
            {
                throw new InvalidOperationException($"Update contact person failed, no financier with id '{model.FinancierId}' found!");
            }

            financier.UpdateContactPerson(model.PersonId, PersonName.Create(model.FirstName, model.LastName, model.MiddleInitial), PhoneNumber.Create(model.Telephone), model.Notes);

            _financierRepo.Update(financier);
            await _unitOfWork.Commit();
        }

        private async Task HandleDeleteContact(DeleteFinancierContactInfo model)
        {
            Financier financier = await _financierRepo.GetByIdAsync(model.FinancierId);

            if (financier is null)
            {
                throw new InvalidOperationException($"Delete contact person failed, no financier with id '{model.FinancierId}' found!");
            }

            financier.DeleteContactPerson(model.PersonId);

            _financierRepo.Update(financier);
            await _unitOfWork.Commit();
        }
    }
}