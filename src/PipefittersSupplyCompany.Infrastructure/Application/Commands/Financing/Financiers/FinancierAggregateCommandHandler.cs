using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Interfaces.Financing;

namespace PipefittersSupplyCompany.Infrastructure.Application.Commands.Financing.Financiers
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
                CreateFinancierInfo model => FinancierWriteCommands.Execute(model, _financierRepo, _unitOfWork),
                EditFinancierInfo model => FinancierWriteCommands.Execute(model, _financierRepo, _unitOfWork),
                DeleteFinancierInfo model => FinancierWriteCommands.Execute(model, _financierRepo, _unitOfWork),
                CreateFinancierAddressInfo model => FinancierAddressWriteCommand.Execute(model, _financierRepo, _unitOfWork),
                EditFinancierAddressInfo model => FinancierAddressWriteCommand.Execute(model, _financierRepo, _unitOfWork),
                DeleteFinancierAddressInfo model => FinancierAddressWriteCommand.Execute(model, _financierRepo, _unitOfWork),
                CreateFinancierContactInfo model => FinancierContactWriteCommand.Execute(model, _financierRepo, _unitOfWork),
                EditFinancierContactInfo model => FinancierContactWriteCommand.Execute(model, _financierRepo, _unitOfWork),
                DeleteFinancierContactInfo model => FinancierContactWriteCommand.Execute(model, _financierRepo, _unitOfWork),
                _ => throw new System.ArgumentOutOfRangeException("Unknown financier write command.", nameof(writeModel))
            };
    }
}