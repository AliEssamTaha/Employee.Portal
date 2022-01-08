using System;
using System.ComponentModel.DataAnnotations;

namespace Employee.Portal.CoreLib.Requests
{
    public class EmployeeRequest
    {
        public int? Id { get; set; }
        [Required] 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }

    }
}
