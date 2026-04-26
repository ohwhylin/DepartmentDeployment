namespace DepartmentContracts.BusinessLogicsContracts.Sync
{
    public class SyncOrchestratorResult
    {
        public bool Started { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public string? Error { get; set; }

        public DateTime? StartedAt { get; set; }

        public DateTime? FinishedAt { get; set; }

        public List<string> CompletedSteps { get; set; } = new();
    }
}