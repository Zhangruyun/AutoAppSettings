using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace AutoAppSettings
{
    internal class Worker : IHostedService
    {
        private readonly IOptions<Test> options;
        private readonly IOptions<Test2> option2;

        public Worker(IOptions<Test> options,
            IOptions<Test2> option2)
        {
            this.options = options;
            this.option2 = option2;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            System.Console.WriteLine("a");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            System.Console.WriteLine("a");
            return Task.CompletedTask;
        }
    }
}