using Employee.Portal.Domain.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee.Portal.Domain.Entities
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }
        public virtual User User { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
    }
}
