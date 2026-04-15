using DepartmentBusinessLogic.BusinessLogics;
using DepartmentContracts.BusinessLogicsContracts;
using DepartmentContracts.StoragesContracts;
using DepartmentDatabaseImplement.Implements;
using Microsoft.OpenApi.Models;
using DepartmentBusinessLogic.Services.OneC;
using DepartmentContracts.Configs;
using DepartmentBusinessLogic.BusinessLogics.Sync;
using DepartmentContracts.BusinessLogicsContracts.Sync;
using DepartmentRestApi.BackgroundServices;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.SetMinimumLevel(LogLevel.Trace);

// Add services to the container.
builder.Services.AddTransient<IAcademicPlanStorage, AcademicPlanStorage>();
builder.Services.AddTransient<IAcademicPlanRecordStorage, AcademicPlanRecordStorage>();
builder.Services.AddTransient<IClassroomStorage, ClassroomStorage>();
builder.Services.AddTransient<IDisciplineStorage, DisciplineStorage>();
builder.Services.AddTransient<IDisciplineBlockStorage, DisciplineBlockStorage>();
builder.Services.AddTransient<IDisciplineStudentRecordStorage, DisciplineStudentRecordStorage>();
builder.Services.AddTransient<IEducationDirectionStorage, EducationDirectionStorage>();
builder.Services.AddTransient<ILecturerStorage, LecturerStorage>();
builder.Services.AddTransient<ILecturerStudyPostStorage, LecturerStudyPostStorage>();
builder.Services.AddTransient<ILecturerDepartmentPostStorage, LecturerDepartmentPostStorage>();
builder.Services.AddTransient<IStudentStorage, StudentStorage>();
builder.Services.AddTransient<IStudentGroupStorage, StudentGroupStorage>();
builder.Services.AddTransient<IStudentOrderStorage, StudentOrderStorage>();
builder.Services.AddTransient<IStudentOrderBlockStorage, StudentOrderBlockStorage>();
builder.Services.AddTransient<IStudentOrderBlockStudentStorage, StudentOrderBlockStudentStorage>();

builder.Services.AddTransient<IAcademicPlanLogic, AcademicPlanLogic>();
builder.Services.AddTransient<IAcademicPlanRecordLogic, AcademicPlanRecordLogic>();
builder.Services.AddTransient<IClassroomLogic, ClassroomLogic>();
builder.Services.AddTransient<IDisciplineLogic, DisciplineLogic>();
builder.Services.AddTransient<IDisciplineBlockLogic, DisciplineBlockLogic>();
builder.Services.AddTransient<IDisciplineStudentRecordLogic, DisciplineStudentRecordLogic>();
builder.Services.AddTransient<IEducationDirectionLogic, EducationDirectionLogic>();
builder.Services.AddTransient<ILecturerLogic, LecturerLogic>();
builder.Services.AddTransient<ILecturerStudyPostLogic, LecturerStudyPostLogic>();
builder.Services.AddTransient<ILecturerDepartmentPostLogic, LecturerDepartmentPostLogic>();
builder.Services.AddTransient<IStudentLogic, StudentLogic>();
builder.Services.AddTransient<IStudentGroupLogic, StudentGroupLogic>();
builder.Services.AddTransient<IStudentOrderLogic, StudentOrderLogic>();
builder.Services.AddTransient<IStudentOrderBlockLogic, StudentOrderBlockLogic>();
builder.Services.AddTransient<IStudentOrderBlockStudentLogic, StudentOrderBlockStudentLogic>();

// ���� ����
builder.Services.Configure<OneCConnectionConfig>(builder.Configuration.GetSection("OneCConnection"));
builder.Services.Configure<AcademicPlanSyncScheduleConfig>(builder.Configuration.GetSection("AcademicPlanSyncSchedule"));
builder.Services.AddHostedService<AcademicPlanSyncBackgroundService>();

builder.Services.AddHttpClient<IOneCApiService, OneCApiService>();
builder.Services.AddScoped<IAcademicPlanSyncLogic, AcademicPlanSyncLogic>();
builder.Services.AddScoped<IStudentGroupSyncLogic, StudentGroupSyncLogic>();
builder.Services.AddScoped<IStudentSyncLogic, StudentSyncLogic>();
builder.Services.AddScoped<IDisciplineStudentRecordSyncLogic, DisciplineStudentRecordSyncLogic>();
builder.Services.AddScoped<IStudentOrderSyncLogic, StudentOrderSyncLogic>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DepartmentRestApi", Version = "v1"});
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "swagger";
    c.SwaggerEndpoint("v1/swagger.json", "DepartmentRestApi");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
