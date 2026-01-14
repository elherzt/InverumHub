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
    public interface IUserService
    {
        Task<List<UserDTO>> GetAll(bool isActive);
        Task<UserDTO> GetById(Guid uid);
        Task<UserDTO> CreateUser(CreateUserDTO model);
        Task<UserDTO> UpdateUser(UpdateUserDTO model);
        Task<UserDTO> ChangePassword(Guid userUid, ChangePasswordDTO model);
        Task<UserDTO> AddRoleApplication(Guid userUid, int roleId, int appId);
        Task RemoveRoleApplication(Guid userUid, int roleId, int appId);
        Task Disable(Guid userUid);
        Task<UserDTO> Enable(Guid userUid);
        Task<UserDTO> InitializeAdminUser(CreateUserDTO model);
    }

    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private readonly IMapper _mapper;



        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO> CreateUser(CreateUserDTO model)
        {
            CustomResponse response = await _userRepository.Create(model);
            return response.TypeOfResponse switch
            {
                TypeOfResponse.OK => _mapper.Map<UserDTO>((User)response.Data),
                TypeOfResponse.FailedResponse => throw new BusinessException(response.Message),
                TypeOfResponse.NotFound => throw new NotFoundException(response.Message),
                _ => throw new Exception(response.Message)
            };
        }

        public async Task<UserDTO> UpdateUser(UpdateUserDTO model)
        {
            CustomResponse response = await _userRepository.Update(model);
            return response.TypeOfResponse switch
            {
                TypeOfResponse.OK => _mapper.Map<UserDTO>((User)response.Data),
                TypeOfResponse.FailedResponse => throw new BusinessException(response.Message),
                TypeOfResponse.NotFound => throw new NotFoundException(response.Message),
                _ => throw new Exception(response.Message)
            };
        }

        public async Task<UserDTO> InitializeAdminUser(CreateUserDTO model)
        {
            
            UserDTO new_user = new UserDTO();
           
            CustomResponse exist_admin_response = await _userRepository.GetByRole((int)ConstantsEnums.SSOT_ADMIN_ROLE_ID);

            if (exist_admin_response.TypeOfResponse == TypeOfResponse.NotFound)
            {
                new_user = await CreateUser(model);
                await _userRepository.AddRoleApplication(new_user.Uid, (int)ConstantsEnums.SSOT_ADMIN_ROLE_ID, (int)ConstantsEnums.SSOT_APPLICATION_ID);
                new_user = await GetById(new_user.Uid);
            }
            else
            {
                throw new BusinessException("Admin user already exists. Use Users endpoint");
            }
       
            
            return new_user;
        }

        public async Task<UserDTO> GetById(Guid uid)
        {
            CustomResponse response = await _userRepository.GetById(uid);
            return response.TypeOfResponse switch
            {
                TypeOfResponse.OK => _mapper.Map<UserDTO>((User)response.Data),
                TypeOfResponse.FailedResponse => throw new BusinessException(response.Message),
                TypeOfResponse.NotFound => throw new NotFoundException(response.Message),
                _ => throw new Exception(response.Message)
            };
        }

        public async Task<List<UserDTO>> GetAll(bool isActive)
        {
            CustomResponse response = await _userRepository.Get(isActive);
            return response.TypeOfResponse switch
            {
                TypeOfResponse.OK => _mapper.Map<List<UserDTO>>((List<User>)response.Data),
                TypeOfResponse.FailedResponse => throw new BusinessException(response.Message),
                TypeOfResponse.NotFound => throw new NotFoundException(response.Message),
                _ => throw new Exception(response.Message)
            };
        }

        public async Task<UserDTO> AddRoleApplication(Guid userUid, int roleId, int appId)
        {
            CustomResponse response = await _userRepository.AddRoleApplication( userUid,  roleId,  appId);
            if (response.TypeOfResponse == TypeOfResponse.OK)
            {
                return await GetById(userUid);
            }
            else 
            {
                throw new BusinessException(response.Message);
            }
           
        }

        public async Task RemoveRoleApplication(Guid userUid, int roleId, int appId)
        {
            CustomResponse response = await _userRepository.DeleteRoleApplication(userUid, roleId, appId);
            if (response.TypeOfResponse != TypeOfResponse.OK)
            {
                throw new BusinessException(response.Message);
            }
        }

        public async Task<UserDTO> ChangePassword(Guid userUid, ChangePasswordDTO model)
        {
            CustomResponse response = await _userRepository.ChangePassword(userUid, model);
            return response.TypeOfResponse switch
            {
                TypeOfResponse.OK => _mapper.Map<UserDTO>((User)response.Data),
                TypeOfResponse.FailedResponse => throw new BusinessException(response.Message),
                TypeOfResponse.NotFound => throw new NotFoundException(response.Message),
                _ => throw new Exception(response.Message)
            };
        }

        public async Task Disable(Guid userUid)
        {
            CustomResponse response = await _userRepository.Disable(userUid);
            if (response.TypeOfResponse != TypeOfResponse.OK)
            {
                throw new BusinessException(response.Message);
            }
        }

        public async Task<UserDTO> Enable(Guid userUid)
        {
            CustomResponse response = await _userRepository.Enable(userUid);
            return response.TypeOfResponse switch
            {
                TypeOfResponse.OK => _mapper.Map<UserDTO>((User)response.Data),
                TypeOfResponse.FailedResponse => throw new BusinessException(response.Message),
                TypeOfResponse.NotFound => throw new NotFoundException(response.Message),
                _ => throw new Exception(response.Message)
            };
        }
    }
}
