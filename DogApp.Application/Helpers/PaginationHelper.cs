namespace DogApp.Application.Helpers
{
    public class PaginationHelper
    {
        /// <summary>
        /// This method check our currentPage. 
        /// If <paramref name="totalItems"/> less than or equal 0, method returns default values.
        /// If <paramref name="pageSize"/> greater than totalPages, method retuns first page;
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalItems"></param>
        /// <returns></returns>
        public static (int PageNumber, int PageSize) FormatCurrentPage(int pageNumber, int pageSize, int totalItems)
        {
            if (totalItems <= 0)
                return (default, default);

            int totalPages = GetTotalPages(totalItems, pageSize);
            pageNumber = pageNumber > totalPages ? 1 : pageNumber;

            return (pageNumber, pageSize);
        }

        public static int Skip(int pageNumber, int pageSize)
        {
            return (pageNumber - 1) * pageSize;
        }

        private static int GetTotalPages(int totalItems, int elementsOnPage)
        {
            return (int)Math.Ceiling((decimal)totalItems / elementsOnPage);
        }
    }
}