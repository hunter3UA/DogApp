using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;

namespace DogApp.Api.Extensions
{
    public static class ControllerExtensions
    {
        public static ActionResult ToOk<TResult>(this Result<TResult> result)
            
        {
            return result.Match<ActionResult>(obj =>
            {
                return new ObjectResult(obj);
             
            }, excepiton =>
            {
                return new BadRequestObjectResult("My exception");
            });
        }
    }
}
