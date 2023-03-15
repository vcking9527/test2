using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AspectCore.DynamicProxy;
using AspectCore.Extensions.DependencyInjection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using NLog.Web;

namespace ES.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            NLogBuilder.ConfigureNLog("NLog.config");
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                .UseUrls("http://*:60590")
                .UseStartup<Startup>();
            })
               .UseNLog()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory());
    }
}
