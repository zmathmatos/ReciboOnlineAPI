using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Qsti.ReciboOnline.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //CreateWebHostBuilder(args).Build().Run();
            var host = new WebHostBuilder()
             .UseKestrel()
             .UseContentRoot(Directory.GetCurrentDirectory())
             .UseIISIntegration()
             .UseStartup<Startup>()
             .ConfigureAppConfiguration((hostContext, config) =>
             {
                 config.Sources.Clear();
                 config.AddJsonFile("appsettings.json", optional: true);
             })
             .Build();
            host.Run();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
    }
}
