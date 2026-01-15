using AutoMapper;
using InverumHub.Core.Common;
using InverumHub.Core.DTOs;
using InverumHub.Core.Entities;
using InverumHub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverumHub.Core.Services
{
    public interface IRoleService
    {
        Task<List<RoleDTO>> GetAll();
        Task<RoleDTO?> GetById(int id);
        Task<RoleDTO> Create(CreateAndUpdateRoleDTO model);
        Task<RoleDTO> Update(CreateAndUpdateRoleDTO model);
        Task Delete(int id);
    }

    public class RoleService : IRoleService
    {
        private readonly IGenericRepository<Role> _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IGenericRepository<Role> roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<RoleDTO> Create(CreateAndUpdateRoleDTO model)
        {
            var new_rol = _mapper.Map<Role>(model);
            new_rol.Id = model.Id;

            await _roleRepository.Add(new_rol);
            return _mapper.Map<RoleDTO>(new_rol);


        }
        public async Task Delete(int id)
        {
            var role = await _roleRepository.GetById(id);
            if (role == null)
            {
                throw new BusinessException("Role not found");
            }

            await _roleRepository.Delete(role);

        }
        public async Task<List<RoleDTO>> GetAll()
        {
            var roles = await _roleRepository.GetAll();
            return _mapper.Map<List<RoleDTO>>(roles);
        }
        public async Task<RoleDTO?> GetById(int id)
        {
            var role = await _roleRepository.GetById(id);
            return _mapper.Map<RoleDTO>(role);
        }
        public async Task<RoleDTO> Update(CreateAndUpdateRoleDTO model)
        {
            var role = await _roleRepository.GetById(model.Id);
            if (role == null)
            {
                throw new BusinessException("Role not found");
            }
            _mapper.Map(model, role);
            await _roleRepository.Update(role);
            return _mapper.Map<RoleDTO>(role);
        }
    }
}
