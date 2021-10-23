using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PipefittersSupplyCompany.WebApi.Utilities;

namespace PipefittersSupplyCompany.WebApi.Controllers.Financing.Financiers.Helpers
{
    public class FinancierContactLinkGeneration
    {
        public static HashSet<Link> CreateLinks(HttpContext httpContext, LinkGenerator linkGenerator, int id)
        {
            var links = new HashSet<Link>
                {
                    new Link(linkGenerator.GetUriByAction(httpContext, "GetFinancierContactDetails", values: new { personId = id }), "self", "GET"),
                    new Link(linkGenerator.GetUriByAction(httpContext, "DeleteFinancierContactInfo", values: new { personId = id }), "delete_financiercontact", "DELETE"),
                    new Link(linkGenerator.GetUriByAction(httpContext, "EditFinancierContactInfo", values: new { personId = id }), "update_financiercontact", "PUT")
                };

            return links;
        }
    }
}