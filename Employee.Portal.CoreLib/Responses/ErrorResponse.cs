using System.Collections.Generic;

namespace Employee.Portal.CoreLib.Responses
{
    public class ErrorResponse
    {
        public IEnumerable<string> errorMessages { get; set; }
        public ErrorResponse(IEnumerable<string> errorMessages)
        {
            this.errorMessages = errorMessages;
        }
    }
}
