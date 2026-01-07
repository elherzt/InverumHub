using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverumHub.Core.Entities
{
    public class UserApplicationRole
    {
        public Guid UserUid { get; set; }
        public int ApplicationId { get; set; }
        public int RoleId { get; set; }

        // relations
        public User User { get; set; } = default!;
        public Application Application { get; set; } = default!;
        public Role Role { get; set; } = default!;

    }
}
