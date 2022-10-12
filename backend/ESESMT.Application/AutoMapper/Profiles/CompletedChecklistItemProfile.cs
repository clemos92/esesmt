using AutoMapper;
using ESESMT.Domain.Entities;
using ESESMT.Domain.Models;

namespace ESESMT.Application.AutoMapper.Profiles
{
    public class CompletedChecklistItemProfile : Profile
    {
        public CompletedChecklistItemProfile()
        {
            CreateMap<CompletedChecklistItem, CompletedChecklistItemDto>();
            //evita criar um novo registro em CompletedChecklist e ChecklistItem
            CreateMap<CompletedChecklistItemDto, CompletedChecklistItem>()
                .ForMember(p => p.CompletedChecklist, opt => opt.Ignore())
                .ForMember(p => p.ChecklistItem, opt => opt.Ignore())
                .ForMember(p => p.Valid, opt => opt.Ignore())
                .ForMember(p => p.ValidationResult, opt => opt.Ignore());
        }
    }
}
