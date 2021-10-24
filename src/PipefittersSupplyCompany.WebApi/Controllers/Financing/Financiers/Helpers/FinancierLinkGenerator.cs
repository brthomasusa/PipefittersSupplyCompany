using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PipefittersSupplyCompany.WebApi.Utilities;

namespace PipefittersSupplyCompany.WebApi.Controllers.Financing.Financiers.Helpers
{
    public class FinancierLinkGenerator
    {
        public static HashSet<Link> CreateLinks(HttpContext httpContext, LinkGenerator linkGenerator, Guid id)
        {
            var links = new HashSet<Link>
                {
                    new Link(linkGenerator.GetUriByAction(httpContext, "GetFinancierDetails", values: new { financierId = id }), "self", "GET"),
                    new Link(linkGenerator.GetUriByAction(httpContext, "DeleteFinancierInfo", values: new { financierId = id }), "delete_financier", "DELETE"),
                    new Link(linkGenerator.GetUriByAction(httpContext, "EditFinancierInfo", values: new { financierId = id }), "update_financier", "PUT"),
                    new Link(linkGenerator.GetUriByAction(httpContext, "PatchFinancierInfo", values: new { financierId = id }), "patch_financier", "PATCH"),
                    new Link(linkGenerator.GetUriByAction(httpContext, "GetFinancierAddresses", values: new { financierId = id }), "addresses", "GET"),
                    new Link(linkGenerator.GetUriByAction(httpContext, "GetFinancierContacts", values: new { financierId = id }), "contacts", "GET"),
                };

            return links;
        }
    }
}