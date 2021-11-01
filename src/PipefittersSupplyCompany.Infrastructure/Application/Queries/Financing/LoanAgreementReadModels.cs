using System;
using PipefittersSupplyCompany.Infrastructure.Interfaces;

namespace PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing
{
    public class LoanAgreementDependencyCheckResult : IReadModel
    {
        public Guid LoanId { get; set; }
        public int CashTransactions { get; set; }
        public int ScheduledLoanPayments { get; set; }
    }

    public class LoanAgreementDetail : IReadModel
    {
        public Guid LoanId { get; set; }
        public Guid FinancierId { get; set; }
        public decimal LoanAmount { get; set; }
        public double InterestRate { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime MaturityDate { get; set; }
        public int PaymentsPerYear { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public Guid UserId { get; set; }
    }

    public class LoanAgreementListItems : IReadModel
    {
        public Guid LoanId { get; set; }
        public Guid FinancierId { get; set; }
        public decimal LoanAmount { get; set; }
        public double InterestRate { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime MaturityDate { get; set; }
        public int PaymentsPerYear { get; set; }
    }

    public class LoanAgreementFinancierInfoListItems : IReadModel
    {
        public Guid LoanId { get; set; }
        public Guid FinancierId { get; set; }
        public string FinancierName { get; set; }
        public string Telephone { get; set; }
        public decimal LoanAmount { get; set; }
        public double InterestRate { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime MaturityDate { get; set; }
        public int PaymentsPerYear { get; set; }
        public string UserName { get; set; }
    }


}