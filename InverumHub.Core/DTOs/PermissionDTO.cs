using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverumHub.Core.DTOs
{
    public class PermissionDTO
    {
        public string Code { get; set; } = default!;
        public string Description { get; set; } = default!;
    }

    public class PermissionSysDTO
    {
        public int Id { get; set; } 
        public string Code { get; set; } = default!;
        public string Description { get; set; } = default!;
    }

    public class CreatePermissionDTO
    {
        [Required]
        public string Code { get; set; } = default!;
        [Required]
        public string Description { get; set; } = default!;
    }

    public class UpdatePermissionDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; } = default!;
        [Required]
        public string Description { get; set; } = default!;
    }


}
