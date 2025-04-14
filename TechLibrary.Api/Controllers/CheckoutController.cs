using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechLibrary.Api.Services.LoggedUser;
using TechLibrary.Api.UseCases.Checkouts;

namespace TechLibrary.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize] // add auth to the controller, all endpoints here will require authorization, if it was added to the method, only that method would require authorization
    public class CheckoutController : ControllerBase
    {
        [HttpPost]
        [Route("{bookId}")] // avoid url with "/checkouts?bookId=3453-345-345", this way will be "checkouts/3453-345-345"
        public IActionResult BookCheckout(Guid bookId)
        {
            var loggedUser = new LoggedUserService(HttpContext);

            var useCase = new RegisterBookCheckoutUseCase(loggedUser);

            useCase.Execute(bookId);

            return NoContent();
        }
    }
}
