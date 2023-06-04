namespace DogApp.Application.Helpers
{
    public class PaginationHelper
    {
        private const int MaxPageSize = 30;

        public static int FormatCurrentPage(int currentPage, int elementsOnPage, int totalItems)
        {
            int totalPages = GetTotalPages(totalItems, elementsOnPage);
            currentPage = currentPage > totalPages || currentPage <= 0 ? 1 : currentPage;

            return currentPage;
        }

        private static int GetTotalPages(int totalItems, int elementsOnPage)
        {
            return (int)Math.Ceiling((decimal)totalItems / elementsOnPage);
        }
    }
}
/*
    public static ItemPageDTO<T> CreatePage(int currentPage, int elementsOnPage, int totalItems, List<T> items)
        {
            return new ItemPageDTO<T>()
            {
                Items = items,
                PageInfo = new PageInfoDTO
                {
                    CurrentPageNumber = currentPage,
                    ElementsOnPage = elementsOnPage,
                    TotalItems = totalItems
                }
            };    
        }

        public static int CheckCurrentPage(int currentPage, int elementsOnPage,int totalItemss)
        {
            PageInfoDTO pageInfo = new PageInfoDTO { CurrentPageNumber = currentPage, ElementsOnPage = elementsOnPage, TotalItems = totalItemss };

            currentPage = currentPage > pageInfo.TotalPages || currentPage <= 0 ? 1 : currentPage;

            return currentPage;
        }
 */