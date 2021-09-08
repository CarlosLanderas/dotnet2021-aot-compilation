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
            var hostlifeTime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
            hostlifeTime.ApplicationStarted.Register(() =>
            {
                Console.WriteLine($"Elapsed time: {st.Elapsed.TotalMilliseconds}");
            });

            app
            .UseRouting()
            .UseEndpoints(config =>
            {
                config.MapGet("/hello", async context =>
                {
                    var svc = context.RequestServices.GetRequiredService<UserService>();
                    var userName = context.Request.Query["name"].FirstOrDefault();
                    await context.Response.WriteAsync(svc.Salute(userName));
                });
            });
        });
    })
    .Build()
    .Run();

class UserService
{
    public string Salute(string user)
    {
        return $"Hello mr/mrs {user}";
    }
}