using LaboratoryHeadApp.Services;
using MOLServiceWebClient;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

QuestPDF.Settings.License = LicenseType.Community;

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IScheduleApiClient, ScheduleApiClient>(client =>
{
    var baseUrl = builder.Configuration["ApiSettings:ScheduleServiceUrl"];

    if (string.IsNullOrWhiteSpace(baseUrl))
    {
        throw new InvalidOperationException("Не указан ApiSettings:ScheduleServiceUrl");
    }

    client.BaseAddress = new Uri(baseUrl.EndsWith("/") ? baseUrl : baseUrl + "/");
    client.Timeout = TimeSpan.FromMinutes(10);
});

builder.Services.AddHttpClient<IMolApiClient, MolApiClient>(client =>
{
    var baseUrl = builder.Configuration["ApiSettings:MolServiceUrl"];

    if (string.IsNullOrWhiteSpace(baseUrl))
    {
        throw new InvalidOperationException("Не указан ApiSettings:MolServiceUrl");
    }

    client.BaseAddress = new Uri(baseUrl.EndsWith("/") ? baseUrl : baseUrl + "/");
    client.Timeout = TimeSpan.FromMinutes(3);
});

builder.Services.AddScoped<IInventoryReportPdfService, InventoryReportPdfService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

var pathBase = builder.Configuration["PathBase"];
if (!string.IsNullOrWhiteSpace(pathBase))
{
    app.Use((context, next) =>
    {
        if (context.Request.Path.StartsWithSegments(pathBase, out var remaining))
        {
            context.Request.PathBase = pathBase;
            context.Request.Path = remaining;
        }
        else
        {
            context.Request.PathBase = pathBase;
        }

        return next();
    });
}

// app.UseHttpsRedirection();  // убрать
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();