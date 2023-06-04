using DogApp.Application.Dtos.Dog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DogApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        [HttpPost]
        public IActionResult Create([FromBody]AddDogDto addDogDto)
        {

            var res = ModelState.IsValid;
            return Ok();
        }



        [HttpGet]
        public IActionResult Get([FromQuery] FilterQuery filterQuery, [FromQuery] SortingQuery sortingQuery)
        {
            return Ok();
        }




    }

    public class FilterQuery
    {
        public string Name { get; set; }

        public string Size { get; set; }
    }

    public class SortingQuery
    {
        public string SotringParam { get; set; }

        public string SortingOrder { get; set; }
    }
}
