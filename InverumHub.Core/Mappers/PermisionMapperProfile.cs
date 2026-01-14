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
    public class PermisionMapperProfile : Profile
    {
        public PermisionMapperProfile()
        {
            CreateMap<Permission, PermissionDTO>();
        }
    }
}
