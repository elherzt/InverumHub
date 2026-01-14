using InverumHub.Core.Common;
using InverumHub.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverumHub.Core.Repositories
{
    public interface IUserRepository
    {
        Task<CustomResponse> Get(bool isActive);
        Task<CustomResponse> GetById(Guid uid);
        Task<CustomResponse> Create(CreateUserDTO user);
        Task<CustomResponse> Update(UpdateUserDTO user);
        Task<CustomResponse> ChangePassword(Guid userUid, ChangePasswordDTO user);
        Task<CustomResponse> ChangePassword(Guid userUid, ChangeOwnPasswordDTO model);
        Task<CustomResponse> GetByRole(int rol_id);
        Task<CustomResponse> AddRoleApplication(Guid user_id, int rol_id, int application_id);
        Task<CustomResponse> DeleteRoleApplication(Guid user_id, int rol_id, int application_id);
        Task<CustomResponse> Disable(Guid userUid);
        Task<CustomResponse> Enable(Guid userUid);



    }
}
