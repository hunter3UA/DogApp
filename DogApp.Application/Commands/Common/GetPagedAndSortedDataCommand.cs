using DogApp.Domain.Enums;
using LanguageExt.Common;
using MediatR;

namespace DogApp.Application.Commands.Common
{
    public class GetPagedAndSortedDataCommand<T> : IRequest<Result<IEnumerable<T>>>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string? SortingAttribute { get; set; }

        public string? SortingOrder { get; set; }
    }
}
