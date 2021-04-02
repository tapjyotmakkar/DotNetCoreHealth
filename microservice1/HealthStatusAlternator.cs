using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace microservice1
{
    internal class HealthStatusAlternator : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(GetRandomHealthSwitch ? HealthCheckResult.Healthy("microservice1 is healthy") : HealthCheckResult.Unhealthy("microservice1 is unhealthy"));
        }

        private bool GetRandomHealthSwitch
        {
            get
            {
                var rnd = new Random().Next(1, 5);
                return rnd % 2 != 0;
            }            
        }
    }
}