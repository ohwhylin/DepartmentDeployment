using DepartmentContracts.BusinessLogicsContracts.Sync;
using Microsoft.Extensions.Logging;

namespace DepartmentBusinessLogic.BusinessLogics.Sync
{
    public class SyncOrchestrator : ISyncOrchestrator
    {
        private static readonly SemaphoreSlim SyncLock = new(1, 1);

        private readonly IAcademicPlanSyncLogic _academicPlanSyncLogic;
        private readonly IStudentGroupSyncLogic _studentGroupSyncLogic;
        private readonly IStudentSyncLogic _studentSyncLogic;
        private readonly IDisciplineStudentRecordSyncLogic _disciplineStudentRecordSyncLogic;
        private readonly IStudentOrderSyncLogic _studentOrderSyncLogic;
        private readonly ILogger<SyncOrchestrator> _logger;

        public SyncOrchestrator(
            IAcademicPlanSyncLogic academicPlanSyncLogic,
            IStudentGroupSyncLogic studentGroupSyncLogic,
            IStudentSyncLogic studentSyncLogic,
            IDisciplineStudentRecordSyncLogic disciplineStudentRecordSyncLogic,
            IStudentOrderSyncLogic studentOrderSyncLogic,
            ILogger<SyncOrchestrator> logger)
        {
            _academicPlanSyncLogic = academicPlanSyncLogic;
            _studentGroupSyncLogic = studentGroupSyncLogic;
            _studentSyncLogic = studentSyncLogic;
            _disciplineStudentRecordSyncLogic = disciplineStudentRecordSyncLogic;
            _studentOrderSyncLogic = studentOrderSyncLogic;
            _logger = logger;
        }

        public Task<SyncOrchestratorResult> RunSyncAsync(
            CancellationToken cancellationToken = default)
        {
            return RunWithLockAsync(
                "полная синхронизация данных",
                async result =>
                {

                    await RunStepAsync(
                        "Синхронизация учебных планов",
                        () => _academicPlanSyncLogic.SyncAcademicPlansAsync(),
                        result,
                        cancellationToken);

                    await RunStepAsync(
                        "Синхронизация студенческих групп",
                        () => _studentGroupSyncLogic.SyncStudentGroupsAsync(),
                        result,
                        cancellationToken);

                    await RunStepAsync(
                        "Синхронизация студентов",
                        () => _studentSyncLogic.SyncStudentsAsync(),
                        result,
                        cancellationToken);

                    await RunStepAsync(
                        "Синхронизация оценок студентов",
                        () => _disciplineStudentRecordSyncLogic.SyncDisciplineStudentRecordsAsync(),
                        result,
                        cancellationToken);

                    await RunStepAsync(
                        "Синхронизация распоряжений студентов",
                        () => _studentOrderSyncLogic.SyncStudentOrdersAsync(),
                        result,
                        cancellationToken);
                },
                cancellationToken);
        }

        public Task<SyncOrchestratorResult> RunAcademicPlansSyncAsync(
            CancellationToken cancellationToken = default)
        {
            return RunWithLockAsync(
                "синхронизация учебных планов",
                result => RunStepAsync(
                    "Синхронизация учебных планов",
                    () => _academicPlanSyncLogic.SyncAcademicPlansAsync(),
                    result,
                    cancellationToken),
                cancellationToken);
        }

        public Task<SyncOrchestratorResult> RunStudentGroupsSyncAsync(
            CancellationToken cancellationToken = default)
        {
            return RunWithLockAsync(
                "синхронизация студенческих групп",
                result => RunStepAsync(
                    "Синхронизация студенческих групп",
                    () => _studentGroupSyncLogic.SyncStudentGroupsAsync(),
                    result,
                    cancellationToken),
                cancellationToken);
        }

        public Task<SyncOrchestratorResult> RunStudentsSyncAsync(
            CancellationToken cancellationToken = default)
        {
            return RunWithLockAsync(
                "синхронизация студентов",
                result => RunStepAsync(
                    "Синхронизация студентов",
                    () => _studentSyncLogic.SyncStudentsAsync(),
                    result,
                    cancellationToken),
                cancellationToken);
        }

        public Task<SyncOrchestratorResult> RunDisciplineStudentRecordsSyncAsync(
            CancellationToken cancellationToken = default)
        {
            return RunWithLockAsync(
                "синхронизация оценок студентов",
                result => RunStepAsync(
                    "Синхронизация оценок студентов",
                    () => _disciplineStudentRecordSyncLogic.SyncDisciplineStudentRecordsAsync(),
                    result,
                    cancellationToken),
                cancellationToken);
        }

        public Task<SyncOrchestratorResult> RunStudentOrdersSyncAsync(
            CancellationToken cancellationToken = default)
        {
            return RunWithLockAsync(
                "синхронизация распоряжений студентов",
                result => RunStepAsync(
                    "Синхронизация распоряжений студентов",
                    () => _studentOrderSyncLogic.SyncStudentOrdersAsync(),
                    result,
                    cancellationToken),
                cancellationToken);
        }

        private async Task<SyncOrchestratorResult> RunWithLockAsync(
            string syncName,
            Func<SyncOrchestratorResult, Task> syncAction,
            CancellationToken cancellationToken)
        {
            var lockTaken = await SyncLock.WaitAsync(0, cancellationToken);

            if (!lockTaken)
            {
                _logger.LogWarning(
                    "Запуск пропущен: {SyncName} уже выполняется.",
                    syncName);

                return new SyncOrchestratorResult
                {
                    Started = false,
                    Success = false,
                    Message = "Синхронизация уже выполняется. Дождитесь завершения текущего процесса."
                };
            }

            var result = new SyncOrchestratorResult
            {
                Started = true,
                Success = false,
                StartedAt = DateTime.Now,
                Message = $"Начата {syncName}."
            };

            try
            {
                _logger.LogInformation("Начата {SyncName}.", syncName);

                await syncAction(result);

                result.Success = true;
                result.FinishedAt = DateTime.Now;
                result.Message = $"{UpperFirst(syncName)} завершена успешно.";

                _logger.LogInformation(
                    "{SyncName} завершена успешно.",
                    UpperFirst(syncName));

                return result;
            }
            catch (OperationCanceledException)
            {
                result.Success = false;
                result.FinishedAt = DateTime.Now;
                result.Message = $"{UpperFirst(syncName)} отменена.";
                result.Error = "Операция была отменена.";

                _logger.LogWarning(
                    "{SyncName} была отменена.",
                    UpperFirst(syncName));

                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.FinishedAt = DateTime.Now;
                result.Message = $"Ошибка при выполнении операции: {syncName}.";
                result.Error = ex.Message;

                _logger.LogError(
                    ex,
                    "Ошибка при выполнении операции: {SyncName}.",
                    syncName);

                return result;
            }
            finally
            {
                SyncLock.Release();
            }
        }

        private async Task RunStepAsync(
            string stepName,
            Func<Task> action,
            SyncOrchestratorResult result,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _logger.LogInformation("Начат этап: {StepName}.", stepName);

            await action();

            result.CompletedSteps.Add(stepName);

            _logger.LogInformation("Завершен этап: {StepName}.", stepName);
        }

        private static string UpperFirst(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            return char.ToUpper(value[0]) + value[1..];
        }
    }
}