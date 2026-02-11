using CurrieTechnologies.Razor.SweetAlert2;
using Delab.AccessService.Repositories;
using Delab.Front;
using Delab.Front.AutheticationProviders;
using Delab.Front.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7204") });

builder.Services.AddMudServices();

builder.Services.AddAuthorizationCore();

builder.Services.AddSweetAlert2();

builder.Services.AddScoped<HttpResponseHandler>();

builder.Services.AddScoped(sp =>
{
    var jsRuntime = sp.GetRequiredService<IJSRuntime>(); // Obtener el IJSRuntime
    var httpClient = sp.GetRequiredService<HttpClient>(); ;

    // Usar tu clase de extensión para obtener el token desde localStorage
    return new Repository(
        httpClient,
        async () =>
        {
            var token = await jsRuntime.GetLocalStorage("TOKEN_KEY");
            return Convert.ToString(token); // Asegurarse de que el token es un string
        }
    );
});

// Registrar IRepository como Repository
builder.Services.AddScoped<IRepository>(sp => sp.GetRequiredService<Repository>());

//Authentication Provider
builder.Services.AddScoped<AuthenticationProviderJWT>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());
builder.Services.AddScoped<ILoginService, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());

await builder.Build().RunAsync();
