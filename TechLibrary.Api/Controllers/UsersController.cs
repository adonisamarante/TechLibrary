using Microsoft.AspNetCore.Mvc;
using TechLibrary.Api.UseCases.Users.Register;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;

namespace TechLibrary.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // IActionResult enables you to return different types of response codes(200, 404, 500, etc.)
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
        public IActionResult Create(RequestUserJson request)
        { 
            var useCase = new RegisterUserUseCase();

            var response = useCase.Execute(request);

            return Created(string.Empty, response);
        }
    }
}
