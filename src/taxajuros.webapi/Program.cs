using TaxaJuros.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;

namespace TaxaJuros.Webapi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://*:5009", "http://*:83")
                .UseStartup<Startup>();
    }
}
