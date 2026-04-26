namespace DepartmentContracts.BusinessLogicsContracts.Sync
{
    public interface ISyncOrchestrator
    {
        Task<SyncOrchestratorResult> RunSyncAsync(
            CancellationToken cancellationToken = default);

        Task<SyncOrchestratorResult> RunAcademicPlansSyncAsync(
            CancellationToken cancellationToken = default);

        Task<SyncOrchestratorResult> RunStudentGroupsSyncAsync(
            CancellationToken cancellationToken = default);

        Task<SyncOrchestratorResult> RunStudentsSyncAsync(
            CancellationToken cancellationToken = default);

        Task<SyncOrchestratorResult> RunDisciplineStudentRecordsSyncAsync(
            CancellationToken cancellationToken = default);

        Task<SyncOrchestratorResult> RunStudentOrdersSyncAsync(
            CancellationToken cancellationToken = default);
    }
}