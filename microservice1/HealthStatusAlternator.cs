using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace microservice1
{
    internal class HealthStatusAlternator : IHealthCheck
    {
        private IOptions<Service> _option;

        public HealthStatusAlternator(IOptions<Service> option)
        {
            _option = option;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_option.Value.IsHealthy ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy());
        }
    }
}