using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using NLog.Web;

namespace TodoApi {
    public class Program {
        public static void Main (string[] args) {
            var host = CreateWebHostBuilder (args).Build ();
            var logger = host.Services.GetRequiredService<ILogger<Program>> ();
            logger.LogInformation ("Seeded the database.");
            host.Run ();
        }
        public static IWebHostBuilder CreateWebHostBuilder (string[] args) =>
            WebHost.CreateDefaultBuilder (args)
            .UseStartup<Startup> ()
            .UseUrls("http://*:5000")
            .ConfigureLogging (logging => {
                logging.ClearProviders ();
                logging.AddConsole ();
            })
            .UseNLog ();
    }
}