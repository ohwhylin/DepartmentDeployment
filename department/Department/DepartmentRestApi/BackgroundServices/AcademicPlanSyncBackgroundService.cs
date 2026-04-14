using DepartmentContracts.BusinessLogicsContracts.Sync;
using DepartmentContracts.Configs;
using Microsoft.Extensions.Options;

namespace DepartmentRestApi.BackgroundServices
{
    public class AcademicPlanSyncBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<AcademicPlanSyncBackgroundService> _logger;
        private readonly AcademicPlanSyncScheduleConfig _schedule;

        public AcademicPlanSyncBackgroundService(
            IServiceProvider serviceProvider,
            ILogger<AcademicPlanSyncBackgroundService> logger,
            IOptions<AcademicPlanSyncScheduleConfig> scheduleOptions)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _schedule = scheduleOptions.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!_schedule.Enabled)
            {
                _logger.LogInformation("Academic plan automatic sync is disabled.");
                return;
            }

            _logger.LogInformation(
                "Academic plan automatic sync is enabled. Schedule: {DayOfWeek} {Hour:D2}:{Minute:D2}",
                _schedule.DayOfWeek,
                _schedule.Hour,
                _schedule.Minute);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var nextRun = GetNextRunTime(DateTime.Now, _schedule);
                    var delay = nextRun - DateTime.Now;

                    if (delay < TimeSpan.Zero)
                    {
                        delay = TimeSpan.Zero;
                    }

                    _logger.LogInformation("Next academic plan sync scheduled at {NextRun}", nextRun);

                    await Task.Delay(delay, stoppingToken);

                    if (stoppingToken.IsCancellationRequested)
                    {
                        break;
                    }

                    using var scope = _serviceProvider.CreateScope();
                    var syncLogic = scope.ServiceProvider.GetRequiredService<IAcademicPlanSyncLogic>();

                    _logger.LogInformation("Academic plan automatic sync started at {StartTime}", DateTime.Now);

                    await syncLogic.SyncAcademicPlansAsync();

                    _logger.LogInformation("Academic plan automatic sync completed successfully at {EndTime}", DateTime.Now);
                }
                catch (TaskCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during academic plan automatic sync.");

                    // чтобы при ошибке сервис не умер насовсем
                    await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
                }
            }
        }

        private static DateTime GetNextRunTime(DateTime now, AcademicPlanSyncScheduleConfig schedule)
        {
            if (!Enum.TryParse<DayOfWeek>(schedule.DayOfWeek, true, out var targetDay))
            {
                targetDay = DayOfWeek.Sunday;
            }

            var nextRun = new DateTime(
                now.Year,
                now.Month,
                now.Day,
                schedule.Hour,
                schedule.Minute,
                0);

            var daysUntilTarget = ((int)targetDay - (int)now.DayOfWeek + 7) % 7;

            nextRun = nextRun.AddDays(daysUntilTarget);

            if (nextRun <= now)
            {
                nextRun = nextRun.AddDays(7);
            }

            return nextRun;
        }
    }
}