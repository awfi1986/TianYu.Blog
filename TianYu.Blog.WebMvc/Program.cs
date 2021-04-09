using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace TianYu.Blog.WebMvc
{
    public class Program
    {
        public static void Main(string[] args)
        { 
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((ILoggingBuilder logBuilder) => {
                    logBuilder.AddNLog();
                    logBuilder.AddConsole();    
                    NLog.LogManager.LoadConfiguration("XmlConfig/nlog.config");
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .UseUrls("http://localhost:8081")
                    .UseStartup<Startup>();
                });
    }
}
