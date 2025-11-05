using Mapster;
using MapsterMapper;

namespace histopat_back.Mapster
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMapster(this IServiceCollection services)
        {
            TypeAdapterConfig.GlobalSettings.Scan(typeof(ServiceCollectionExtensions)
            .Assembly);
            services.AddSingleton(new Mapper(TypeAdapterConfig.GlobalSettings));
            services.AddScoped<IMapper, ServiceMapper>();
            return services;
        }

    }
}
