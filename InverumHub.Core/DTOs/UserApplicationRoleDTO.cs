using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverumHub.Core.DTOs
{
    public class UserApplicationRoleDTO
    {
        public int ApplicationId { get; set; }
        public string ApplicationName { get; set; } = default!;
        public List<RoleDTO> Roles { get; set; } = default!;
    }
}
