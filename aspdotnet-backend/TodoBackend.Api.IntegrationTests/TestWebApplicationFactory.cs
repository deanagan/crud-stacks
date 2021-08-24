
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using TodoBackend.Api.Interfaces;

namespace TodoBackend.Api.IntegrationTests
{

    public class TestWebApplicationFactory<TStartup> : WebApplicationFactory<Startup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .UseEnvironment("Integration")
                .UseStartup<TestStartup>();
        }
    }
}