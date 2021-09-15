using System.Collections.Generic;

namespace PipefittersSupplyCompany.Infrastructure.Application.LinkModels
{
    public class LinksWrapper<T>
    {
        public T Value { get; set; }
        public HashSet<Link> Links { get; set; }
    }
}