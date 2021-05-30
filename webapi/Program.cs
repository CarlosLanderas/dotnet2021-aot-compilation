using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Diagnostics;
using System;

var st = Stopwatch.StartNew();

Host.CreateDefaultBuilder()
    .ConfigureWebHostDefaults(config =>
    {
        config
        .ConfigureServices(services => services.AddSingleton<UserService>())
        .Configure(app =>
        {
            var hostLifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();
            hostLifetime.ApplicationStarted.Register(() =>
            {
                Console.WriteLine($"Application started in {st.Elapsed.TotalMilliseconds}");
            });

            app
            .UseRouting()
            .UseEndpoints(config =>
            {
                config.MapGet("/hello", async context =>
                {
                    var svc = context.RequestServices.GetService<UserService>();
                    var user = context.Request.Query["user"].FirstOrDefault();
                    await context.Response.WriteAsync(svc.Salute(user));
                });
            });
        });
    })
    .Build()
    .Run();


class UserService
{
    public string Salute(string userName) => $"Hello {userName}";    
}