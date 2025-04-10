using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TechLibrary.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        [HttpPost]
        public IActionResult BookCheckout(Guid bookId)
        {
            return NoContent();
        }
    }
}
