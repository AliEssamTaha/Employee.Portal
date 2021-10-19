using System.ComponentModel.DataAnnotations;

namespace Employee.Portal.CoreLib.Requests
{
    public class RefreshRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
