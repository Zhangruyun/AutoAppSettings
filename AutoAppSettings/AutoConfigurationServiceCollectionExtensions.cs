using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace AutoAppSettings
{
    public static class AutoConfigurationServiceCollectionExtensions
    {
        public static IServiceCollection AddAutoConfiguration(this IServiceCollection services, params Assembly[] assemblies)
        {
            var types = ServiceRegistrar.GetClasses(assemblies);
            foreach (var item in types)
            {
                var attribute = item.GetCustomAttribute<AutoConfig>();
                var appSettingKey = (attribute == null)
                    ? item.Name
                    : attribute.Key;

                 
                var type = typeof(AutoConfigurationWrapperImpl<>).MakeGenericType(item);
                var wrapper = (AutoConfigurationWrapper)Activator.CreateInstance(type);
                wrapper.Register(services, appSettingKey);
            }
            //var type = typeof(AutoConfigurationWrapperImpl<>).MakeGenericType(typeof(Test));
            //var wrapper = (AutoConfigurationWrapper)Activator.CreateInstance(type);


            //wrapper.Register(services);



            return services;
        }
    }

}
