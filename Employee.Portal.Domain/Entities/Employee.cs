using Employee.Portal.Domain.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee.Portal.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public virtual Department Department { get; set; }
        [ForeignKey(nameof(Department))]
        public int DepartmentId { get; set; }
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
        [NotMapped]
        public int Age => DateTime.Now.Subtract(BirthDate).Days / 365;
    }
}
