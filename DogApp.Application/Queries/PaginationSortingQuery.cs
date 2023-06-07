using Microsoft.AspNetCore.Mvc;

namespace DogApp.Application.Queries
{
    public sealed class PaginationSortingQuery
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        [FromQuery(Name = "Attribute")]
        public string? SortingAttribute { get; set; }

        [FromQuery(Name = "Order")]
        public string? SortingOrder { get; set; }

        public PaginationSortingQuery()
        {
            SortingAttribute = string.Empty;
        }
    }
}