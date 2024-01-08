using mvc_query_hub.Data;

namespace mvc_query_hub.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services)
        {
            services.AddDbContext<HubContext>();
        }
    }
}
