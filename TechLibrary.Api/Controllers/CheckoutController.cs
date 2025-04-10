using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechLibrary.Api.UseCases.Checkouts;

namespace TechLibrary.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        [HttpPost]
        [Route("{bookId}")] // avoid url with "/checkouts?bookId=3453-345-345", this way will be "checkouts/3453-345-345"
        public IActionResult BookCheckout(Guid bookId)
        {
            var useCase = new RegisterBookCheckoutUseCase();

            useCase.Execute(bookId);

            return NoContent();
        }
    }
}
