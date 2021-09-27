using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PipefittersSupplyCompany.Infrastructure.Interfaces;

namespace PipefittersSupplyCompany.Infrastructure.Application.Queries
{
    public class PagedList<T> : List<T>
    {
        public MetaData MetaData { get; set; }

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };

            AddRange(items);
        }

        public static PagedList<T> CreatePagedList(List<T> source, int totalRecords, int pageNumber, int pageSize)
        {
            return new PagedList<T>(source, totalRecords, pageNumber, pageSize);
        }
    }
}