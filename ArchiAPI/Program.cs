using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConfigureLogger();
            Log.Information("Starting Application");
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            finally
            {
                Log.Information("Closing Application");
                Log.CloseAndFlush();
            }
            //CreateHostBuilder(args).Build().Run();
        }

        // method for logs
        public static void ConfigureLogger()
        {
            // add different paramater for he logging like Memory, thread, CorrelationId...
            Log.Logger = new LoggerConfiguration()
                        .Enrich.WithMachineName()
                        .WriteTo.Console(outputTemplate : "[{Timestamp: MM/dd/HH:mm:ss} MachineName {MachineName} {Level:u3}] {Message:lj} {NewLine}{Exception}").CreateLogger();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
