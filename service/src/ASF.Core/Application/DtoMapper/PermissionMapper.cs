﻿using ASF.Application.DTO;
using ASF.Domain.Entities;
using AutoMapper;

namespace ASF.Application.DtoMapper
{
    public class PermissionMapper : Profile
    {
        public PermissionMapper()
        {
            base.CreateMap<Permission, PermissionActionInfoDetailsResponseDto>();
            base.CreateMap<Permission, PermissionMenuInfoDetailsResponseDto>();
            base.CreateMap<Permission, PermissionMenuInfoBaseResponseDto>();
            base.CreateMap<Permission, PermissionOpenApiInfoDetailsResponseDto>();

        }
    }
}
