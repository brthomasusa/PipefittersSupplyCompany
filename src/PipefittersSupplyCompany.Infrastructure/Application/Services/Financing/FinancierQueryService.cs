using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using PipefittersSupplyCompany.Infrastructure.Persistence;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing.ReadModels;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing.QueryParameters;

namespace PipefittersSupplyCompany.Infrastructure.Application.Services.Financing
{
    public class FinancierQueryService : IFinancierQueryService
    {
        public Task<PagedList<FinancierListItem>> Query(GetFinanciers queryParameters)
        {
            throw new NotImplementedException();
        }

        public Task<FinancierDetail> Query(GetFinancier queryParameters)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<FinancierAddressListItem>> Query(GetFinancierAddresses queryParameters)
        {
            throw new NotImplementedException();
        }

        public Task<FinancierAddressDetail> Query(GetFinancierAddress queryParameters)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<FinancierContactListItem>> Query(GetFinancierContacts queryParameters)
        {
            throw new NotImplementedException();
        }

        public Task<FinancierContactDetail> Query(GetFinancierContact queryParameters)
        {
            throw new NotImplementedException();
        }
    }
}