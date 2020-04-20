using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Caist.Framework.WebService.Services
{
    public class PLCTaskService : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Work(cancellationToken);

                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);
                }
                catch (TaskCanceledException)
                {
                }

            }
        }

        void Work(CancellationToken cancellationToken)
        {
            Console.WriteLine($"循环任务{DateTime.Now}");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("循环任务结束");
            return Task.CompletedTask;
        }
    }
}
