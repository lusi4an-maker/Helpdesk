using Helpdesk.Web;
using Helpdesk.Web.Services;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();

builder.Services.AddTransient<AuthHeaderHandler>();

var uriString = builder.Configuration["ApiSettings:BaseUrl"];

if (Uri.TryCreate(uriString, UriKind.Absolute, out var uriSettings))
{
    builder.Services.AddHttpClient("client", c =>
    {
        c.BaseAddress = uriSettings;
    })
        .AddHttpMessageHandler<AuthHeaderHandler>();
}
else
{
    throw new Exception("La Uri base no esta bien configurada. Verifique.");
}

builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<CustomAuthenticationStateProvider>();

builder.Services.AddScoped<AuthenticationStateProvider>( sp =>
{
    var customService = sp.GetRequiredService<CustomAuthenticationStateProvider>();
    return customService;
});

builder.Services.AddScoped(sp =>
{
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    return factory.CreateClient("client");
});

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();

await builder.Build().RunAsync();
