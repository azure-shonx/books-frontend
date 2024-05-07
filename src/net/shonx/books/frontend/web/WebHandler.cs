namespace net.shonx.books.frontend.web;

using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web.UI;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

//*************************************************************
// ██     ██  █████  ██████  ███    ██ ██ ███    ██  ██████  
// ██     ██ ██   ██ ██   ██ ████   ██ ██ ████   ██ ██       
// ██  █  ██ ███████ ██████  ██ ██  ██ ██ ██ ██  ██ ██   ███ 
// ██ ███ ██ ██   ██ ██   ██ ██  ██ ██ ██ ██  ██ ██ ██    ██ 
//  ███ ███  ██   ██ ██   ██ ██   ████ ██ ██   ████  ██████  
//*************************************************************
// ON AZURE, THE ENVIRONMENT VARIABLE
// `ASPNETCORE_FORWARDEDHEADERS_ENABLED`
// MUST BE SET TO `true` IN ORDER FOR LOGIN
// TO WORK CORRECTLY.

public class WebHandler
{
    private readonly WebApplicationBuilder builder;
    private readonly WebApplication app;
    public WebHandler(string[] args)
    {
        builder = WebApplication.CreateBuilder(args);
        builder.Configuration.AddJsonFile("appsettings.json");

        builder.Services.AddMicrosoftIdentityWebAppAuthentication(builder.Configuration)
            .EnableTokenAcquisitionToCallDownstreamApi(GetScopes())
                .AddDownstreamApi("Books", builder.Configuration.GetSection("DownstreamApi"))
                .AddInMemoryTokenCaches();

        builder.Services.Configure<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme, options =>
        {
            options.TokenValidationParameters.RoleClaimType = "roles";
        });
        // Add services to the container.
        builder.Services.AddControllersWithViews().AddMvcOptions(options =>
        {
            var policy = new AuthorizationPolicyBuilder()
                          .RequireAuthenticatedUser()
                          .Build();
            options.Filters.Add(new AuthorizeFilter(policy));
        }).AddMicrosoftIdentityUI();

        app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseForwardedHeaders();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{ID?}");
    }

    private IEnumerable<string> GetScopes()
    {
        foreach (var scope in builder.Configuration.GetSection("DownstreamApi:Scopes").GetChildren())
        {
            yield return scope.Value ?? throw new NullReferenceException();
        }
    }

    public void Run()
    {
        BookIndex.Initalize();
        app.Run();
    }
}
