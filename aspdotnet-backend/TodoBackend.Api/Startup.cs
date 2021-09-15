using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using DbUp;
using TodoBackend.Api.Data.Access;
using TodoBackend.Api.Services;
using TodoBackend.Api.Interfaces;
using TodoBackend.Api.Bindings;

namespace TodoBackend.Api
{
    public class Startup
    {
        readonly string AllowSpecificOrigins = "_AllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
             services.AddCors(options =>
            {
                options.AddPolicy(name: AllowSpecificOrigins,
                              builder =>
                              {
                                  builder.WithOrigins("*").AllowAnyHeader();
                              });
            });

            services.AddControllers();
            services.AddHealthChecks();
            ConfigureDBContext(services);
            services.AddScoped(typeof(IRolesRepository), typeof(RolesRepository));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(ITodoRepository), typeof(TodoRepository));
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Todo", Version = "v1" });
            });

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<ITodoService, TodoService>();
        }

        public virtual void ConfigureDBContext(IServiceCollection services)
        {
            var connectionString = Configuration["ConnectionStrings:DefaultConnection"];

            EnsureDatabase.For.SqlDatabase(connectionString);
            var upgrader = DeployChanges.To
                .SqlDatabase(connectionString)
                .WithScriptsFromFileSystem(@"./SqlScripts")
                .WithTransactionPerScript()
                .LogToConsole()
                .Build();

            if (upgrader.IsUpgradeRequired())
            {
                upgrader.PerformUpgrade();
            }

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(AllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/healthcheck");
                endpoints.MapControllers();
            });
        }
    }
}
