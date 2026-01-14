using InverumHub.Core.Common;
using InverumHub.Core.Enums;
using InverumHub.Core.Repositories;
using InverumHub.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverumHub.DataLayer.Repositories
{
    public class PermissionsRepository : IPermissionsRepository
    {
        private readonly InverumDbContext _context;
        public PermissionsRepository(InverumDbContext context)
        {
            _context = context;
        }
        public async Task<CustomResponse> GetPermissionsByRoleAndApplication(string roleName, string applicationName)
        {
            CustomResponse response = new CustomResponse(TypeOfResponse.OK, "Permission retrieved sucessfully");
            try
            {
                var permissions = await _context.Roles
                 .Where(r => r.Name == roleName)
                 .SelectMany(r => r.Permissions)
                 .Select(rp => rp.Permission)
                 .ToListAsync();

                if (permissions == null || !permissions.Any())
                {
                    response.TypeOfResponse = TypeOfResponse.NotFound;
                    response.Message = "No permissions found for the specified role and application.";
                    return response;
                }

                response.Data = permissions;
            }
            catch (Exception ex)
            {
                response.TypeOfResponse = TypeOfResponse.Exception;
                response.Message = $"An error occurred while retrieving permissions: {ex.Message}";
            }
            return response;
        }
    }
}
