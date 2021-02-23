using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inlook_API.Helpers
{
    /// <summary>
    /// Helper class for paging user request
    /// </summary>
    public static class Paging
    {
        /// <summary>
        /// Gets single page of items
        /// </summary>
        /// <typeparam name="T">Item type.</typeparam>
        /// <param name="items">List of items to get page from.</param>
        /// <param name="page">Page number, starts from 0.</param>
        /// <param name="pageSize">Single page size</param>
        /// <param name="searchText">Text for filtering.</param>
        /// <param name="searchPropSelectors">List of parameters selectors for searchText to search in.</param>
        /// <param name="orderType">"asc" or "desc".</param>
        /// <param name="orderBySelector">Parameter selector for ordering.</param>
        /// <returns>(Filtered page of items, total count of items after searchText  application.</returns>
        public static (IEnumerable<T>, int totalCount) GetPage<T>(IEnumerable<T> items,
            int? page,
            int? pageSize,
            string searchText,
            IEnumerable<Func<T, string>> searchPropSelectors,
            string orderType,
            Func<T, object> orderBySelector
           )
        {
            if (!string.IsNullOrEmpty(searchText))
            {
                items = items
                    .Where(u => searchPropSelectors.Any(sel => sel(u).Contains(searchText)));
            }

            int totalCount = items.Count();

            if (orderType == "desc")
            {
                if(orderBySelector != null)
                {
                    items = items.OrderByDescending(orderBySelector);
                }
            }
            else
            {
                if (orderBySelector != null)
                {
                    items = items.OrderBy(orderBySelector);
                }
            }

            if (page.HasValue && pageSize.HasValue)
            {
                items = items.Skip(page.Value * pageSize.Value);
                items = items.Take(pageSize.Value);
            }

            return (items, totalCount);
        }
    }
}
