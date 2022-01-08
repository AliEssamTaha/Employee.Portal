using Microsoft.AspNetCore.Identity;
using System;

namespace Employee.Portal.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public bool IsAdmin { get; set; }
    }
}
