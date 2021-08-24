using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Testing;

using Xunit;

using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using TodoBackend.Api.Interfaces;
using FakeItEasy;
using System;

namespace TodoBackend.Api.IntegrationTests
{
    public class HealthCheckTest : IClassFixture<TestWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;
        private TestWebApplicationFactory<Startup> _factory;

        private IUnitOfWork _fakeUnitOfWork;
        public HealthCheckTest(TestWebApplicationFactory<Startup> factory)
        {
            _fakeUnitOfWork = A.Fake<IUnitOfWork>(uow => uow.Implements<IUnitOfWork>());
            _factory = factory;
            _factory.ClientOptions.BaseAddress = new Uri("http://localhost/v1/api/users");
            _httpClient = factory.WithWebHostBuilder(builder =>
            {
                builder.UseSetting("https_port", "5001");
                builder.UseStartup<TestStartup>();
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton(typeof(IUnitOfWork), _fakeUnitOfWork);
                });
            }).CreateClient();
        }

        [Fact]
        public async Task HealthCheck_ReturnsOk()
        {
            var response = await _httpClient.GetAsync("/healthcheck");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}