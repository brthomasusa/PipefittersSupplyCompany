using System.Collections.Generic;
using PipefittersSupplyCompany.Infrastructure.Application.LinkModels;

namespace PipefittersSupplyCompany.Infrastructure.Application.Queries
{
    public abstract class ReadModelBase
    {
        public HashSet<Link> Links { get; set; } = new HashSet<Link>();
    }
}