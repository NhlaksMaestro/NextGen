using Microsoft.AspNetCore.Hosting;
using NextGen.Web;

public class Program
{
    public static void Main(string[] args) => Program.CreateHostBuilder(args).Build().Run();

    public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults((Action<IWebHostBuilder>)(webBuilder => webBuilder.UseStartup<StartUp>()));
}
