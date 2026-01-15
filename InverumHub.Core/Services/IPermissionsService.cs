using AutoMapper;
using InverumHub.Core.Common;
using InverumHub.Core.DTOs;
using InverumHub.Core.Entities;
using InverumHub.Core.Enums;
using InverumHub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverumHub.Core.Services
{
    public interface IPermissionsService
    {
        Task<List<PermissionDTO>> ChekPermissions(GlobalSessionModel sessionModel, string roleName, string applicationName);
        Task<List<PermissionSysDTO>> GetAll();
        Task<PermissionSysDTO?> GetById(int id);
        Task<PermissionSysDTO> Create(CreatePermissionDTO model);
        Task<PermissionSysDTO> Update(UpdatePermissionDTO model);

        Task Delete(int id);
    }

    public class PermissionsService : IPermissionsService
    {
        private readonly IPermissionsRepository _permissionsRepository;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Permission> _permissionRepository;

        public PermissionsService(IPermissionsRepository permissionsRepository, IMapper mapper, IGenericRepository<Permission> permissionRepository)
        {
            _permissionsRepository = permissionsRepository;
            _mapper = mapper;
            _permissionRepository = permissionRepository;
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

        public async Task<PermissionSysDTO> Create(CreatePermissionDTO model)
        {
            var new_permission = _mapper.Map<Permission>(model);
            await _permissionRepository.Add(new_permission);
            return _mapper.Map<PermissionSysDTO>(new_permission);
        }

        public async Task Delete(int id)
        {
            var permission = await _permissionRepository.GetById(id);
            if (permission == null)
            {
                throw new BusinessException("Permission not found");
            }
            await _permissionRepository.Delete(permission);

        }

        public async Task<List<PermissionSysDTO>> GetAll()
        {
            var permissions = await _permissionRepository.GetAll();
            return _mapper.Map<List<PermissionSysDTO>>(permissions);
        }

        public async Task<PermissionSysDTO?> GetById(int id)
        {
            var permission = await _permissionRepository.GetById(id);
            if (permission == null)
            {
                throw new BusinessException("Permission not found");
            }
            return _mapper.Map<PermissionSysDTO>(permission);

        }

        public async Task<PermissionSysDTO> Update(UpdatePermissionDTO model)
        {
            var permission = await _permissionRepository.GetById(model.Id);
            if (permission == null)
            {
                throw new BusinessException("Permission not found");
            }
            _mapper.Map(model, permission);
            await _permissionRepository.Update(permission);
            return _mapper.Map<PermissionSysDTO>(permission);

        }
    }
}
