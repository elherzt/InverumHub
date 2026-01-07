using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverumHub.Core.Entities
{
    public class Application
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Alias { get; set; } = default!;
        public string Description { get; set; } = default!;
        public  bool IsActive { get; set; }

        // relations
        public ICollection<UserApplicationRole> UserRoles { get; set; } = new List<UserApplicationRole>();
    }
}
