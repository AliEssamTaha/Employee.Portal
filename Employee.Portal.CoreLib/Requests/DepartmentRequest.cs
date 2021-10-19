using System.ComponentModel.DataAnnotations;

namespace Employee.Portal.CoreLib.Requests
{
    public class DepartmentRequest
    {
        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

    }
}
