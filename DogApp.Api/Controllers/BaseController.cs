using System.Collections;
using DogApp.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace DogApp.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected static ActionResult ExceptionResolver(Exception exception)
        {
            var errorResponse = new ErrorModel { Message = exception.Message };

            return new BadRequestObjectResult(new ErrorResponse(errorResponse));
        }

        protected static ActionResult CreatePagedResult<T>(
            T data, int PageNumber, int PageSize, int totalItems, int totalPages) where T : IEnumerable
        {
            var pagedResponse = new PagedResponse<T>(data)
            {
                PageNumber = PageNumber + 1,
                PageSize = PageSize,
                TotalItems = totalItems,
                TotalPages = totalPages
            };

            return new OkObjectResult(pagedResponse);
        }
    }
}
