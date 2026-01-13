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
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<UserApplicationRole, UserApplicationRoleDTO>()
                .ForMember(d => d.ApplicationName,
                    o => o.MapFrom(s => s.Application.Name))
                .ForMember(d => d.RoleName,
                    o => o.MapFrom(s => s.Role.Name));

            CreateMap<User, UserDTO>()
                .ForMember(d => d.ApplicationsRoles,
                    o => o.MapFrom(s => s.ApplicationRoles));
        }
    }
}
