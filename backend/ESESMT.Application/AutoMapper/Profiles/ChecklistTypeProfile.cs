using AutoMapper;
using ESESMT.Domain.Entities;
using ESESMT.Domain.Models;

namespace ESESMT.Application.AutoMapper.Profiles
{
    public class ChecklistTypeProfile : Profile
    {
        public ChecklistTypeProfile()
        {
            CreateMap<ChecklistType, DropdownDefaultModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(z => z.Id))
                .ForMember(x => x.Value, opt => opt.MapFrom(z => z.Name));

            CreateMap<ChecklistType, ChecklistTypeDto>()
                .ReverseMap();
        }
    }
}
