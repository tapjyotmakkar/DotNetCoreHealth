using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ServerStatusService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServerStatusController : ControllerBase
    {
        private readonly ILogger<ServerStatusController> _logger;
        private readonly ServerStatusDbContext _serverStatusDbContext;
        private readonly IHttpClientFactory _httpClientFactory;

        public ServerStatusController(ILogger<ServerStatusController> logger, 
            ServerStatusDbContext serverStatusDbContext,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _serverStatusDbContext = serverStatusDbContext;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<ServerStatuses> Get()
        {
            var serverStatuses = new ServerStatuses();
            var serverStatus = await _serverStatusDbContext.Statuses.Where(x => x.Date.Date == DateTime.Today.Date).ToListAsync();
            serverStatuses.MicroService1Status = serverStatus.Where(x => x.Name == "microservice1").OrderByDescending(x => x.Date).Take(5).ToList();
            serverStatuses.MicroService2Status = serverStatus.Where(x => x.Name == "microservice2").OrderByDescending(x => x.Date).Take(5).ToList();
            return serverStatuses;
        }
    }
}
