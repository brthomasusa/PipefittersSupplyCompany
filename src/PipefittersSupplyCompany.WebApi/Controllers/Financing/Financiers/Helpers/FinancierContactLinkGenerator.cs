using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PipefittersSupplyCompany.WebApi.Utilities;

namespace PipefittersSupplyCompany.WebApi.Controllers.Financing.Financiers.Helpers
{
    public class FinancierContactLinkGenerator
    {
        public static HashSet<Link> CreateLinks(HttpContext httpContext, LinkGenerator linkGenerator, int id)
        {
            var links = new HashSet<Link>
                {
                    new Link(linkGenerator.GetUriByAction(httpContext,
                                                          action: "GetFinancierContactDetails",
                                                          controller: "FinancierContacts",
                                                          values: new { personId = id }),
                                                          "self", "GET"),
                    new Link(linkGenerator.GetUriByAction(httpContext,
                                                          "DeleteFinancierContactInfo",
                                                          controller: "FinancierContacts",
                                                          values: new {  }),
                                                          "delete_financiercontact", "DELETE"),
                    new Link(linkGenerator.GetUriByAction(httpContext,
                                                          "EditFinancierContactInfo",
                                                          controller: "FinancierContacts",
                                                          values: new {  }),
                                                          "update_financiercontact", "PUT")
                };

            return links;
        }
    }
}