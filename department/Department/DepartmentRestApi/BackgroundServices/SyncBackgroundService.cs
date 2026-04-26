using DepartmentContracts.BusinessLogicsContracts.Sync;
using DepartmentContracts.Configs;
using Microsoft.Extensions.Options;

namespace DepartmentRestApi.BackgroundServices
{
    public class SyncBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<SyncBackgroundService> _logger;
        private readonly SyncScheduleConfig _schedule;

        public SyncBackgroundService(
            IServiceProvider serviceProvider,
            ILogger<SyncBackgroundService> logger,
            IOptions<SyncScheduleConfig> scheduleOptions)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _schedule = scheduleOptions.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!_schedule.Enabled)
            {
                _logger.LogInformation("Automatic synchronization is disabled.");
                return;
            }

            _logger.LogInformation(
                "Automatic synchronization is enabled. Schedule: every day at {Hour:D2}:{Minute:D2}",
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

                    _logger.LogInformation(
                        "Next automatic synchronization scheduled at {NextRun}",
                        nextRun);

                    await Task.Delay(delay, stoppingToken);

                    if (stoppingToken.IsCancellationRequested)
                    {
                        break;
                    }

                    using var scope = _serviceProvider.CreateScope();

                    var syncOrchestrator = scope.ServiceProvider
                        .GetRequiredService<ISyncOrchestrator>();

                    _logger.LogInformation(
                        "Automatic synchronization started at {StartTime}",
                        DateTime.Now);

                    var result = await syncOrchestrator.RunSyncAsync(stoppingToken);

                    if (result.Success)
                    {
                        _logger.LogInformation(
                            "Automatic synchronization completed successfully at {EndTime}. Completed steps: {CompletedSteps}",
                            DateTime.Now,
                            string.Join(", ", result.CompletedSteps));
                    }
                    else
                    {
                        _logger.LogWarning(
                            "Automatic synchronization was not completed. Message: {Message}. Error: {Error}",
                            result.Message,
                            result.Error);
                    }
                }
                catch (TaskCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during automatic synchronization.");

                    await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
                }
            }
        }

        private static DateTime GetNextRunTime(DateTime now, SyncScheduleConfig schedule)
        {
            var nextRun = new DateTime(
                now.Year,
                now.Month,
                now.Day,
                schedule.Hour,
                schedule.Minute,
                0);

            if (nextRun <= now)
            {
                nextRun = nextRun.AddDays(1);
            }

            return nextRun;
        }
    }
}