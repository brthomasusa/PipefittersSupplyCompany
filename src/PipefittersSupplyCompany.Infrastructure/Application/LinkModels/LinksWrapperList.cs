using System.Collections.Generic;

namespace PipefittersSupplyCompany.Infrastructure.Application.LinkModels
{
    public class LinksWrapperList<T>
    {
        public HashSet<LinksWrapper<T>> Values { get; set; }
        public HashSet<Link> Links { get; set; }
    }
}