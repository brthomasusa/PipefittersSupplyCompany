using System;
using System.Collections.Generic;

namespace PipefittersSupplyCompany.Infrastructure.Application.Queries
{
    public class PagedList<T>
    {
        private List<T> _readModels = new List<T>();

        public List<T> ReadModels { get => _readModels; }

        public MetaData MetaData { get; set; }

        protected PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };

            _readModels.AddRange(items);
        }

        public static PagedList<T> CreatePagedList(List<T> source, int totalRecords, int pageNumber, int pageSize)
        {
            return new PagedList<T>(source, totalRecords, pageNumber, pageSize);
        }
    }
}