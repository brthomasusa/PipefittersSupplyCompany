using System;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Interfaces.Financing;
using PipefittersSupplyCompany.Core.Financing.LoanAgreementAggregate;

namespace PipefittersSupplyCompany.Infrastructure.Application.Commands.Financing.LoanAgreements
{
    public class LoanAgreementUpdateCommand
    {
        public static async Task Execute
        (
            EditLoanAgreementInfo model,
            ILoanAgreementAggregateRepository loanAgreementRepo,
            IUnitOfWork unitOfWork
        )
        {
            string errMsg = $"Unable to locate loan agreement with LoanId '{model.Id}'!";
            LoanAgreement loanAgreement = await loanAgreementRepo.GetByIdAsync(model.Id) ?? throw new InvalidOperationException(errMsg);

            loanAgreement.UpdateLoanAmount(LoanAmount.Create(model.LoanAmount));
            loanAgreement.UpdateInterestRate(InterestRate.Create(model.InterestRate));
            loanAgreement.UpdateLoanDate(LoanDate.Create(model.LoanDate));
            loanAgreement.UpdateMaturityDate(MaturityDate.Create(model.MaturityDate));
            loanAgreement.UpdatePaymentsPerYear(PaymentsPerYear.Create(model.PaymentsPerYear));
            loanAgreement.UpdateUserId(model.UserId);
            loanAgreement.UpdateLastModifiedDate();

            await unitOfWork.Commit();
        }
    }
}