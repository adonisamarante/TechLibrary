using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;

namespace TechLibrary.Api.UseCases.Login.DoLogin
{
    public class DoLoginUseCase
    {
        public ResponseRegisteredUserJson Execute(RequestLoginJson request)
        {
            return new ResponseRegisteredUserJson
            {

            };
        }
    }
}
