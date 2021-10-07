using System;

namespace PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing
{
    public static class QueryParameters
    {
        public class GetFinancier
        {
            public Guid FinancierID { get; set; }
        }

        public class GetFinanciers
        {
            public int Page { get; set; }
            public int PageSize { get; set; }
        }

        public class GetFinancierAddress
        {
            public int AddressID { get; set; }
        }

        public class GetFinancierAddresses
        {
            public Guid FinancierID { get; set; }
            public int Page { get; set; }
            public int PageSize { get; set; }
        }

        public class GetFinancierContact
        {
            public int PersonID { get; set; }
        }

        public class GetFinancierContacts
        {
            public Guid FinancierID { get; set; }
            public int Page { get; set; }
            public int PageSize { get; set; }
        }
    }
}