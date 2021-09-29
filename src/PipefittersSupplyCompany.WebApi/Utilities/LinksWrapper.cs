using System.Collections.Generic;
using PipefittersSupplyCompany.WebApi.Interfaces;

namespace PipefittersSupplyCompany.WebApi.Utilities
{
    public class LinksWrapper<T> : ILinksWrapper
    {
        public T Value { get; set; }
        public HashSet<Link> Links { get; set; }
    }
}
