using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Smarti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smarti.Services
{
    public static class IWebHostExtensions
    {
        public static IWebHost Seed(this IWebHost webhost)
        {
            using (var scope = webhost.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<IDbInitializer>().Initialize();//.GetAwaiter().GetResult();
            }

            //return Task.FromResult(webhost).GetAwaiter().GetResult();
            return webhost;
        }
    }
}
