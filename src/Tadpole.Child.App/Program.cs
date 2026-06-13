using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Tadpole.Client.Shared;
using Tadpole.Client.Shared.Auth;
using Tadpole.Child.App;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTadpoleClient(options =>
    builder.Configuration.GetSection(TadpoleClientOptions.SectionName).Bind(options));

var host = builder.Build();

var session = host.Services.GetRequiredService<AuthSession>();
await session.InitializeAsync();

await host.RunAsync();
