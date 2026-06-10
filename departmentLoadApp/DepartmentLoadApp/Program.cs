using System.Globalization;
using System.IO;
using DepartmentLoadApp.Data;
using DepartmentLoadApp.Helpers;
using DepartmentLoadApp.Integration.CoreApi;
using DepartmentLoadApp.Integration.CoreSync;
using DepartmentLoadApp.Integration.CoreSync.Interfaces;
using DepartmentLoadApp.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo("/keys"))
    .SetApplicationName("DepartmentLoadApp");

builder.Services.AddDbContext<DepartmentLoadDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<CalculationImportService>();
builder.Services.AddScoped<WorkloadCalculationService>();
builder.Services.AddScoped<PracticeCalculationService>();
builder.Services.AddScoped<GiaCalculationService>();
builder.Services.AddScoped<WorkloadDistributionService>();
builder.Services.AddScoped<IndividualPlanService>();
builder.Services.AddScoped<ContingentService>();
builder.Services.AddScoped<AdditionalWorkCalculationService>();

builder.Services.AddHttpClient<CoreApiService>(client =>
{
    var baseUrl = builder.Configuration["CoreApi:BaseUrl"];

    client.BaseAddress = new Uri(
        string.IsNullOrWhiteSpace(baseUrl)
            ? "http://core-api:8080/api/"
            : baseUrl);
});

builder.Services.AddScoped<IEducationDirectionSyncService, EducationDirectionSyncService>();
builder.Services.AddScoped<ILecturerStudyPostSyncService, LecturerStudyPostSyncService>();
builder.Services.AddScoped<ILecturerDepartmentPostSyncService, LecturerDepartmentPostSyncService>();
builder.Services.AddScoped<ILecturerSyncService, LecturerSyncService>();
builder.Services.AddScoped<IStudentGroupSyncService, StudentGroupSyncService>();
builder.Services.AddScoped<IAcademicPlanSyncService, AcademicPlanSyncService>();
builder.Services.AddScoped<IAcademicPlanRecordSyncService, AcademicPlanRecordSyncService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

var culture = new CultureInfo("ru-RU");

var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(culture),
    SupportedCultures = new[] { culture },
    SupportedUICultures = new[] { culture }
};

app.UseRequestLocalization(localizationOptions);

var pathBase = builder.Configuration["PathBase"];
if (!string.IsNullOrWhiteSpace(pathBase))
{
    app.Use((context, next) =>
    {
        context.Request.PathBase = pathBase;
        return next();
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DepartmentLoadDbContext>();
    db.Database.Migrate();
}

app.Run();