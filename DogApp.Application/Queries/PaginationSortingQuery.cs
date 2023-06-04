using System.Text.Json.Serialization;

namespace DogApp.Application.Queries
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

        [JsonPropertyName("attribute")]
        public string? SortingAttribute { get; set; }

        [JsonPropertyName("order")]
        public string? SortingOrder { get; set; }
    }
}
