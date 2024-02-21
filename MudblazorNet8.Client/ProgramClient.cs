using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

var Configuration = builder.Configuration;
var Secret = Configuration["Secret"];
Secret = $"{Secret}.Client.Modified.{DateTime.Now}";
Configuration["Secret"] = Secret;

await builder.Build().RunAsync();
