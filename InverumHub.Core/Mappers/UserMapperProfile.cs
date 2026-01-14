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
            CreateMap<User, UserDTO>()
             .ForMember(
                 d => d.ApplicationsRoles,
                 o => o.MapFrom(s =>
                     s.ApplicationRoles
                      .GroupBy(ar => ar.Application)
                      .Select(g => new UserApplicationRoleDTO
                      {
                          ApplicationId = g.Key.Id,
                          ApplicationName = g.Key.Name,
                          Roles = g
                              .Select(ar => ar.Role)
                              .Distinct()
                              .Select(r => new RoleDTO
                              {
                                  Id = r.Id,
                                  Name = r.Name
                              })
                              .ToList()
                      })
                 )
             );
        }
    }
}
