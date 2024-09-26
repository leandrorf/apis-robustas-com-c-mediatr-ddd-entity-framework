using Microsoft.Extensions.DependencyInjection;

namespace VemDeZap.Domain
{
    public static class ConfigureServices
    {
        public static void AddInjectionApplication( this IServiceCollection services )
        {
            services.AddMediatR( cfg =>
            {
                cfg.RegisterServicesFromAssemblies( typeof( ConfigureServices ).Assembly );
            } );
        }
    }
}
