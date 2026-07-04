using Helpdesk.Web;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var uriString = builder.Configuration["ApiSettings:BaseUri"];

if (Uri.TryCreate(uriString, UriKind.Absolute, out var uriSettings))
{
    builder.Services.AddScoped(sp => new HttpClient {BaseAddress = uriSettings });
}
else
{
    throw new Exception("La Uri base no esta bien configurada. Verifique.");
}

await builder.Build().RunAsync();
