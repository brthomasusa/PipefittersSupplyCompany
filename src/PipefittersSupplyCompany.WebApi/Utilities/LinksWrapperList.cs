using System.Collections.Generic;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;

namespace PipefittersSupplyCompany.WebApi.Utilities
{
    public class LinksWrapperList<T> : ILinksWrapper
    {
        public HashSet<LinksWrapper<T>> Values { get; set; } = new HashSet<LinksWrapper<T>>();

        public MetaData MetaData { get; set; }

        public HashSet<Link> Links { get; set; }
    }
}