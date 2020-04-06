using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Elsa.Guides.HelloWorld.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://0.0.0.0:5100")
                .UseStartup<Startup>();
    }
}