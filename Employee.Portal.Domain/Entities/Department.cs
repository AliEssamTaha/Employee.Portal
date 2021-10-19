using Employee.Portal.Domain.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Employee.Portal.Domain.Entities
{
    public class Department : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
