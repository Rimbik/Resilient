using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PrjPolly.Service
{
    public class GitHubService : IGitHubService
    {
        private readonly HttpClient _httpClient; 
        private readonly string _remoteServiceBaseUrl; 
        public GitHubService(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("http://localhost:50142/");
            httpClient.DefaultRequestHeaders.Add("Accept","application/vnd.github.v3+json");
            httpClient.DefaultRequestHeaders.Add("User-Agent","HttpClientFactory-Sample");

            //set timeout
            httpClient.Timeout = TimeSpan.FromMinutes(1);

            _httpClient = httpClient;
        }
        public async Task<IEnumerable<dynamic>> GetAspNetDocsIssues(Guid requestId)
        {
            //https://api.github.com//repos/aspnet/AspNetCore.Docs/issues?state=open&sort=created&direction=desc

            var response = await _httpClient.GetAsync(
             "/weatherforecast?requestId="+ requestId);

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
           
            return await JsonSerializer.DeserializeAsync<IEnumerable<dynamic>>(responseStream);
        }
    }

}
