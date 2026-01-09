using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverumHub.Core.DTOs
{
    public class RoleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
    }

    public class CreateRoleDTO
    {
        [Required]
        public string Name { get; set; } = default!;
        [Required]
        public string Description { get; set; } = default!;
    }
}
