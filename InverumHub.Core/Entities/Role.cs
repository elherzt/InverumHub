using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverumHub.Core.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;


        // relations
        public ICollection<RolePermission> Permissions { get; set; } = new List<RolePermission>();
        public ICollection<UserApplicationRole> UserApplication { get; set; } = new List<UserApplicationRole>();

    }
}
