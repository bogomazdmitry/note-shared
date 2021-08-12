using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using NoteShared.DTO.Mapping;

namespace NoteShared.Api.Configuration
{
    public static class AutoMapperExtenrions
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc => {
                mc.AddProfile(new UserProfile());
                mc.AddProfile(new NoteProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
