using Core.Application.Pipelines.Transaction;
using Core.Application.Pipelines.Validation;
using Core.Application.Rules;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Application;

public static class ApplicationServiceRegistration//program.cs de yapmak yerine bu şekilde clean kod
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());//git assemblydeki mevcut çalışan her şeyin doğru seçim olduğunu

        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(),typeof(BaseBusinessRules)); //kullanımı

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());//mediatr git bütün assemblyleri tara oarada commendleri bul onların handlerlarını bul, birbiriyle eşleştir listele koy, send yaparsam onun handlerını çalıştır.

            configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));

            configuration.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));
        });
        return services;    
    }

    public static IServiceCollection AddSubClassesOfType(
       this IServiceCollection services,
       Assembly assembly,
       Type type,
       Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null
   )
    {//assembly içerisinde SubClass olarak verdiğimiz yani basebusisnessrule olanları bul, onları LifeCycle a ekle(ioc)
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (var item in types)
            if (addWithLifeCycle == null)
                services.AddScoped(item);

            else
                addWithLifeCycle(services, type);
        return services;
    }

}
