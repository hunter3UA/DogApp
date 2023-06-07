namespace DogApp.Application.Helpers
{
    public class PaginationHelper
    {
        private const int DefaultPageNumber = 1;
        private const int MaxPageSize = 30;

        public static (int PageNumber, int PageSize, int TotalPages) FilterSettings(int pageNumber, int pageSize, int totalItems)
        {
            pageSize = pageSize <= 0 || pageSize > MaxPageSize ? MaxPageSize : pageSize;         
            var totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);
            pageNumber = pageNumber < DefaultPageNumber || pageNumber > totalPages ? DefaultPageNumber - 1 : pageNumber - 1;

            return (pageNumber, pageSize, totalPages);
        }
    }
}