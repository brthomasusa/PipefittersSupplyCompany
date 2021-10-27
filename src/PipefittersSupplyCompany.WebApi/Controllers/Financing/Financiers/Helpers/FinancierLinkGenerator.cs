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
                    new Link(linkGenerator.GetUriByAction(httpContext,
                                                          action: "GetFinancierDetails",
                                                          controller: "Financiers",
                                                          values: new { financierId = id }),
                                                          "self", "GET"),
                    new Link(linkGenerator.GetUriByAction(httpContext,
                                                          action: "DeleteFinancierInfo",
                                                          controller: "Financiers",
                                                          values: new {  }),
                                                          "delete_financier", "DELETE"),
                    new Link(linkGenerator.GetUriByAction(httpContext,
                                                          action: "EditFinancierInfo",
                                                          controller: "Financiers",
                                                          values: new {  }),
                                                          "update_financier", "PUT"),
                    new Link(linkGenerator.GetUriByAction(httpContext,
                                                          action: "PatchFinancierInfo",
                                                          controller: "Financiers",
                                                          values: new { financierId = id }),
                                                          "patch_financier", "PATCH"),
                    new Link(linkGenerator.GetUriByAction(httpContext,
                                                          action: "GetFinancierAddresses",
                                                          controller: "Financiers",
                                                          values: new { financierId = id }),
                                                          "addresses", "GET"),
                    new Link(linkGenerator.GetUriByAction(httpContext,
                                                          action: "GetFinancierContacts",
                                                          controller: "Financiers",
                                                          values: new { financierId = id }),
                                                          "contacts", "GET"),
                };

            return links;
        }
    }
}