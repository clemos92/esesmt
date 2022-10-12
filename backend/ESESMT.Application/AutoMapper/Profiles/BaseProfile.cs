using AutoMapper;
using ESESMT.Domain.Entities;
using ESESMT.Domain.Models;

namespace ESESMT.Application.AutoMapper.Profiles
{
    public class BaseProfile : Profile
    { 
        public BaseProfile()
        {
            CreateMap<BaseEntity<int>, BaseDto<int>>()
                .IncludeAllDerived()
                .ReverseMap();
        }
    }
}
