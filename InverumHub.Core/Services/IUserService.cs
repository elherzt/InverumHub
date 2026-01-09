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
        Task<UserDTO> CreateUser(CreateUserDTO model);
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
    }
}
