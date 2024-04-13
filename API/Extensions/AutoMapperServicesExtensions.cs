using API.Helpers;
using AutoMapper;

namespace API.Extensions;

public static class AutoMapperServicesExtensions
{
    public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfiles()); });
        services.AddSingleton(config.CreateMapper());

        return services;
    }
}