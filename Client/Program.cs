using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            builder.Services.AddScoped(sp =>
            {
                var baseUrl = builder.Configuration["Api:BaseUrl"];
                if (string.IsNullOrEmpty(baseUrl))
                    throw new InvalidOperationException("Api:BaseUrl is not configured in appsettings.json");

                return new HttpClient { BaseAddress = new Uri(baseUrl) };
            });

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}
