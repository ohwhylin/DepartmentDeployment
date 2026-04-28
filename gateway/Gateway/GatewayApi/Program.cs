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
        options.Cookie.Name = "polina_auth";
    });

builder.Services.AddAuthorization();
builder.Services.AddOcelot();

var app = builder.Build();

var pathBase = builder.Configuration["PathBase"];
if (!string.IsNullOrWhiteSpace(pathBase))
{
    app.UsePathBase(pathBase);
}

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    var path = context.Request.Path;

    var isAuthPath = path.StartsWithSegments("/auth");
    var isUiPath =
        path.StartsWithSegments("/core") ||
        path.StartsWithSegments("/load") ||
        path.StartsWithSegments("/lab");

    if (isUiPath && !isAuthPath && !(context.User.Identity?.IsAuthenticated ?? false))
    {
        var returnUrl = Uri.EscapeDataString(
            $"{context.Request.PathBase}{context.Request.Path}{context.Request.QueryString}");

        context.Response.Redirect(
            $"{context.Request.PathBase}/auth/login?returnUrl={returnUrl}");
        return;
    }

    await next();
});

// ВАЖНО: auth-контроллеры должны мапиться ДО Ocelot
app.MapControllers();

await app.UseOcelot();

app.Run();