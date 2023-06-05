using Microsoft.AspNetCore.Mvc;

namespace DogApp.Api.Queries
{
    public class PaginationSortingQuery
    {
        private const int MaxPageSize = 30;

        public PaginationSortingQuery()
        {
            PageSize = PageSize <= 0 || PageSize >= MaxPageSize ? MaxPageSize : PageSize;
            PageNumber = PageNumber < 1 ? 1 : PageNumber;
            SortingAttribute = string.Empty;
        }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        [FromQuery(Name = "Attribute")]
        public string? SortingAttribute { get; set; }

        [FromQuery(Name ="Order")]
        public string? SortingOrder { get; set; }
    }
}
