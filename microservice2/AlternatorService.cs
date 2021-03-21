using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace microservice2
{
    public class Service
    {
        public bool IsHealthy;
    }

    internal class HealthAlternator : BackgroundService
    {
        private IOptions<Service> _option;
        public HealthAlternator(IOptions<Service> option)
        {
            _option = option;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var _timer = new Timer(TimerCallback, null, 0, 10000);
            await Task.CompletedTask;
        }

        private void TimerCallback(object sender)
        {
            _option.Value.IsHealthy = !_option.Value.IsHealthy;
        }
    }
}