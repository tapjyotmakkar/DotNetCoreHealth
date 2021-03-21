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
        public async Task<IEnumerable<ServerStatus>> Get()
        {
            var serverStatus = await _serverStatusDbContext.Statuses.Where(x => x.Date.Date == DateTime.Today.Date).ToListAsync();
            serverStatus = serverStatus.OrderByDescending(x => x.Date).Take(5).ToList();
            return serverStatus;
        }
    }
}
