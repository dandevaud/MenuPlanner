using System;
using System.Net.Http;
using System.Threading.Tasks;
using MenuPlanner.Client.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace MenuPlanner.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("MenuPlanner.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
            //https://chrissainty.com/avoiding-accesstokennotavailableexception-when-using-blazor-webassembly-hosted-template-with-individual-user-accounts/
            builder.Services.AddHttpClient<PublicClient>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("MenuPlanner.ServerAPI"));
            builder.Services.AddApiAuthorization(options =>
            {
                options.ProviderOptions.ConfigurationEndpoint = "oidc.json";
            });

            await builder.Build().RunAsync();
        }
    }
}
