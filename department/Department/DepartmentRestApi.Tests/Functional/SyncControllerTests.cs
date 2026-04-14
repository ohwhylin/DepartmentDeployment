using System.Text.Json;
using DepartmentContracts.BusinessLogicsContracts.Sync;
using DepartmentRestApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace DepartmentRestApi.Tests.Functional;

[TestFixture]
public class SyncControllerTests
{
    private Mock<IAcademicPlanSyncLogic> _academicPlanSyncLogicMock = null!;
    private Mock<IStudentGroupSyncLogic> _studentGroupSyncLogicMock = null!;
    private Mock<IStudentSyncLogic> _studentSyncLogicMock = null!;
    private Mock<IDisciplineStudentRecordSyncLogic> _disciplineStudentRecordSyncLogicMock = null!;
    private Mock<IStudentOrderSyncLogic> _studentOrderSyncLogicMock = null!;

    private SyncController _controller = null!;

    [SetUp]
    public void SetUp()
    {
        _academicPlanSyncLogicMock = new Mock<IAcademicPlanSyncLogic>();
        _studentGroupSyncLogicMock = new Mock<IStudentGroupSyncLogic>();
        _studentSyncLogicMock = new Mock<IStudentSyncLogic>();
        _disciplineStudentRecordSyncLogicMock = new Mock<IDisciplineStudentRecordSyncLogic>();
        _studentOrderSyncLogicMock = new Mock<IStudentOrderSyncLogic>();

        _controller = new SyncController(
            _academicPlanSyncLogicMock.Object,
            _studentGroupSyncLogicMock.Object,
            _studentSyncLogicMock.Object,
            _disciplineStudentRecordSyncLogicMock.Object,
            _studentOrderSyncLogicMock.Object);
    }

    [Test]
    public async Task SyncAcademicPlans_ShouldReturnOk_WhenLogicSucceeds()
    {
        _academicPlanSyncLogicMock
            .Setup(x => x.SyncAcademicPlansAsync())
            .Returns(Task.CompletedTask);

        var result = await _controller.SyncAcademicPlans();

        Assert.That(result, Is.InstanceOf<OkObjectResult>());

        var okResult = (OkObjectResult)result;
        Assert.That(okResult.Value, Is.EqualTo("Academic plans synchronization completed successfully."));

        _academicPlanSyncLogicMock.Verify(x => x.SyncAcademicPlansAsync(), Times.Once);
        VerifyNoOtherCalls();
    }

    [Test]
    public async Task SyncAcademicPlans_ShouldReturn500_WhenLogicThrows()
    {
        _academicPlanSyncLogicMock
            .Setup(x => x.SyncAcademicPlansAsync())
            .ThrowsAsync(new Exception("academic plans test error"));

        var result = await _controller.SyncAcademicPlans();

        Assert.That(result, Is.InstanceOf<ObjectResult>());

        var objectResult = (ObjectResult)result;
        Assert.That(objectResult.StatusCode, Is.EqualTo(500));

        var json = JsonSerializer.Serialize(objectResult.Value);
        Assert.That(json, Does.Contain("Internal server error"));
        Assert.That(json, Does.Contain("academic plans test error"));

        _academicPlanSyncLogicMock.Verify(x => x.SyncAcademicPlansAsync(), Times.Once);
        VerifyNoOtherCalls();
    }

    [Test]
    public async Task SyncStudentGroups_ShouldReturnOk_WhenLogicSucceeds()
    {
        _studentGroupSyncLogicMock
            .Setup(x => x.SyncStudentGroupsAsync())
            .Returns(Task.CompletedTask);

        var result = await _controller.SyncStudentGroups();

        Assert.That(result, Is.InstanceOf<OkObjectResult>());

        var okResult = (OkObjectResult)result;
        Assert.That(okResult.Value, Is.EqualTo("Student groups synchronized successfully."));

        _studentGroupSyncLogicMock.Verify(x => x.SyncStudentGroupsAsync(), Times.Once);
        VerifyNoOtherCalls();
    }

    [Test]
    public async Task SyncStudentGroups_ShouldReturn500_WhenLogicThrows()
    {
        _studentGroupSyncLogicMock
            .Setup(x => x.SyncStudentGroupsAsync())
            .ThrowsAsync(new Exception("student groups test error"));

        var result = await _controller.SyncStudentGroups();

        Assert.That(result, Is.InstanceOf<ObjectResult>());

        var objectResult = (ObjectResult)result;
        Assert.That(objectResult.StatusCode, Is.EqualTo(500));

        var json = JsonSerializer.Serialize(objectResult.Value);
        Assert.That(json, Does.Contain("Internal server error"));
        Assert.That(json, Does.Contain("student groups test error"));

        _studentGroupSyncLogicMock.Verify(x => x.SyncStudentGroupsAsync(), Times.Once);
        VerifyNoOtherCalls();
    }

