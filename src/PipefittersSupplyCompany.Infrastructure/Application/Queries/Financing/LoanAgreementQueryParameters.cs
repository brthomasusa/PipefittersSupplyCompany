using System;
using PipefittersSupplyCompany.Infrastructure.Interfaces;

namespace PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing
{
    public class DoLoanAgreementDependencyCheck
    {
        public Guid LoanId { get; set; }
    }

    public class GetLoanAgreement
    {
        public Guid LoanId { get; set; }
    }

    public class GetLoanAgreements
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class GetLoanAgreementsForFinancier
    {
        public Guid FinancierId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}