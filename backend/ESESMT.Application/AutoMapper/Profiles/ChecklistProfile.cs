using AutoMapper;
using ESESMT.Domain.Entities;
using ESESMT.Domain.Models;

namespace ESESMT.Application.AutoMapper.Profiles
{
    public class ChecklistProfile : Profile
    {
        public ChecklistProfile()
        {
            CreateMap<Checklist, ChecklistListDto>().ReverseMap();
            CreateMap<Checklist, ChecklistRegisterDto>();
            //evita criar um novo registro em ChecklistType
            CreateMap<ChecklistRegisterDto, Checklist>()
                .ForMember(p=>p.ChecklistType, opt=>opt.Ignore())
                .ForMember(p=>p.Valid, opt=>opt.Ignore())
                .ForMember(p=>p.ValidationResult, opt=>opt.Ignore());
        }
    }
}
