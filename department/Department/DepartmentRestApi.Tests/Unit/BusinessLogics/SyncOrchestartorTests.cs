using DepartmentBusinessLogic.BusinessLogics.Sync;
using DepartmentContracts.BusinessLogicsContracts.Sync;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace DepartmentRestApi.Tests.Unit.BusinessLogics;

[TestFixture]
public class SyncOrchestratorTests
{
    private Mock<IAcademicPlanSyncLogic> _academicPlanSyncLogicMock = null!;
    private Mock<IStudentGroupSyncLogic> _studentGroupSyncLogicMock = null!;
    private Mock<IStudentSyncLogic> _studentSyncLogicMock = null!;
    private Mock<IDisciplineStudentRecordSyncLogic> _disciplineStudentRecordSyncLogicMock = null!;
    private Mock<IStudentOrderSyncLogic> _studentOrderSyncLogicMock = null!;
    private Mock<ILogger<SyncOrchestrator>> _loggerMock = null!;

    private SyncOrchestrator _orchestrator = null!;

    [SetUp]
    public void SetUp()
    {
        _academicPlanSyncLogicMock = new Mock<IAcademicPlanSyncLogic>();
        _studentGroupSyncLogicMock = new Mock<IStudentGroupSyncLogic>();
        _studentSyncLogicMock = new Mock<IStudentSyncLogic>();
        _disciplineStudentRecordSyncLogicMock = new Mock<IDisciplineStudentRecordSyncLogic>();
        _studentOrderSyncLogicMock = new Mock<IStudentOrderSyncLogic>();
        _loggerMock = new Mock<ILogger<SyncOrchestrator>>();

        _orchestrator = new SyncOrchestrator(
            _academicPlanSyncLogicMock.Object,
            _studentGroupSyncLogicMock.Object,
            _studentSyncLogicMock.Object,
            _disciplineStudentRecordSyncLogicMock.Object,
            _studentOrderSyncLogicMock.Object,
            _loggerMock.Object);
    }

    [Test]
    public async Task RunSyncAsync_ShouldCompleteAllSteps_WhenAllLogicsSucceed()
    {
        _academicPlanSyncLogicMock.Setup(x => x.SyncAcademicPlansAsync()).Returns(Task.CompletedTask);
        _studentGroupSyncLogicMock.Setup(x => x.SyncStudentGroupsAsync()).Returns(Task.CompletedTask);
        _studentSyncLogicMock.Setup(x => x.SyncStudentsAsync()).Returns(Task.CompletedTask);
        _disciplineStudentRecordSyncLogicMock.Setup(x => x.SyncDisciplineStudentRecordsAsync()).Returns(Task.CompletedTask);
        _studentOrderSyncLogicMock.Setup(x => x.SyncStudentOrdersAsync()).Returns(Task.CompletedTask);

        var result = await _orchestrator.RunSyncAsync();

        Assert.That(result.Started, Is.True);
        Assert.That(result.Success, Is.True);
        Assert.That(result.Error, Is.Null);
        Assert.That(result.StartedAt, Is.Not.Null);
        Assert.That(result.FinishedAt, Is.Not.Null);

        Assert.That(result.CompletedSteps, Is.EqualTo(new[]
        {
            "Синхронизация учебных планов",
            "Синхронизация студенческих групп",
            "Синхронизация студентов",
            "Синхронизация оценок студентов",
            "Синхронизация распоряжений студентов"
        }));

        _academicPlanSyncLogicMock.Verify(x => x.SyncAcademicPlansAsync(), Times.Once);
        _studentGroupSyncLogicMock.Verify(x => x.SyncStudentGroupsAsync(), Times.Once);
        _studentSyncLogicMock.Verify(x => x.SyncStudentsAsync(), Times.Once);
        _disciplineStudentRecordSyncLogicMock.Verify(x => x.SyncDisciplineStudentRecordsAsync(), Times.Once);
        _studentOrderSyncLogicMock.Verify(x => x.SyncStudentOrdersAsync(), Times.Once);
    }

