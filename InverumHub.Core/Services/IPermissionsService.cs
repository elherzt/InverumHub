using AutoMapper;
using InverumHub.Core.Common;
using InverumHub.Core.DTOs;
using InverumHub.Core.Entities;
using InverumHub.Core.Enums;
using InverumHub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverumHub.Core.Services
{
    public interface IPermissionsService
    {
        Task<List<PermissionDTO>> ChekPermissions(GlobalSessionModel sessionModel, string roleName, string applicationName);
    }

    public class PermissionsService : IPermissionsService
    {
        private readonly IPermissionsRepository _permissionsRepository;
        private readonly IMapper _mapper;
        public PermissionsService(IPermissionsRepository permissionsRepository, IMapper mapper)
        {
            _permissionsRepository = permissionsRepository;
            _mapper = mapper;
        }
        public async Task<List<PermissionDTO>> ChekPermissions(GlobalSessionModel sessionModel, string roleName, string applicationName)
        {
            if (sessionModel.ApplicationName != applicationName)
            {
                throw new BusinessException("Invalid application context.");
            }
            if (!sessionModel.RoleNames.Contains(roleName))
            {
                throw new BusinessException("User does not have the required role.");
            }

            CustomResponse response = await _permissionsRepository.GetPermissionsByRoleAndApplication(roleName, applicationName);
            return response.TypeOfResponse switch
            {
                TypeOfResponse.OK => _mapper.Map<List<PermissionDTO>>((List<Permission>)response.Data),
                TypeOfResponse.FailedResponse => throw new BusinessException(response.Message),
                TypeOfResponse.NotFound => throw new NotFoundException(response.Message),
                _ => throw new Exception(response.Message)
            };


        }
    }
}
