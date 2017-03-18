using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Server.Kestrel.Https;

namespace Angular2App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                .Build();
           var cert = new X509Certificate2("Angular2App.pfx", 
                "Diawest95");
                
            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel(cfg => cfg.UseHttps(cert))
                .UseUrls("https://localhost:8090")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
