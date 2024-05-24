using Erdmier.BlazorCore.Demos.DotNet8.Components;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
       .AddInteractiveServerComponents()
       .AddCircuitOptions(options =>
       {
           if (builder.Environment.IsDevelopment())
           {
               options.DetailedErrors = true;
           }
       });

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (! app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(errorHandlingPath: "/Error", createScopeForErrors: true);
    
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
