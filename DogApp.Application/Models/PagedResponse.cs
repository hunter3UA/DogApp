namespace DogApp.Application.Models
{
    public class PagedResponse<T>
    {
        public IEnumerable<T> Data { get; set; }

        public int? PageNumber { get; set; }

        public int? PageSize { get; set; }

        public PagedResponse(IEnumerable<T> data)
        {
            Data = data;
        }

    }

}
