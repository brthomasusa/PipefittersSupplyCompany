using System;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Interfaces.Financing;
using PipefittersSupplyCompany.Core.Financing.LoanAgreementAggregate;

namespace PipefittersSupplyCompany.Infrastructure.Application.Commands.Financing.LoanAgreements
{
    public class LoanAgreementDeleteCommand
    {
        public static async Task Execute
        (
            DeleteLoanAgreementInfo model,
            ILoanAgreementAggregateRepository loanAgreementRepo,
            IUnitOfWork unitOfWork
        )
        {
            string errMsg = $"Unable to delete loan agreement info, a loan agreement with LoanId '{model.Id}' could not be found!";
            LoanAgreement loanAgreement = await loanAgreementRepo.GetByIdAsync(model.Id) ?? throw new InvalidOperationException(errMsg);

            loanAgreementRepo.Delete(loanAgreement);
            await unitOfWork.Commit();
        }
    }
}