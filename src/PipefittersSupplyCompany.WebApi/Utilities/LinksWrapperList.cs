using System.Collections.Generic;
using PipefittersSupplyCompany.WebApi.Interfaces;

namespace PipefittersSupplyCompany.WebApi.Utilities
{
    public class LinksWrapperList<T> : ILinksWrapper
    {
        public HashSet<LinksWrapper<T>> Values { get; set; } = new HashSet<LinksWrapper<T>>();
        public HashSet<Link> Links { get; set; }
    }
}