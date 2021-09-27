using System.Collections.Generic;

namespace PipefittersSupplyCompany.WebApi.Utilities
{
    public class LinksWrapperList<T>
    {
        public HashSet<LinksWrapper<T>> Values { get; set; } = new HashSet<LinksWrapper<T>>();
        public HashSet<Link> Links { get; set; }
    }
}