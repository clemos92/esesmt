using AutoMapper;
using ESESMT.Domain.Entities;
using ESESMT.Domain.Models;

namespace ESESMT.Application.AutoMapper.Profiles
{
    public class CompletedChecklistProfile : Profile
    {
        public CompletedChecklistProfile()
        {
            CreateMap<CompletedChecklist, CompletedChecklistListDto>().ReverseMap();
            CreateMap<CompletedChecklist, CompletedChecklistRegisterDto>();
            //evita criar um novo registro em CompletedChecklist
            CreateMap<CompletedChecklistRegisterDto, CompletedChecklist>()
                .ForMember(p => p.Checklist, opt => opt.Ignore())
                .ForMember(p => p.Valid, opt => opt.Ignore())
                .ForMember(p => p.ValidationResult, opt => opt.Ignore());
        }
    }
}
