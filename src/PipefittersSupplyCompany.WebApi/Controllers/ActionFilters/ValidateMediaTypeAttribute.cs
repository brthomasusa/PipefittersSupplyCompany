using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;

namespace PipefittersSupplyCompany.WebApi.Controllers.ActionFilters
{
    public class ValidateMediaTypeAttribute
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var acceptHeaderPresent = context.HttpContext.Request.Headers.ContainsKey("Accept");

            if (!acceptHeaderPresent)
            {
                context.Result = new BadRequestObjectResult("Accept header is missing.");
                return;
            }

            var mediaType = context.HttpContext.Request.Headers["Accept"].FirstOrDefault();

            if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue outMediaType))
            {
                context.Result = new BadRequestObjectResult("Media type not present. Please add accept header with required media type.");
                return;
            }

            context.HttpContext.Items.Add("AcceptHeaderMediaType", mediaType);
            await next();
        }
    }
}