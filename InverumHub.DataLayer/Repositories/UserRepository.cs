using InverumHub.Core.Common;
using InverumHub.Core.DTOs;
using InverumHub.Core.Enums;
using InverumHub.Core.Repositories;
using InverumHub.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InverumHub.Core.Entities;

namespace InverumHub.DataLayer.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly InverumDbContext _context;
        private readonly IPasswordHasherService _passwordHasherService;

        public UserRepository(InverumDbContext context, IPasswordHasherService hasher)
        {
            _context = context;
            _passwordHasherService = hasher;
        }

        public async Task<CustomResponse> Create(CreateUserDTO user)
        {
            CustomResponse response = new CustomResponse(TypeOfResponse.OK, "User created successfully");
            try
            {
                if (await ExistUserByEmail(user.Email))
                {
                    response.TypeOfResponse = TypeOfResponse.FailedResponse;
                    response.Message = "Email already registered";
                    return response;
                }

                var newUser = new User
                {
                    Uid = Guid.NewGuid(),
                    FullName = user.FullName,
                    Email = user.Email,
                    Password = _passwordHasherService.HashPassword(user.Password), 
                    IsActive = true
                };
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();
                response.Data = newUser;
            }
            catch (Exception ex)
            {
                response.TypeOfResponse = TypeOfResponse.Exception;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<bool> ExistUserByEmail(string email)
        {
            var count_email = await _context.Users.Where(u => u.Email == email && u.IsActive == true).CountAsync();
            return count_email > 0;
        }

        public Task<CustomResponse> Get()
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponse> GetById(Guid uid)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponse> Update(UpdateUserDTO user)
        {
            throw new NotImplementedException();
        }
    }
}
