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
    public interface IAuthService
    {
        Task<GlobalSessionModel> login(LoginDTO model);

    }

    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        public async Task<GlobalSessionModel> login(LoginDTO model)
        {
            var response = await _authRepository.Login(model.Username, model.Password, model.ApplicationName);
            if (response.TypeOfResponse != TypeOfResponse.OK)
            {
                throw new BusinessException(response.Message);
            }
            GlobalSessionModel sessionModel = new GlobalSessionModel();
            List<UserApplicationRole> uar = response.Data as List<UserApplicationRole>;
            var application = uar.First().Application;
            var user = uar.First().User;


            sessionModel.ApplicationId = application.Id;
            sessionModel.ApplicationName = application.Alias;
            sessionModel.UserId = user.Uid;
            sessionModel.UserName = user.FullName;
            sessionModel.UserEmail = user.Email;
            sessionModel.RoleNames = uar.Select(x => x.Role.Name).ToList();

            return sessionModel;
        }
    }

}
