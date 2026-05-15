using DepartmentBusinessLogic.BusinessLogics.Sync;
using DepartmentContracts.BindingModels;
using DepartmentContracts.BusinessLogicsContracts;
using DepartmentContracts.Dtos.OneC;
using DepartmentDataModels.Enums;
using DepartmentDatabaseImplement;
using DepartmentDatabaseImplement.Implements;
using DepartmentDatabaseImplement.Models;
using DepartmentRestApi.Tests.Infrastructure;
using Moq;
using NUnit.Framework;

namespace DepartmentRestApi.Tests.Integration;

[TestFixture]
public class StudentOrderSyncLogicTests
{
    private Mock<IOneCApiService> _oneCApiServiceMock = null!;
    private StudentOrderSyncLogic _logic = null!;

    [SetUp]
    public void SetUp()
    {
        TestDatabaseHelper.RecreateDatabase();
        Environment.SetEnvironmentVariable("DB_CONNECTION_STRING", TestEnvironment.ConnectionString);

        _oneCApiServiceMock = new Mock<IOneCApiService>();

        _logic = new StudentOrderSyncLogic(
            _oneCApiServiceMock.Object,
            new StudentOrderStorage(),
            new StudentOrderBlockStorage(),
            new StudentOrderBlockStudentStorage());
    }

    private static EducationDirection CreateEducationDirection(
        int id,
        string cipher,
        string shortName,
        string title,
        EducationDirectionQualification qualification,
        string profile,
        string description)
    {
        return EducationDirection.Create(new EducationDirectionBindingModel
        {
            Id = id,
            Cipher = cipher,
            ShortName = shortName,
            Title = title,
            Qualification = qualification,
            Profile = profile,
            Description = description
        })!;
    }

    private static StudentGroup CreateStudentGroup(
        int id,
        int educationDirectionId,
        int? curatorId,
        string groupName,
        AcademicCourse course)
    {
        return StudentGroup.Create(new StudentGroupBindingModel
        {
            Id = id,
            EducationDirectionId = educationDirectionId,
            CuratorId = curatorId,
            GroupName = groupName,
            Course = course
        })!;
    }

    private static Student CreateStudent(
        int id,
        int? studentGroupId,
        string numberOfBook,
        string firstName,
        string lastName,
        string patronymic,
        string email,
        StudentState studentState,
        string description,
        byte[] photo,
        bool isSteward)
    {
        return Student.Create(new StudentBindingModel
        {
            Id = id,
            StudentGroupId = studentGroupId,
            NumberOfBook = numberOfBook,
            FirstName = firstName,
            LastName = lastName,
            Patronymic = patronymic,
            Email = email,
            StudentState = studentState,
            Description = description,
            Photo = photo,
            IsSteward = isSteward
        })!;
    }

    private static StudentOrder CreateStudentOrder(
        int id,
        string orderNumber,
        StudentOrderType studentOrderType,
        DateTime orderDate)
    {
        return StudentOrder.Create(new StudentOrderBindingModel
        {
            Id = id,
            OrderNumber = orderNumber,
            StudentOrderType = studentOrderType,
            OrderDate = orderDate
        })!;
    }

    private static StudentOrderBlock CreateStudentOrderBlock(
        int id,
        int studentOrderId,
        int educationDirectionId,
        StudentOrderType studentOrderType)
    {
        return StudentOrderBlock.Create(new StudentOrderBlockBindingModel
        {
            Id = id,
            StudentOrderId = studentOrderId,
            EducationDirectionId = educationDirectionId,
            StudentOrderType = studentOrderType
        })!;
    }

    private static StudentOrderBlockStudent CreateStudentOrderBlockStudent(
        int id,
        int studentOrderBlockId,
        int studentId,
        int? studentGroupFromId,
        int? studentGroupToId)
    {
        return StudentOrderBlockStudent.Create(new StudentOrderBlockStudentBindingModel
        {
            Id = id,
            StudentOrderBlockId = studentOrderBlockId,
            StudentId = studentId,
            StudentGroupFromId = studentGroupFromId,
            StudentGroupToId = studentGroupToId
        })!;
    }

