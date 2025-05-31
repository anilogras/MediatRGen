using Core.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Deneme
{
    public static class ServiceRegistiration
    {
        public static void AddAppilicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddDbContext<DbContext , TempDbContext>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
