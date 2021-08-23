
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PlanningBoard.Api.IntegrationTests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureDBContext(IServiceCollection services)
        {
            // Stub
        }
        protected override void MigrateDB(IApplicationBuilder app)
        {
            // Stub
        }
    }
}
