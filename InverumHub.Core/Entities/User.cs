using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverumHub.Core.Entities
{
    public class User
    {
        public Guid Uid {  get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public bool IsActive { get; set; }

        // relations
        public ICollection<UserApplicationRole> ApplicationRoles { get; set; } = new List<UserApplicationRole>();

    }
}
