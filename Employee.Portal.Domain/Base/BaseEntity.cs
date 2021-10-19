using System;

namespace Employee.Portal.Domain.Base
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime CreationOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
