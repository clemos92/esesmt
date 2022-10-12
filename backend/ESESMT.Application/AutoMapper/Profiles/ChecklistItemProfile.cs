using AutoMapper;
using ESESMT.Domain.Entities;
using ESESMT.Domain.Models;

namespace ESESMT.Application.AutoMapper.Profiles
{
    public class ChecklistItemProfile : Profile
    {
        public ChecklistItemProfile()
        {
            CreateMap<ChecklistItem, ChecklistItemDto>();
            //evita criar um novo registro em Checklist
            CreateMap<ChecklistItemDto, ChecklistItem>()
                .ForMember(p => p.Checklist, opt => opt.Ignore())
                .ForMember(p => p.Valid, opt => opt.Ignore())
                .ForMember(p => p.ValidationResult, opt => opt.Ignore());
        }
    }
}
