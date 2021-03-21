using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ServerStatusService
{
    public class StoreServerStatusInDatabaseService : BackgroundService
    {
        public IServiceProvider _services { get; }
        private readonly IHttpClientFactory _httpClientFactory;
        private Timer _timer;

        public StoreServerStatusInDatabaseService(
            IServiceProvider services,
            IHttpClientFactory httpClientFactory)
        {
            _services = services;
            _httpClientFactory = httpClientFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(TimerCallback, null, TimeSpan.FromMilliseconds(0), TimeSpan.FromSeconds(10));
            await Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _timer.Change(Timeout.Infinite, 0);
            return base.StopAsync(cancellationToken);
        }

        private async void TimerCallback(object sender)
        {
            var microservice1Status = new ServerStatus { Date = DateTime.Now, Name = "microservice1" };
            var client = _httpClientFactory.CreateClient("microservice1");
            var request = new HttpRequestMessage(HttpMethod.Get, string.Empty);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                microservice1Status.Status = result;
            }
            else
            {
                microservice1Status.Status = "Unhealthy";
            }

            var microservice2Status = new ServerStatus { Date = DateTime.Now, Name = "microservice2" };
            var microservice2Client = _httpClientFactory.CreateClient("microservice2");
            var microservice2Request = new HttpRequestMessage(HttpMethod.Get, string.Empty);
            var microservice2Response = await microservice2Client.SendAsync(microservice2Request);
            if (microservice2Response.IsSuccessStatusCode)
            {
                var microservice2Result = await microservice2Response.Content.ReadAsStringAsync();
                microservice2Status.Status = microservice2Result;
            }
            else
            {
                microservice2Status.Status = "Unhealthy";
            }

            using (var scope = _services.CreateScope())
            {
                var serverStatusDbContext = scope.ServiceProvider.GetRequiredService<ServerStatusDbContext>();
                serverStatusDbContext.Statuses.Add(microservice1Status);
                serverStatusDbContext.Statuses.Add(microservice2Status);
                await serverStatusDbContext.SaveChangesAsync();
            }
            
        }
    }
}