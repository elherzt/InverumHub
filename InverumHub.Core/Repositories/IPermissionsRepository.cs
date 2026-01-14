using InverumHub.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverumHub.Core.Repositories
{
    public interface IPermissionsRepository
    {
        Task<CustomResponse> GetPermissionsByRoleAndApplication(string roleName, string applicationName);
    }
}
