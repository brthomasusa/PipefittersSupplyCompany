using System;
using PipefittersSupplyCompany.Infrastructure.Interfaces;

namespace PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing
{
    public class FinancierDependencyCheckResult : IReadModel
    {
        public Guid FinancierId { get; set; }
        public int Addresses { get; set; }
        public int Contacts { get; set; }
        public int LoanAgreements { get; set; }
        public int StockSubscriptions { get; set; }
    }

    public class FinancierDetail : IReadModel
    {
        public Guid FinancierId { get; set; }
        public string FinancierName { get; set; }
        public string Telephone { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public Guid UserId { get; set; }
    }

    public class FinancierListItem : IReadModel
    {
        public Guid FinancierId { get; set; }
        public string FinancierName { get; set; }
        public string Telephone { get; set; }
        public bool IsActive { get; set; }
    }

    public class FinancierAddressListItem : IReadModel
    {
        public Guid FinancierId { get; set; }
        public int AddressId { get; set; }
        public string FullAddress { get; set; }
    }

    public class FinancierAddressDetail : IReadModel
    {
        public int AddressId { get; set; }
        public Guid FinancierId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string Zipcode { get; set; }
    }

    public class FinancierContactDetail : IReadModel
    {
        public int PersonId { get; set; }
        public Guid FinancierId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string Telephone { get; set; }
        public string Notes { get; set; }
    }

    public class FinancierContactListItem : IReadModel
    {
        public int PersonId { get; set; }
        public Guid FinancierId { get; set; }
        public string FullName { get; set; }
    }
}