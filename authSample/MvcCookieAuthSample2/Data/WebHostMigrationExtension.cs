using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MvcCookieAuthSample.Data
{
    public static class WebHostMigrationExtension
    {
        public static IWebHost MigrationDbContext<TContext>(this IWebHost host, Action<TContext, IServiceProvider> serviceAction)
            where TContext : DbContext
        {
            using (var scope=host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();
                try
                {
                    context.Database.Migrate();
                    serviceAction(context, services);
                    logger.LogInformation($"执行DbContext{typeof(TContext).Name}seed方法成功");

                }
                catch (Exception ex)
                {
                    logger.LogError(ex,$"执行DbContext{typeof(TContext).Name}seed方法失败");
                }
            }
            return host;
        }
    }
}
