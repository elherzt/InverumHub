using InverumHub.Core.Common;
using InverumHub.Core.Repositories;
using InverumHub.DataLayer.Context;
using InverumHub.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InverumHub.Core.DTOs;

namespace InverumHub.DataLayer.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly InverumDbContext _context;
        private readonly IPasswordHasherService _passwordHasherService;

        public AuthRepository(InverumDbContext context, IPasswordHasherService passwordHasherService)
        {
            _context = context;
            _passwordHasherService = passwordHasherService;
        }

        

        public async Task<CustomResponse> Login(string username, string password, string application_name)
        {
            CustomResponse response = new CustomResponse(TypeOfResponse.OK, "Login successful");
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == username && u.IsActive == true);

                if (user == null)
                {
                    response.Message = "User not found, inactive or credentials are invalid."; //to do implement message from resources
                    response.TypeOfResponse = TypeOfResponse.NotFound;
                    return response;
                }

                if (!_passwordHasherService.VerifyPassword(user.Password, password))
                {
                    response.TypeOfResponse = TypeOfResponse.FailedResponse;
                    response.Message = "User not found, inactive or credentials are invalid."; //to do implement message from resources
                    return response;
                }

                
                var userAppRoles = await _context.UserApplicationRoles
                    .Include(uar => uar.Application)
                    .Include(uar => uar.Role)
                    .Include(uar => uar.User)
                    .Where(uar => uar.UserUid == user.Uid && uar.Application.Alias == application_name)
                    .ToListAsync();


                if (userAppRoles == null || userAppRoles.Count == 0)
                {
                    response.TypeOfResponse = TypeOfResponse.FailedResponse;
                    response.Message = "User does not have access to the specified application."; //to do implement message from resources
                    return response;
                }

                response.Data = userAppRoles;


            }
            catch (Exception ex)
            {
                response = new CustomResponse(TypeOfResponse.Exception, $"An error occurred during login: {ex.Message}");
            }
            return response;
        }
    }
}
