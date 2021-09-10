using System.Collections.Generic;

namespace PipefittersSupplyCompany.Infrastructure.Application.LinkModels
{
    public class LinkResourceBase
    {
        public LinkResourceBase() { }

        public List<Link> Links { get; set; } = new List<Link>();
    }
}