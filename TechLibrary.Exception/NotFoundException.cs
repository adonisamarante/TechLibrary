using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TechLibrary.Exception
{
    public class NotFoundException : TechLibraryException
    {
        // this exception will be expecting a message, base means it will be passed to the base class, in this case TechLibraryException
        // TechLibraryException will need to have a constructor that accepts the string message
        public NotFoundException(string message) : base(message)
        {
            
        }

        public override List<string> GetErrorMessages() => [Message]; // this message is from System.Exception the base class in TechLibraryException

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
    }
}
