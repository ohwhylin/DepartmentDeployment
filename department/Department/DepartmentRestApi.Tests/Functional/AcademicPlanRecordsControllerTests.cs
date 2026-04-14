using DepartmentContracts.BindingModels;
using DepartmentContracts.BusinessLogicsContracts;
using DepartmentContracts.SearchModels;
using DepartmentContracts.ViewModels;
using DepartmentRestApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace DepartmentRestApi.Tests.Functional;

[TestFixture]
public class AcademicPlanRecordsControllerTests
{
    private Mock<IAcademicPlanRecordLogic> _logicMock = null!;
    private AcademicPlanRecordsController _controller = null!;

    [SetUp]
    public void SetUp()
    {
        _logicMock = new Mock<IAcademicPlanRecordLogic>();

        _controller = new AcademicPlanRecordsController(
            Mock.Of<ILogger<AcademicPlanRecordsController>>(),
            _logicMock.Object);
    }

    [Test]
    public void GetAcademicPlanRecordList_ShouldReturnOk()
    {
        _logicMock
            .Setup(x => x.ReadList(null))
            .Returns(new List<AcademicPlanRecordViewModel> { new() });

        var result = _controller.GetAcademicPlanRecordList();

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public void GetAcademicPlanRecordList_ShouldReturn500_WhenLogicThrows()
    {
        _logicMock
            .Setup(x => x.ReadList(null))
            .Throws(new Exception("test"));

        var result = _controller.GetAcademicPlanRecordList();

        Assert.That(result, Is.InstanceOf<ObjectResult>());
        Assert.That(((ObjectResult)result).StatusCode, Is.EqualTo(500));
    }

    [Test]
    public void GetAcademicPlanRecord_ShouldReturnOk_WhenElementExists()
    {
        _logicMock
            .Setup(x => x.ReadElement(It.IsAny<AcademicPlanRecordSearchModel>()))
            .Returns(new AcademicPlanRecordViewModel());

        var result = _controller.GetAcademicPlanRecord(new AcademicPlanRecordSearchModel { Id = 1 });

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public void GetAcademicPlanRecord_ShouldReturnNotFound_WhenElementMissing()
    {
        _logicMock
            .Setup(x => x.ReadElement(It.IsAny<AcademicPlanRecordSearchModel>()))
            .Returns((AcademicPlanRecordViewModel?)null);

        var result = _controller.GetAcademicPlanRecord(new AcademicPlanRecordSearchModel { Id = 1 });

        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public void AcademicPlanRecordCreate_ShouldReturnBadRequest_WhenModelIsNull()
    {
        var result = _controller.AcademicPlanRecordCreate(null!);

        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public void AcademicPlanRecordCreate_ShouldReturnBadRequest_WhenModelStateInvalid()
    {
        _controller.ModelState.AddModelError("AcademicPlanId", "Required");

        var result = _controller.AcademicPlanRecordCreate(new AcademicPlanRecordBindingModel());

        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public void AcademicPlanRecordCreate_ShouldReturnOk_WhenModelIsValid()
    {
        _logicMock
            .Setup(x => x.Create(It.IsAny<AcademicPlanRecordBindingModel>()))
            .Returns(true);

        var result = _controller.AcademicPlanRecordCreate(new AcademicPlanRecordBindingModel
        {
            Id = 1,
            AcademicPlanId = 10,
            Index = "Б1.О.01",
            Name = "Математика",
            Semester = 1,
            Zet = 3,
            AcademicHours = 108
        });

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public void AcademicPlanRecordCreate_ShouldReturnConflict_WhenLogicThrowsInvalidOperationException()
    {
        _logicMock
            .Setup(x => x.Create(It.IsAny<AcademicPlanRecordBindingModel>()))
            .Throws(new InvalidOperationException("conflict"));

        var result = _controller.AcademicPlanRecordCreate(new AcademicPlanRecordBindingModel
        {
            AcademicPlanId = 10
        });

        Assert.That(result, Is.InstanceOf<ConflictObjectResult>());
    }

    [Test]
    public void AcademicPlanRecordUpdate_ShouldReturnOk_WhenModelIsValid()
    {
        _logicMock
            .Setup(x => x.Update(It.IsAny<AcademicPlanRecordBindingModel>()))
            .Returns(true);

        var result = _controller.AcademicPlanRecordUpdate(new AcademicPlanRecordBindingModel
        {
            Id = 1,
            AcademicPlanId = 10,
            Index = "NEW",
            Name = "Новая запись",
            Semester = 2,
            Zet = 4,
            AcademicHours = 144
        });

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public void AcademicPlanRecordDelete_ShouldReturnBadRequest_WhenIdInvalid()
    {
        var result = _controller.AcademicPlanRecordDelete(new AcademicPlanRecordBindingModel
        {
            Id = 0
        });

        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public void AcademicPlanRecordDelete_ShouldReturnOk_WhenDeleteSucceeded()
    {
        _logicMock
            .Setup(x => x.Delete(It.IsAny<AcademicPlanRecordBindingModel>()))
            .Returns(true);

        var result = _controller.AcademicPlanRecordDelete(new AcademicPlanRecordBindingModel
        {
            Id = 1
        });

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }
}