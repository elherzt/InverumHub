using AutoMapper;
using InverumHub.Core.DTOs;
using InverumHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverumHub.Core.Mappers
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDTO>();
            CreateMap<CreateAndUpdateRoleDTO, Role>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
