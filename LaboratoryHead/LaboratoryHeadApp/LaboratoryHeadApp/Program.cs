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
    var baseUrl = builder.Configuration["ApiSettings:ScheduleServiceUrl"];

    if (string.IsNullOrWhiteSpace(baseUrl))
    {
        throw new InvalidOperationException("�� �������� ApiSettings:ScheduleServiceUrl");
    }

    client.BaseAddress = new Uri(baseUrl.EndsWith("/") ? baseUrl : baseUrl + "/");

    // ������������� ���������� ����� ���� ������ 100 ������,
    // ������ ��� �������������� ��� ������ ������������
    client.Timeout = TimeSpan.FromMinutes(10);
});

// MOL API
builder.Services.AddHttpClient<IMolApiClient, MolApiClient>(client =>
{
    var baseUrl = builder.Configuration["ApiSettings:MolServiceUrl"];

    if (string.IsNullOrWhiteSpace(baseUrl))
    {
        throw new InvalidOperationException("�� �������� ApiSettings:MolServiceUrl");
    }

    client.BaseAddress = new Uri(baseUrl.EndsWith("/") ? baseUrl : baseUrl + "/");

    // �� ������ ������, ���� ������ ���������/������������ ����� ��������� ������ ��������
    client.Timeout = TimeSpan.FromMinutes(3);
});

builder.Services.AddScoped<IInventoryReportPdfService, InventoryReportPdfService>();

var app = builder.Build();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();