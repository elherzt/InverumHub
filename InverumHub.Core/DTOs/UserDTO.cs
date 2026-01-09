using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverumHub.Core.DTOs
{
    public class CreateUserDTO
    {
        [Required]
        public string FullName { get; set; } = default!;
        [Required]
        public string Email { get; set; } = default!;
        [Required]
        public string Password { get; set; } = default!;
    }

    public class UpdateUserDTO
    {
        [Required]
        public Guid Uid { get; set; }
        [Required]
        public string FullName { get; set; } = default!;
        [Required]
        public string Email { get; set; } = default!;
        [Required]
        public bool IsActive { get; set; }
        public string Password { get; set; } = default!;
    }

    public class UserDTO
    {
        public Guid Uid { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public bool IsActive { get; set; }

        public List<UserApplicationRoleDTO> ApplicationsRoles { get; set; } = new List<UserApplicationRoleDTO>();
    }
}
