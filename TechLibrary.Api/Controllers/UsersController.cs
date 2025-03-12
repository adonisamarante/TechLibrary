using Microsoft.AspNetCore.Mvc;

namespace TechLibrary.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // IActionResult enables you to return different types of response codes(200, 404, 500, etc.)
        [HttpPost]
        public IActionResult Create()
        { 
            return Created();
        }
    }
}
