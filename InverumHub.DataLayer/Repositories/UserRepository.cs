using InverumHub.Core.Common;
using InverumHub.Core.DTOs;
using InverumHub.Core.Entities;
using InverumHub.Core.Enums;
using InverumHub.Core.Repositories;
using InverumHub.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<CustomResponse> AddRoleApplication(Guid user_id, int rol_id, int application_id)
        {
            CustomResponse response = new CustomResponse(TypeOfResponse.OK, "Role application added successfully");
            try
            {
                var userRoleApplication = new UserApplicationRole
                {
                    UserUid = user_id,
                    RoleId = rol_id,
                    ApplicationId = application_id
                };

                _context.UserApplicationRoles.Add(userRoleApplication);
                await _context.SaveChangesAsync();

                response.Data = userRoleApplication;
            }
            catch (Exception ex)
            {
                response.TypeOfResponse = TypeOfResponse.Exception;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<CustomResponse> DeleteRoleApplication(Guid user_id, int rol_id, int application_id)
        {
            CustomResponse response = new CustomResponse(TypeOfResponse.OK, "Role application deleted successfully");
            try
            {
                var userRoleApplication = await _context.UserApplicationRoles
                    .FirstOrDefaultAsync(ura => ura.UserUid == user_id && ura.RoleId == rol_id && ura.ApplicationId == application_id);
                if (userRoleApplication == null)
                {
                    response.TypeOfResponse = TypeOfResponse.NotFound;
                    response.Message = "Role application not found";
                    return response;
                }
                _context.UserApplicationRoles.Remove(userRoleApplication);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                response.TypeOfResponse = TypeOfResponse.Exception;
                response.Message = ex.Message;
            }
            return response;
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

        public async Task<CustomResponse> Get(bool isActive)
        {
            CustomResponse response = new CustomResponse(TypeOfResponse.OK, "Users retrieved successfully");
            try
            {
                var user_list = await _context.Users
                    .Include(u => u.ApplicationRoles)
                        .ThenInclude(ur => ur.Role)
                    .Include(u => u.ApplicationRoles)
                        .ThenInclude(ur => ur.Application)
                    .Where(u => u.IsActive == isActive)
                    .ToListAsync();
                response.Data = user_list;
            }
            catch (Exception ex)
            {
                response.TypeOfResponse = TypeOfResponse.Exception;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<CustomResponse> GetById(Guid uid)
        {
            CustomResponse response = new CustomResponse(TypeOfResponse.OK, "User retrieved successfully");
            try
            {
                var user = await _context.Users
                    .Include(u => u.ApplicationRoles)
                        .ThenInclude(ur => ur.Role)
                    .Include(u => u.ApplicationRoles)
                        .ThenInclude(ur => ur.Application)
                    .Where(u => u.Uid == uid && u.IsActive == true)
                    .FirstOrDefaultAsync();
                if (user == null)
                {
                    response.TypeOfResponse = TypeOfResponse.NotFound;
                    response.Message = "User not found";
                }
                response.Data = user;
            }
            catch (Exception ex)
            {
                response.TypeOfResponse = TypeOfResponse.Exception;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<CustomResponse> GetByRole(int rol_id)
        {
            CustomResponse response = new CustomResponse(TypeOfResponse.OK, "Users retrieved successfully");
            try
            {
                
                var user_list = await _context.Users
                    .Include(u => u.ApplicationRoles)
                    .Where(u => u.ApplicationRoles.Any(ur => ur.RoleId == rol_id && u.IsActive == true))
                    .ToListAsync();

                if (user_list == null || user_list.Count == 0)
                {
                    response.TypeOfResponse = TypeOfResponse.NotFound;
                    response.Message = "No users found for the specified role";
                }

                response.Data = user_list;
            }
            catch (Exception ex)
            {
                response.TypeOfResponse = TypeOfResponse.Exception;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<CustomResponse> Update(UpdateUserDTO user)
        {
            CustomResponse response = new CustomResponse(TypeOfResponse.OK, "User updated successfully");
            try
            {
                var existingUserResponse = await GetById(user.Uid);

                if (existingUserResponse.TypeOfResponse != TypeOfResponse.OK)
                {
                    return existingUserResponse;
                }

                User existingUser = (User)existingUserResponse.Data!;
                existingUser.FullName = user.FullName;
                existingUser.Email = user.Email;
                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();
                response.Data = existingUser;
            }
            catch (Exception ex)
            {
                response.TypeOfResponse = TypeOfResponse.Exception;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<CustomResponse> ChangePassword(Guid userUid, ChangePasswordDTO user)
        {
            CustomResponse response = new CustomResponse(TypeOfResponse.OK, "Password changed successfully");
            try
            {
                var existingUserResponse = await GetById(userUid);

                if (existingUserResponse.TypeOfResponse != TypeOfResponse.OK)
                {
                    return existingUserResponse;
                }
                User existingUser = (User)existingUserResponse.Data!;
                existingUser.Password = _passwordHasherService.HashPassword(user.NewPassword);
                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();
                response.Data = existingUser;
            }
            catch (Exception ex)
            {
                response.TypeOfResponse = TypeOfResponse.Exception;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<CustomResponse> Disable(Guid userUid)
        {
            CustomResponse response = new CustomResponse(TypeOfResponse.OK, "User disabled successfully");
            try
            {

                var existingUserResponse = await GetById(userUid);

                if (existingUserResponse.TypeOfResponse != TypeOfResponse.OK)
                {
                    return existingUserResponse;
                }

                User existingUser = (User)existingUserResponse.Data!;

                existingUser.IsActive = false;
                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.TypeOfResponse = TypeOfResponse.Exception;
                response.Message = ex.Message;
            }
            return response;

        }

        public async Task<CustomResponse> Enable(Guid userUid)
        {
            CustomResponse response = new CustomResponse(TypeOfResponse.OK, "User enabled successfully");
            try
            {
                var existingUser = await _context.Users
                    .Include(u => u.ApplicationRoles)
                        .ThenInclude(ur => ur.Role)
                    .Include(u => u.ApplicationRoles)
                        .ThenInclude(ur => ur.Application)
                    .Where(u => u.Uid == userUid && u.IsActive == false)
                    .FirstOrDefaultAsync();
                if (existingUser == null)
                {
                    response.TypeOfResponse = TypeOfResponse.NotFound;
                    response.Message = "User not found";
                    return response;
                }
                existingUser.IsActive = true;
                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();
                response.Data = existingUser;


            }
            catch (Exception ex)
            {
                response.TypeOfResponse = TypeOfResponse.Exception;
                response.Message = ex.Message;
            }
            return response;

        }
    }
}