    [Test]
    public async Task SyncStudentOrdersAsync_ShouldInsertOrderBlockAndBlockStudent()
    {
        using (var db = new DepartmentDatabase())
        {
            db.EducationDirections.Add(CreateEducationDirection(
                id: 1,
                cipher: "09.03.04",
                shortName: "ПИ",
                title: "Программная инженерия",
                qualification: (EducationDirectionQualification)0,
                profile: "Разработка ПО",
                description: "Тест"));

            db.StudentGroups.Add(CreateStudentGroup(
                id: 10,
                educationDirectionId: 1,
                curatorId: null,
                groupName: "ПИбд-41",
                course: AcademicCourse.Course_1));

            db.StudentGroups.Add(CreateStudentGroup(
                id: 11,
                educationDirectionId: 1,
                curatorId: null,
                groupName: "ПИбд-42",
                course: AcademicCourse.Course_2));

            db.Students.Add(CreateStudent(
                id: 100,
                studentGroupId: 10,
                numberOfBook: "211004",
                firstName: "Иван",
                lastName: "Иванов",
                patronymic: "Иванович",
                email: "ivan@test.local",
                studentState: (StudentState)0,
                description: "",
                photo: Array.Empty<byte>(),
                isSteward: false));

            db.SaveChanges();
        }

        _oneCApiServiceMock
            .Setup(x => x.GetStudentOrdersAsync())
            .ReturnsAsync(new List<StudentOrderOneCDto>
            {
                new StudentOrderOneCDto
                {
                    Id = 1000,
                    OrderNumber = "123/к",
                    StudentOrderType = (StudentOrderType)0,
                    OrderDate = new DateTime(2024, 5, 1),
                    Blocks = new List<StudentOrderBlockOneCDto>
                    {
                        new StudentOrderBlockOneCDto
                        {
                            Id = 2000,
                            StudentOrderId = 1000,
                            EducationDirectionId = 1,
                            StudentOrderType = (StudentOrderType)0,
                            Students = new List<StudentOrderBlockStudentOneCDto>
                            {
                                new StudentOrderBlockStudentOneCDto
                                {
                                    Id = 3000,
                                    StudentOrderBlockId = 2000,
                                    StudentId = 100,
                                    StudentGroupFromId = 10,
                                    StudentGroupToId = 11
                                }
                            }
                        }
                    }
                }
            });

        await _logic.SyncStudentOrdersAsync();

        using var checkDb = new DepartmentDatabase();

        Assert.That(checkDb.StudentOrders.Count(), Is.EqualTo(1));
        Assert.That(checkDb.StudentOrderBlocks.Count(), Is.EqualTo(1));
        Assert.That(checkDb.StudentOrderBlockStudents.Count(), Is.EqualTo(1));

        var order = checkDb.StudentOrders.Single();
        var block = checkDb.StudentOrderBlocks.Single();
        var blockStudent = checkDb.StudentOrderBlockStudents.Single();

        Assert.That(order.Id, Is.EqualTo(1000));
        Assert.That(order.OrderNumber, Is.EqualTo("123/к"));
        Assert.That(order.OrderDate, Is.EqualTo(DateTime.SpecifyKind(new DateTime(2024, 5, 1), DateTimeKind.Utc)));

        Assert.That(block.Id, Is.EqualTo(2000));
        Assert.That(block.StudentOrderId, Is.EqualTo(1000));
        Assert.That(block.EducationDirectionId, Is.EqualTo(1));

        Assert.That(blockStudent.Id, Is.EqualTo(3000));
        Assert.That(blockStudent.StudentOrderBlockId, Is.EqualTo(2000));
        Assert.That(blockStudent.StudentId, Is.EqualTo(100));
        Assert.That(blockStudent.StudentGroupFromId, Is.EqualTo(10));
        Assert.That(blockStudent.StudentGroupToId, Is.EqualTo(11));
    }

