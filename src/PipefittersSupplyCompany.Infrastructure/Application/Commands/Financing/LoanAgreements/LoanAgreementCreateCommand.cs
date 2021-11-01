using System;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Interfaces.Financing;
using PipefittersSupplyCompany.Core.Financing.FinancierAggregate;
using PipefittersSupplyCompany.Core.Financing.LoanAgreementAggregate;
using PipefittersSupplyCompany.Core.Shared;


namespace PipefittersSupplyCompany.Infrastructure.Application.Commands.Financing.LoanAgreements
{
    public class LoanAgreementCreateCommand
    {
        public static async Task Execute
        (
            CreateLoanAgreementInfo model,
            ILoanAgreementAggregateRepository loanAgreementRepo,
            IFinancierAggregateRepository financierRepo,
            IUnitOfWork unitOfWork
        )
        {
            if (await loanAgreementRepo.Exists(model.Id))
            {
                throw new InvalidOperationException($"This loan agreement already exists!");
            }

            string errMsg = $"Unable to create loan agreement info, a financier with id {model.FinancierId} could not be found!";
            Financier financier = await financierRepo.GetByIdAsync(model.FinancierId) ?? throw new InvalidOperationException(errMsg);

            LoanAgreement loanAgreement = new LoanAgreement
            (
                model.Id,
                financier,
                LoanAmount.Create(model.LoanAmount),
                InterestRate.Create(model.InterestRate),
                LoanDate.Create(model.LoanDate),
                MaturityDate.Create(model.MaturityDate),
                PaymentsPerYear.Create(model.PaymentsPerYear),
                model.UserId
            );

            await loanAgreementRepo.AddAsync(loanAgreement);
            await unitOfWork.Commit();
        }
    }
}