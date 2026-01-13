using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverumHub.Core.Common
{
    public class GlobalSessionModel
    {
        public int ApplicationId { get; set; }
        public string ApplicationName { get; set; } = default!;
        public Guid UserId { get; set; }
        public string UserName { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
        public List<string> RoleNames { get; set; } = new List<string>();
    }
}
