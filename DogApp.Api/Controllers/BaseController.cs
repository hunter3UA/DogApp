using DogApp.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace DogApp.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected static ActionResult ExceptionResolver(Exception exception)
        {
            var errorResponse = new ErrorResponse { Message = exception.Message };

            return new BadRequestObjectResult(errorResponse);
        }
    }
}