    [Test]
    public async Task RunSyncAsync_ShouldReturnFailedResult_WhenStepThrows()
    {
        _academicPlanSyncLogicMock.Setup(x => x.SyncAcademicPlansAsync()).Returns(Task.CompletedTask);
        _studentGroupSyncLogicMock.Setup(x => x.SyncStudentGroupsAsync()).Returns(Task.CompletedTask);
        _studentSyncLogicMock
            .Setup(x => x.SyncStudentsAsync())
            .ThrowsAsync(new InvalidOperationException("students sync failed"));

        _disciplineStudentRecordSyncLogicMock
            .Setup(x => x.SyncDisciplineStudentRecordsAsync())
            .Returns(Task.CompletedTask);

        _studentOrderSyncLogicMock
            .Setup(x => x.SyncStudentOrdersAsync())
            .Returns(Task.CompletedTask);

        var result = await _orchestrator.RunSyncAsync();

        Assert.That(result.Started, Is.True);
        Assert.That(result.Success, Is.False);
        Assert.That(result.Error, Does.Contain("students sync failed"));
        Assert.That(result.FinishedAt, Is.Not.Null);

        Assert.That(result.CompletedSteps, Is.EqualTo(new[]
        {
            "Синхронизация учебных планов",
            "Синхронизация студенческих групп"
        }));

        _academicPlanSyncLogicMock.Verify(x => x.SyncAcademicPlansAsync(), Times.Once);
        _studentGroupSyncLogicMock.Verify(x => x.SyncStudentGroupsAsync(), Times.Once);
        _studentSyncLogicMock.Verify(x => x.SyncStudentsAsync(), Times.Once);

        _disciplineStudentRecordSyncLogicMock.Verify(
            x => x.SyncDisciplineStudentRecordsAsync(),
            Times.Never);

        _studentOrderSyncLogicMock.Verify(
            x => x.SyncStudentOrdersAsync(),
            Times.Never);
    }

    [Test]
    public async Task RunAcademicPlansSyncAsync_ShouldRunOnlyAcademicPlansStep()
    {
        _academicPlanSyncLogicMock
            .Setup(x => x.SyncAcademicPlansAsync())
            .Returns(Task.CompletedTask);

        var result = await _orchestrator.RunAcademicPlansSyncAsync();

        Assert.That(result.Started, Is.True);
        Assert.That(result.Success, Is.True);
        Assert.That(result.Error, Is.Null);

        Assert.That(result.CompletedSteps, Is.EqualTo(new[]
        {
            "Синхронизация учебных планов"
        }));

        _academicPlanSyncLogicMock.Verify(x => x.SyncAcademicPlansAsync(), Times.Once);
        _studentGroupSyncLogicMock.Verify(x => x.SyncStudentGroupsAsync(), Times.Never);
        _studentSyncLogicMock.Verify(x => x.SyncStudentsAsync(), Times.Never);
        _disciplineStudentRecordSyncLogicMock.Verify(x => x.SyncDisciplineStudentRecordsAsync(), Times.Never);
        _studentOrderSyncLogicMock.Verify(x => x.SyncStudentOrdersAsync(), Times.Never);
    }

    [Test]
    public async Task RunSyncAsync_ShouldReturnNotStarted_WhenAnotherSynchronizationIsRunning()
    {
        var firstStepStarted = new TaskCompletionSource<bool>();
        var releaseFirstStep = new TaskCompletionSource<bool>();

        _academicPlanSyncLogicMock
            .Setup(x => x.SyncAcademicPlansAsync())
            .Returns(async () =>
            {
                firstStepStarted.TrySetResult(true);
                await releaseFirstStep.Task;
            });

        _studentGroupSyncLogicMock.Setup(x => x.SyncStudentGroupsAsync()).Returns(Task.CompletedTask);
        _studentSyncLogicMock.Setup(x => x.SyncStudentsAsync()).Returns(Task.CompletedTask);
        _disciplineStudentRecordSyncLogicMock.Setup(x => x.SyncDisciplineStudentRecordsAsync()).Returns(Task.CompletedTask);
        _studentOrderSyncLogicMock.Setup(x => x.SyncStudentOrdersAsync()).Returns(Task.CompletedTask);

        var firstRunTask = _orchestrator.RunSyncAsync();

        await firstStepStarted.Task;

        var secondRunResult = await _orchestrator.RunSyncAsync();

        Assert.That(secondRunResult.Started, Is.False);
        Assert.That(secondRunResult.Success, Is.False);
        Assert.That(secondRunResult.Message, Does.Contain("Синхронизация уже выполняется"));

        releaseFirstStep.TrySetResult(true);

        var firstRunResult = await firstRunTask;

        Assert.That(firstRunResult.Started, Is.True);
        Assert.That(firstRunResult.Success, Is.True);

        _academicPlanSyncLogicMock.Verify(x => x.SyncAcademicPlansAsync(), Times.Once);
    }

