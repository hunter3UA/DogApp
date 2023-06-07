using System.Collections;

namespace DogApp.Application.Models
{
    public sealed class PagedResponse<T> where T : IEnumerable
    {
        public T Data { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages { get; set; }

        public PagedResponse(T data)
        {
            Data = data;
        }
    }
}