    [Test]
    public async Task SyncStudents_ShouldReturnOk_WhenLogicSucceeds()
    {
        _studentSyncLogicMock
            .Setup(x => x.SyncStudentsAsync())
            .Returns(Task.CompletedTask);

        var result = await _controller.SyncStudents();

        Assert.That(result, Is.InstanceOf<OkObjectResult>());

        var okResult = (OkObjectResult)result;
        Assert.That(okResult.Value, Is.EqualTo("Students synchronized successfully."));

        _studentSyncLogicMock.Verify(x => x.SyncStudentsAsync(), Times.Once);
        VerifyNoOtherCalls();
    }

    [Test]
    public async Task SyncStudents_ShouldReturn500_WhenLogicThrows()
    {
        _studentSyncLogicMock
            .Setup(x => x.SyncStudentsAsync())
            .ThrowsAsync(new Exception("students test error"));

        var result = await _controller.SyncStudents();

        Assert.That(result, Is.InstanceOf<ObjectResult>());

        var objectResult = (ObjectResult)result;
        Assert.That(objectResult.StatusCode, Is.EqualTo(500));

        var json = JsonSerializer.Serialize(objectResult.Value);
        Assert.That(json, Does.Contain("Internal server error"));
        Assert.That(json, Does.Contain("students test error"));

        _studentSyncLogicMock.Verify(x => x.SyncStudentsAsync(), Times.Once);
        VerifyNoOtherCalls();
    }

    [Test]
    public async Task SyncDisciplineStudentRecords_ShouldReturnOk_WhenLogicSucceeds()
    {
        _disciplineStudentRecordSyncLogicMock
            .Setup(x => x.SyncDisciplineStudentRecordsAsync())
            .Returns(Task.CompletedTask);

        var result = await _controller.SyncDisciplineStudentRecords();

        Assert.That(result, Is.InstanceOf<OkObjectResult>());

        var okResult = (OkObjectResult)result;
        Assert.That(okResult.Value, Is.EqualTo("Discipline student records synchronized successfully."));

        _disciplineStudentRecordSyncLogicMock.Verify(x => x.SyncDisciplineStudentRecordsAsync(), Times.Once);
        VerifyNoOtherCalls();
    }

    [Test]
    public async Task SyncDisciplineStudentRecords_ShouldReturn500_WhenLogicThrows()
    {
        _disciplineStudentRecordSyncLogicMock
            .Setup(x => x.SyncDisciplineStudentRecordsAsync())
            .ThrowsAsync(new Exception("discipline student records test error"));

        var result = await _controller.SyncDisciplineStudentRecords();

        Assert.That(result, Is.InstanceOf<ObjectResult>());

        var objectResult = (ObjectResult)result;
        Assert.That(objectResult.StatusCode, Is.EqualTo(500));

        var json = JsonSerializer.Serialize(objectResult.Value);
        Assert.That(json, Does.Contain("Internal server error"));
        Assert.That(json, Does.Contain("discipline student records test error"));

        _disciplineStudentRecordSyncLogicMock.Verify(x => x.SyncDisciplineStudentRecordsAsync(), Times.Once);
        VerifyNoOtherCalls();
    }

    [Test]
    public async Task SyncStudentOrders_ShouldReturnOk_WhenLogicSucceeds()
    {
        _studentOrderSyncLogicMock
            .Setup(x => x.SyncStudentOrdersAsync())
            .Returns(Task.CompletedTask);

        var result = await _controller.SyncStudentOrders();

        Assert.That(result, Is.InstanceOf<OkObjectResult>());

        var okResult = (OkObjectResult)result;
        Assert.That(okResult.Value, Is.EqualTo("Student orders synchronized successfully."));

        _studentOrderSyncLogicMock.Verify(x => x.SyncStudentOrdersAsync(), Times.Once);
        VerifyNoOtherCalls();
    }

    [Test]
    public async Task SyncStudentOrders_ShouldReturn500_WhenLogicThrows()
    {
        _studentOrderSyncLogicMock
            .Setup(x => x.SyncStudentOrdersAsync())
            .ThrowsAsync(new Exception("student orders test error"));

        var result = await _controller.SyncStudentOrders();

        Assert.That(result, Is.InstanceOf<ObjectResult>());

        var objectResult = (ObjectResult)result;
        Assert.That(objectResult.StatusCode, Is.EqualTo(500));

        var json = JsonSerializer.Serialize(objectResult.Value);
        Assert.That(json, Does.Contain("Internal server error"));
        Assert.That(json, Does.Contain("student orders test error"));

        _studentOrderSyncLogicMock.Verify(x => x.SyncStudentOrdersAsync(), Times.Once);
        VerifyNoOtherCalls();
    }

    private void VerifyNoOtherCalls()
    {
        _academicPlanSyncLogicMock.VerifyNoOtherCalls();
        _studentGroupSyncLogicMock.VerifyNoOtherCalls();
        _studentSyncLogicMock.VerifyNoOtherCalls();
        _disciplineStudentRecordSyncLogicMock.VerifyNoOtherCalls();
        _studentOrderSyncLogicMock.VerifyNoOtherCalls();
    }
}


