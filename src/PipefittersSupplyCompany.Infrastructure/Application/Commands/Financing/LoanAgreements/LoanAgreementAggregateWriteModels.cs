using System;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.Infrastructure.Interfaces;


namespace PipefittersSupplyCompany.Infrastructure.Application.Commands.Financing.LoanAgreements
{
    public class CreateLoanAgreementInfo : IWriteModel
    {
        public Guid Id { get; set; }
        public Guid FinancierId { get; set; }
        public decimal LoanAmount { get; set; }
        public double InterestRate { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime MaturityDate { get; set; }
        public int PaymentsPerYear { get; set; }
        public UserId UserId { get; set; }
    }

    public class EditLoanAgreementInfo : IWriteModel
    {
        public Guid Id { get; set; }
        public decimal LoanAmount { get; set; }
        public double InterestRate { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime MaturityDate { get; set; }
        public int PaymentsPerYear { get; set; }
        public UserId UserId { get; set; }
    }

    public class DeleteLoanAgreementInfo : IWriteModel
    {
        public Guid Id { get; set; }
        public UserId UserId { get; set; }
    }
}

