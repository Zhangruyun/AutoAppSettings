using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Reflection;

namespace AutoAppSettings
{
    class Program
    {
        public static void Main(string[] args) =>
            CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", true, true)
                    .AddEnvironmentVariables();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    Assembly[] assemblies = new Assembly[] { typeof(Program).Assembly };
                    ScanAssembly.ConnectImplementationsToTypesClosing(typeof(IAutoAppSetting), assemblies);
                    services.AddAutoConfiguration(typeof(Program).Assembly);


                    services.AddLogging();

                    services.AddHostedService<Worker>();
                })
                .UseConsoleLifetime();
        }
    }
    public class AutoConfig : Attribute
    {
        public string Key { get; set; }
    }
    [AutoConfig(Key = "Authorization")]
    public class Test : IAutoAppSetting
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpirationMinutes { get; set; }
    }
    [AutoConfig(Key = "Test2")]
    public class Test2 : IAutoAppSetting
    {
        public int ExpirationMinutes { get; set; }
    }
    public interface IAutoAppSetting
    {

    }
}
