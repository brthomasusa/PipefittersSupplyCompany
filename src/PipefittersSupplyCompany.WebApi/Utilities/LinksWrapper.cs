using System.Collections.Generic;

namespace PipefittersSupplyCompany.WebApi.Utilities
{
    public class LinksWrapper<T>
    {
        public T Value { get; set; }
        public HashSet<Link> Links { get; set; }
    }
}