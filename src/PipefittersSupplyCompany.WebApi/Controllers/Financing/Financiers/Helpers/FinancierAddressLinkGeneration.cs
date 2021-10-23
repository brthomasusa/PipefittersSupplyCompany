using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PipefittersSupplyCompany.WebApi.Utilities;

namespace PipefittersSupplyCompany.WebApi.Controllers.Financing.Financiers.Helpers
{
    public class FinancierAddressLinkGeneration
    {
        public static HashSet<Link> CreateLinks(HttpContext httpContext, LinkGenerator linkGenerator, int id)
        {
            var links = new HashSet<Link>
                {
                    new Link(linkGenerator.GetUriByAction(httpContext, "GetFinancierAddressDetails", values: new { addressId = id }), "self", "GET"),
                    new Link(linkGenerator.GetUriByAction(httpContext, "DeleteFinancierAddressInfo", values: new { addressId = id }), "delete_financier", "DELETE"),
                    new Link(linkGenerator.GetUriByAction(httpContext, "EditFinancierAddressInfo", values: new { addressId = id }), "update_financier", "PUT")
                };

            return links;
        }
    }
}