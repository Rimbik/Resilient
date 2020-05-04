using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrjPolly.Service
{
    public interface IGitHubService
    {
        public Task<IEnumerable<dynamic>> GetAspNetDocsIssues(Guid requestId);
    }
}
