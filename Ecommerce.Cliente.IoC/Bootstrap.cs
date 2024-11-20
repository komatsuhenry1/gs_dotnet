using Ecommerce.Cliente.Application.Services;
using Ecommerce.Cliente.Data.AppData;
using Ecommerce.Cliente.Data.Repositories;
using Ecommerce.Cliente.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Cliente.IoC
{
    public class Bootstrap
    {
        public static void Start(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(x => {
                x.UseOracle(configuration["ConnectionStrings:Oracle"]);
            });

            services.AddTransient<IClienteRepository, ClienteRepository>();

            services.AddTransient<IClienteApplicationService, ClienteApplicationService>();
        }
    }
}
