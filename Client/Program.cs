using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using Client.Auth;

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

            builder.Services.AddScoped<JwtAuthenticationStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
                sp.GetRequiredService<JwtAuthenticationStateProvider>());
            builder.Services.AddAuthorizationCore();

            await builder.Build().RunAsync();
        }
    }
}
