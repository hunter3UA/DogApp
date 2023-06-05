using LanguageExt.Common;
using MediatR;

namespace DogApp.Application.Commands.Common
{
    public sealed class GetPagedAndSortedDataCommand<T> : IRequest<Result<List<T>>>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string? SortingAttribute { get; set; }

        public string? SortingOrder { get; set; }
    }
}
