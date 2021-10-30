using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Interfaces.Financing;

namespace PipefittersSupplyCompany.Infrastructure.Application.Commands.Financing.LoanAgreements
{
    public class LoanAgreementAggregateCommandHandler : ICommandHandler
    {
        private readonly IFinancierAggregateRepository _financierRepo;
        private readonly ILoanAgreementAggregateRepository _loanAgreementRepo;
        private readonly IUnitOfWork _unitOfWork;

        public LoanAgreementAggregateCommandHandler
        (
            IFinancierAggregateRepository financierRepo,
            ILoanAgreementAggregateRepository loanAgreementRepo,
            IUnitOfWork unitOfWork
        )
        {
            _financierRepo = financierRepo;
            _loanAgreementRepo = loanAgreementRepo;
            _unitOfWork = unitOfWork;
        }

        public Task Handle(IWriteModel writeModel) =>
            writeModel switch
            {
                CreateLoanAgreementInfo model => LoanAgreementCreateCommand.Execute(model, _loanAgreementRepo, _financierRepo, _unitOfWork),
                EditLoanAgreementInfo model => LoanAgreementUpdateCommand.Execute(model, _loanAgreementRepo, _unitOfWork),
                DeleteLoanAgreementInfo model => LoanAgreementDeleteCommand.Execute(model, _loanAgreementRepo, _unitOfWork),

                // CreateLoanAgreementAddressInfo model => LoanAgreementAddressWriteCommand.Execute(model, _repo, _unitOfWork),
                // EditLoanAgreementAddressInfo model => LoanAgreementAddressWriteCommand.Execute(model, _repo, _unitOfWork),
                // DeleteLoanAgreementAddressInfo model => LoanAgreementAddressWriteCommand.Execute(model, _repo, _unitOfWork),
                // CreateLoanAgreementContactInfo model => LoanAgreementContactWriteCommand.Execute(model, _repo, _unitOfWork),
                // EditLoanAgreementContactInfo model => LoanAgreementContactWriteCommand.Execute(model, _repo, _unitOfWork),
                // DeleteLoanAgreementContactInfo model => LoanAgreementContactWriteCommand.Execute(model, _repo, _unitOfWork),

                _ => throw new System.ArgumentOutOfRangeException("Unknown LoanAgreement write command.", nameof(writeModel))
            };
    }
}