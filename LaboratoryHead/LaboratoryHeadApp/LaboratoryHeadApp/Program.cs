using MOLServiceWebClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IScheduleApiClient, ScheduleApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:ScheduleServiceUrl"]);
});

builder.Services.AddHttpClient<IMolApiClient, MolApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:MolServiceUrl"]);
});

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
        context.Request.PathBase = pathBase;
        return next();
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();