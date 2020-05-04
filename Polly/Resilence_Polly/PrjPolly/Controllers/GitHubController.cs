using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polly.CircuitBreaker;
using PrjPolly.Service;

namespace PrjPolly.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GitHubController : ControllerBase
    {
        private readonly ILogger<GitHubController> _logger;
        private readonly IGitHubService _gitHubSvc;

        public GitHubController(ILogger<GitHubController> logger, IGitHubService service)
        {
            _logger = logger;
            _gitHubSvc = service;
        }

        [HttpGet]
        public async Task<IEnumerable<dynamic>> GetAsync()
        {
            dynamic response = null;

            try
            {
                var reqId = Guid.NewGuid();
                response = await _gitHubSvc.GetAspNetDocsIssues(reqId);
           
            }
            catch (BrokenCircuitException cbe) 
            {
                // Circuit Borker Exception
                throw new Exception("Circuit Breaker Invoked- Exception: ", cbe);
            }
            catch (HttpRequestException ex)
            {
                // Other Exceptions
                throw new Exception("Generic HTTP Exception: ", ex);
            }

            return response;
        }
    }
}
