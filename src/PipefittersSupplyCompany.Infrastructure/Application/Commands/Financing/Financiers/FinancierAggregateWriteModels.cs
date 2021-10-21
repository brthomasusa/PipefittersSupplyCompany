// FinancierAggregateWriteModels
using System;
using PipefittersSupplyCompany.Infrastructure.Interfaces;

namespace PipefittersSupplyCompany.Infrastructure.Application.Commands.Financing.Financiers
{
    public class CreateFinancierInfo : IWriteModel
    {
        public Guid Id { get; set; }
        public string FinancierName { get; set; }
        public string Telephone { get; set; }
        public bool IsActive { get; set; }
        public Guid UserId { get; set; }
    }

    public class EditFinancierInfo : IWriteModel
    {
        public Guid Id { get; set; }
        public string FinancierName { get; set; }
        public string Telephone { get; set; }
        public bool IsActive { get; set; }
        public Guid UserId { get; set; }
    }

    public class DeleteFinancierInfo : IWriteModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }

    public class CreateFinancierAddressInfo : IWriteModel
    {
        public Guid FinancierId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string Zipcode { get; set; }
    }

    public class EditFinancierAddressInfo : IWriteModel
    {
        public int AddressId { get; set; }
        public Guid FinancierId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string Zipcode { get; set; }
    }

    public class DeleteFinancierAddressInfo : IWriteModel
    {
        public int AddressId { get; set; }
        public Guid FinancierId { get; set; }
    }

    public class CreateFinancierContactInfo : IWriteModel
    {
        public Guid FinancierId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleInitial { get; set; }
        public string Telephone { get; set; }
        public string Notes { get; set; }
    }

    public class EditFinancierContactInfo : IWriteModel
    {
        public int PersonId { get; set; }
        public Guid FinancierId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleInitial { get; set; }
        public string Telephone { get; set; }
        public string Notes { get; set; }
    }

    public class DeleteFinancierContactInfo : IWriteModel
    {
        public int PersonId { get; set; }
        public Guid FinancierId { get; set; }
    }
}