using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using System.Linq;


namespace PipefittersSupplyCompany.WebApi.ActionFilters
{
    public class ValidateMediaTypeAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var acceptHeaderPresent = context.HttpContext.Request.Headers.ContainsKey("Accept");

            if (!acceptHeaderPresent)
            {
                context.Result = new BadRequestObjectResult($"Accept header is missing.");
                return;
            }

            var mediaType = context.HttpContext.Request.Headers["Accept"].FirstOrDefault();

            if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue outMediaType))
            {
                var msg = @"
                Media type not present. 
                Please add Accept header with the required media type (at least '*/*') to the request. 
                If you want HATEOAS links in the response then add: 
                'application/vnd.btechnical-consulting.hateoas+json'";

                context.Result = new BadRequestObjectResult(msg);
                return;
            }

            context.HttpContext.Items.Add("AcceptHeaderMediaType", outMediaType);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}