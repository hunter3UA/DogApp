using Microsoft.AspNetCore.Mvc;

namespace DogApp.Api.Queries
{
    public class PaginationSortingQuery
    {
        public PaginationSortingQuery() 
        {
            SortingAttribute = string.Empty;
        }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        [FromQuery(Name = "Attribute")]
        public string? SortingAttribute { get; set; }

        [FromQuery(Name = "Order")]
        public string? SortingOrder { get; set; }
    }
}
