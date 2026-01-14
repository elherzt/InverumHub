using InverumHub.Core.Common;
using InverumHub.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverumHub.Core.Repositories
{
    public interface IAuthRepository
    {
        Task<CustomResponse> Login(string username, string password, string application_name);
    }
}
