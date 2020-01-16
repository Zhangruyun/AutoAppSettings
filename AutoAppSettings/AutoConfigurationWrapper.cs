using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AutoAppSettings
{
    public abstract class AutoConfigurationWrapper
    {
        public abstract void Register(IServiceCollection services, string appSettingKey);
    }
    public class AutoConfigurationWrapperImpl<TConfiguration> : AutoConfigurationWrapper where TConfiguration : class, IAutoAppSetting
    {
        public override void Register(IServiceCollection services, string appSettingKey)
        { 
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            services.Configure<TConfiguration>(configuration.GetSection(appSettingKey));
        }
    }
}
