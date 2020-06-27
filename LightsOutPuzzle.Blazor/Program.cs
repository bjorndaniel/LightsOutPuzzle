using System.Threading.Tasks;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
namespace LightsOutPuzzle.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddBlazoredToast();
            await builder.Build().RunAsync();
        }
    }
}