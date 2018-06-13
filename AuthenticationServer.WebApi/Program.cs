using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NLog.Common;

namespace AuthenticationServer.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            InternalLogger.LogFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", "nlog-internals.txt");
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
    }
}
