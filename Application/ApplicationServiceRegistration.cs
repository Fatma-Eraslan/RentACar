using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ApplicationServiceRegistration//program.cs de yapmak yerine bu şekilde clean kod
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());//git assemblydeki mevcut çalışan her şeyin doğru seçim olduğunu
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());//mediatr git bütün assemblyleri tara oarada commendleri bul onların handlerlarını bul, birbiriyle eşleştir listele koy, send yaparsam onun handlerını çalıştır.
            });
            return services;    
        }
    }
}