    [Test]
    public async Task RunAcademicPlansSyncAsync_ShouldReturnFailedResult_WhenLogicThrows()
    {
        _academicPlanSyncLogicMock
            .Setup(x => x.SyncAcademicPlansAsync())
            .ThrowsAsync(new InvalidOperationException("academic plans sync failed"));

        var result = await _orchestrator.RunAcademicPlansSyncAsync();

        Assert.That(result.Started, Is.True);
        Assert.That(result.Success, Is.False);
        Assert.That(result.Error, Does.Contain("academic plans sync failed"));
        Assert.That(result.FinishedAt, Is.Not.Null);
        Assert.That(result.CompletedSteps, Is.Empty);

        _academicPlanSyncLogicMock.Verify(x => x.SyncAcademicPlansAsync(), Times.Once);
        _studentGroupSyncLogicMock.Verify(x => x.SyncStudentGroupsAsync(), Times.Never);
        _studentSyncLogicMock.Verify(x => x.SyncStudentsAsync(), Times.Never);
        _disciplineStudentRecordSyncLogicMock.Verify(x => x.SyncDisciplineStudentRecordsAsync(), Times.Never);
        _studentOrderSyncLogicMock.Verify(x => x.SyncStudentOrdersAsync(), Times.Never);
    }

    [Test]
    public async Task RunSyncAsync_ShouldReturnCanceledResult_WhenCancellationRequestedBetweenSteps()
    {
        using var cts = new CancellationTokenSource();

        _academicPlanSyncLogicMock
            .Setup(x => x.SyncAcademicPlansAsync())
            .Returns(() =>
            {
                cts.Cancel();
                return Task.CompletedTask;
            });

        _studentGroupSyncLogicMock
            .Setup(x => x.SyncStudentGroupsAsync())
            .Returns(Task.CompletedTask);

        _studentSyncLogicMock
            .Setup(x => x.SyncStudentsAsync())
            .Returns(Task.CompletedTask);

        _disciplineStudentRecordSyncLogicMock
            .Setup(x => x.SyncDisciplineStudentRecordsAsync())
            .Returns(Task.CompletedTask);

        _studentOrderSyncLogicMock
            .Setup(x => x.SyncStudentOrdersAsync())
            .Returns(Task.CompletedTask);

        var result = await _orchestrator.RunSyncAsync(cts.Token);

        Assert.That(result.Started, Is.True);
        Assert.That(result.Success, Is.False);
        Assert.That(result.Message, Does.Contain("отменена"));
        Assert.That(result.Error, Is.EqualTo("Операция была отменена."));
        Assert.That(result.FinishedAt, Is.Not.Null);

        Assert.That(result.CompletedSteps, Is.EqualTo(new[]
        {
        "Синхронизация учебных планов"
    }));

        _academicPlanSyncLogicMock.Verify(x => x.SyncAcademicPlansAsync(), Times.Once);
        _studentGroupSyncLogicMock.Verify(x => x.SyncStudentGroupsAsync(), Times.Never);
        _studentSyncLogicMock.Verify(x => x.SyncStudentsAsync(), Times.Never);
        _disciplineStudentRecordSyncLogicMock.Verify(x => x.SyncDisciplineStudentRecordsAsync(), Times.Never);
        _studentOrderSyncLogicMock.Verify(x => x.SyncStudentOrdersAsync(), Times.Never);
    }
}