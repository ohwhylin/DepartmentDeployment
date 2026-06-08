using System;
using System.Collections.Generic;
using System.Linq;

namespace DepartmentContracts.ViewModels
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();

        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public int TotalPages =>
            PageSize <= 0
                ? 1
                : Math.Max(1, (int)Math.Ceiling((double)TotalCount / PageSize));

        public bool HasPreviousPage => Page > 1;
        public bool HasNextPage => Page < TotalPages;

        public static PagedResult<T> Create(IEnumerable<T>? source, int page, int pageSize)
        {
            var safePageSize = pageSize <= 0 ? 10 : pageSize;
            var list = source?.ToList() ?? new List<T>();

            var totalCount = list.Count;
            var totalPages = totalCount == 0
                ? 1
                : (int)Math.Ceiling((double)totalCount / safePageSize);

            var safePage = page < 1 ? 1 : page;
            if (safePage > totalPages)
            {
                safePage = totalPages;
            }

            return new PagedResult<T>
            {
                Page = safePage,
                PageSize = safePageSize,
                TotalCount = totalCount,
                Items = list
                    .Skip((safePage - 1) * safePageSize)
                    .Take(safePageSize)
                    .ToList()
            };
        }
    }
}