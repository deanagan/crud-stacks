using RealTimeChat.Hubs;
using RealTimeChat.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

builder.Services.AddSingleton<IDictionary<string, UserConnection>>(opts => new Dictionary<string, UserConnection>());

var app = builder.Build();

app.UseRouting();
app.UseCors("AllowAllHeaders");

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<AlertHub>("/chat");
});

app.Run();