    [Test]
    public async Task SyncStudentOrdersAsync_ShouldUpdateExistingOrderBlockAndBlockStudent()
    {
        using (var db = new DepartmentDatabase())
        {
            db.EducationDirections.Add(CreateEducationDirection(
                id: 1,
                cipher: "09.03.04",
                shortName: "ПИ",
                title: "Программная инженерия",
                qualification: (EducationDirectionQualification)0,
                profile: "Разработка ПО",
                description: "Тест"));

            db.StudentGroups.Add(CreateStudentGroup(
                id: 10,
                educationDirectionId: 1,
                curatorId: null,
                groupName: "ПИбд-41",
                course: AcademicCourse.Course_1));

            db.StudentGroups.Add(CreateStudentGroup(
                id: 11,
                educationDirectionId: 1,
                curatorId: null,
                groupName: "ПИбд-42",
                course: AcademicCourse.Course_2));

            db.Students.Add(CreateStudent(
                id: 100,
                studentGroupId: 10,
                numberOfBook: "211004",
                firstName: "Иван",
                lastName: "Иванов",
                patronymic: "Иванович",
                email: "ivan@test.local",
                studentState: (StudentState)0,
                description: "",
                photo: Array.Empty<byte>(),
                isSteward: false));

            db.StudentOrders.Add(CreateStudentOrder(
                id: 1000,
                orderNumber: "OLD",
                studentOrderType: (StudentOrderType)0,
                orderDate: DateTime.SpecifyKind(new DateTime(2024, 1, 1), DateTimeKind.Utc)));

            db.StudentOrderBlocks.Add(CreateStudentOrderBlock(
                id: 2000,
                studentOrderId: 1000,
                educationDirectionId: 1,
                studentOrderType: (StudentOrderType)0));

            db.StudentOrderBlockStudents.Add(CreateStudentOrderBlockStudent(
                id: 3000,
                studentOrderBlockId: 2000,
                studentId: 100,
                studentGroupFromId: 10,
                studentGroupToId: null));

            db.SaveChanges();
        }

        _oneCApiServiceMock
            .Setup(x => x.GetStudentOrdersAsync())
            .ReturnsAsync(new List<StudentOrderOneCDto>
            {
                new StudentOrderOneCDto
                {
                    Id = 1000,
                    OrderNumber = "NEW",
                    StudentOrderType = (StudentOrderType)1,
                    OrderDate = new DateTime(2024, 6, 1),
                    Blocks = new List<StudentOrderBlockOneCDto>
                    {
                        new StudentOrderBlockOneCDto
                        {
                            Id = 2000,
                            StudentOrderId = 1000,
                            EducationDirectionId = 1,
                            StudentOrderType = (StudentOrderType)1,
                            Students = new List<StudentOrderBlockStudentOneCDto>
                            {
                                new StudentOrderBlockStudentOneCDto
                                {
                                    Id = 3000,
                                    StudentOrderBlockId = 2000,
                                    StudentId = 100,
                                    StudentGroupFromId = 10,
                                    StudentGroupToId = 11
                                }
                            }
                        }
                    }
                }
            });

        await _logic.SyncStudentOrdersAsync();

        using var checkDb = new DepartmentDatabase();

        var order = checkDb.StudentOrders.Single(x => x.Id == 1000);
        var block = checkDb.StudentOrderBlocks.Single(x => x.Id == 2000);
        var blockStudent = checkDb.StudentOrderBlockStudents.Single(x => x.Id == 3000);

        Assert.That(order.OrderNumber, Is.EqualTo("NEW"));
        Assert.That(order.StudentOrderType, Is.EqualTo((StudentOrderType)1));
        Assert.That(order.OrderDate, Is.EqualTo(DateTime.SpecifyKind(new DateTime(2024, 6, 1), DateTimeKind.Utc)));

        Assert.That(block.StudentOrderType, Is.EqualTo((StudentOrderType)1));

        Assert.That(blockStudent.StudentGroupFromId, Is.EqualTo(10));
        Assert.That(blockStudent.StudentGroupToId, Is.EqualTo(11));
    }

    [Test]
    public async Task SyncStudentOrdersAsync_ShouldDeleteMissingOrdersBlocksAndBlockStudents()
    {
        using (var db = new DepartmentDatabase())
        {
            db.EducationDirections.Add(CreateEducationDirection(
                id: 1,
                cipher: "09.03.04",
                shortName: "ПИ",
                title: "Программная инженерия",
                qualification: (EducationDirectionQualification)0,
                profile: "Разработка ПО",
                description: "Тест"));

            db.StudentGroups.Add(CreateStudentGroup(
                id: 10,
                educationDirectionId: 1,
                curatorId: null,
                groupName: "ПИбд-41",
                course: AcademicCourse.Course_1));

            db.Students.Add(CreateStudent(
                id: 100,
                studentGroupId: 10,
                numberOfBook: "211004",
                firstName: "Иван",
                lastName: "Иванов",
                patronymic: "Иванович",
                email: "ivan@test.local",
                studentState: (StudentState)0,
                description: "",
                photo: Array.Empty<byte>(),
                isSteward: false));

            db.StudentOrders.Add(CreateStudentOrder(
                id: 1000,
                orderNumber: "123/к",
                studentOrderType: (StudentOrderType)0,
                orderDate: DateTime.SpecifyKind(new DateTime(2024, 5, 1), DateTimeKind.Utc)));

            db.StudentOrderBlocks.Add(CreateStudentOrderBlock(
                id: 2000,
                studentOrderId: 1000,
                educationDirectionId: 1,
                studentOrderType: (StudentOrderType)0));

            db.StudentOrderBlockStudents.Add(CreateStudentOrderBlockStudent(
                id: 3000,
                studentOrderBlockId: 2000,
                studentId: 100,
                studentGroupFromId: 10,
                studentGroupToId: null));

            db.SaveChanges();
        }

        _oneCApiServiceMock
            .Setup(x => x.GetStudentOrdersAsync())
            .ReturnsAsync(new List<StudentOrderOneCDto>());

        await _logic.SyncStudentOrdersAsync();

        using var checkDb = new DepartmentDatabase();

        Assert.That(checkDb.StudentOrders.Count(), Is.EqualTo(0));
        Assert.That(checkDb.StudentOrderBlocks.Count(), Is.EqualTo(0));
        Assert.That(checkDb.StudentOrderBlockStudents.Count(), Is.EqualTo(0));

        // Связанные сущности этой логикой не удаляются
        Assert.That(checkDb.EducationDirections.Count(), Is.EqualTo(1));
        Assert.That(checkDb.Students.Count(), Is.EqualTo(1));
        Assert.That(checkDb.StudentGroups.Count(), Is.EqualTo(1));
    }
}