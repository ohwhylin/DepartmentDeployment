using LaboratoryHeadApp.Services;
using MOLServiceWebClient;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
QuestPDF.Settings.License = LicenseType.Community;

// MVC
builder.Services.AddControllersWithViews();


// Schedule API
builder.Services.AddHttpClient<IScheduleApiClient, ScheduleApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:ScheduleServiceUrl"]);
});

// MOL API
builder.Services.AddHttpClient<IMolApiClient, MolApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:MolServiceUrl"]);
});
builder.Services.AddScoped<IInventoryReportPdfService, InventoryReportPdfService>();

var app = builder.Build();

// паф бейс
var pathBase = builder.Configuration["PathBase"];
if (!string.IsNullOrWhiteSpace(pathBase))
{
    app.Use((context, next) =>
    {
        context.Request.PathBase = pathBase;
        return next();
    });
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();