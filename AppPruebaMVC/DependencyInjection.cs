using AppPruebaMVC.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AppPruebaMVC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<consultoriobdContext>(opt =>
            {

                opt.UseSqlServer(configuration.GetConnectionString("DBConexion"));

            });

            return services;
        }
    }
}
