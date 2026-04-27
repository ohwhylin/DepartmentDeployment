using System.Security.Claims;
using GatewayApi.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllersWithViews();

builder.Services.Configure<LdapOptions>(builder.Configuration.GetSection("Ldap"));
builder.Services.AddScoped<LdapLookupService>();

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/auth/login";
        options.LogoutPath = "/auth/logout";
        options.Cookie.Name = "department_auth";
    });

builder.Services.AddAuthorization();
builder.Services.AddOcelot();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// «ащищаем только UI-маршруты.
// /api/core/* пока не трогаем, потому что core-web ходит в gateway сервер-сайд.
app.Use(async (context, next) =>
{
    var path = context.Request.Path;

    var isAuthPath = path.StartsWithSegments("/auth");
    var isUiPath = path.StartsWithSegments("/core");

    if (isUiPath && !isAuthPath && !(context.User.Identity?.IsAuthenticated ?? false))
    {
        var returnUrl = Uri.EscapeDataString(context.Request.Path + context.Request.QueryString);
        context.Response.Redirect($"/auth/login?returnUrl={returnUrl}");
        return;
    }

    await next();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

await app.UseOcelot();

app.Run();