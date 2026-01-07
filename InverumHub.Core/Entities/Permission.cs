using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverumHub.Core.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        public string Code { get; set; } = default!;
        public string Description { get; set; } = default!;


        // relations
        public ICollection<RolePermission> Roles { get; set; } = new List<RolePermission>();
    }
}
