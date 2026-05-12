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

builder.Services.AddHttpClient<CoreAuthApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["CoreAuth:BaseUrl"]!);
});

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

static bool HasPermission(HttpContext context, string permission) =>
    context.User.Claims.Any(x => x.Type == "perm" && x.Value == permission);

static bool StartsWithAny(PathString path, params string[] prefixes) =>
    prefixes.Any(prefix => path.StartsWithSegments(prefix));

static Task DenyAsync(HttpContext context)
{
    context.Response.Redirect($"{context.Request.PathBase}/auth/forbidden");
    return Task.CompletedTask;
}

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

    // €дро
    if (path.StartsWithSegments("/core") && !HasPermission(context, "Core.Access"))
    {
        await DenyAsync(context);
        return;
    }

    // нагрузка
    if (path.StartsWithSegments("/load") && !HasPermission(context, "Load.Access"))
    {
        await DenyAsync(context);
        return;
    }

    // лабораторный модуль
    if (path.StartsWithSegments("/lab"))
    {
        // ћќЋ Ч только завлаб / developer
        if (StartsWithAny(path,
                "/lab/Mol",
                "/lab/Classroom",
                "/lab/EquipmentMovementHistory",
                "/lab/InventoryReport",
                "/lab/MaterialResponsiblePerson",
                "/lab/MaterialTechnicalValue",
                "/lab/Software",
                "/lab/SoftwareRecord",
                "/lab/OneCImport"))
        {
            if (!HasPermission(context, "Lab.Inventory.Access"))
            {
                await DenyAsync(context);
                return;
            }
        }
        // график дежурств
        else if (StartsWithAny(path,
                     "/lab/DutyPerson",
                     "/lab/DutySchedule",
                     "/lab/Group",
                     "/lab/LessonTime",
                     "/lab/Teacher"))
        {
            if (!HasPermission(context, "Lab.DutySchedule.Access"))
            {
                await DenyAsync(context);
                return;
            }
        }
        // добавление / редактирование / удаление консультаций
        else if (StartsWithAny(path,
                     "/lab/ClassroomReservation"))
        {
            if (!HasPermission(context, "Lab.Schedule.BookConsultation"))
            {
                await DenyAsync(context);
                return;
            }
        }
        // остальное внутри lab Ч обычный просмотр расписани€
        else
        {
            var hasLabAccess =
                HasPermission(context, "Lab.Schedule.View") ||
                HasPermission(context, "Lab.DutySchedule.Access") ||
                HasPermission(context, "Lab.Inventory.Access") ||
                HasPermission(context, "Lab.Schedule.BookConsultation");

            if (!hasLabAccess)
            {
                await DenyAsync(context);
                return;
            }
        }
    }

    await next();
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

await app.UseOcelot();

app.Run();