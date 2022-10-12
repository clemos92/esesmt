using AutoMapper;
using ESESMT.Application.AutoMapper.Profiles;

namespace ESESMT.Application.AutoMapper
{
    public class AutoMapperConfiguration : Profile
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BaseProfile());
                cfg.AddProfile(new ChecklistTypeProfile());
                cfg.AddProfile(new ChecklistProfile());
                cfg.AddProfile(new ChecklistItemProfile());
            });
        }
    }
}