using AutoMapper;
using JwtAuthServer.Core.Dtos;
using JwtAuthServer.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwtAuthServer.Service
{
    public class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<UserAppDto, UserApp>().ReverseMap();
        }
    }
}
