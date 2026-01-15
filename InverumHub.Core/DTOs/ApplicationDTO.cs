using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverumHub.Core.DTOs
{
    public class ApplicationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Alias { get; set; } = default!;
        public string Description { get; set; } = default!;
        public bool IsActive { get; set; }
    }

    public class CreateAndUpdateApplicationDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = default!;
        [Required]
        public string Alias { get; set; } = default!;
        [Required]
        public string Description { get; set; } = default!;
        [Required]
        public bool IsActive { get; set; }
    }

   
}
