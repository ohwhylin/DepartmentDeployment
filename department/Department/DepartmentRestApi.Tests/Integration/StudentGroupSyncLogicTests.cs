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
public class StudentGroupSyncLogicTests
{
    private Mock<IOneCApiService> _oneCApiServiceMock = null!;
    private StudentGroupSyncLogic _logic = null!;

    [SetUp]
    public void SetUp()
    {
        TestDatabaseHelper.RecreateDatabase();
        Environment.SetEnvironmentVariable("DB_CONNECTION_STRING", TestEnvironment.ConnectionString);

        _oneCApiServiceMock = new Mock<IOneCApiService>();

        _logic = new StudentGroupSyncLogic(
            _oneCApiServiceMock.Object,
            new StudentGroupStorage());
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

    [Test]
    public async Task SyncStudentGroupsAsync_ShouldInsertGroup_WhenEducationDirectionExists()
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

            db.SaveChanges();
        }

        _oneCApiServiceMock
            .Setup(x => x.GetStudentGroupsAsync())
            .ReturnsAsync(new List<StudentGroupOneCDto>
            {
                new StudentGroupOneCDto
                {
                    Id = 10,
                    EducationDirectionId = 1,
                    CuratorId = null,
                    GroupName = "ПИбд-41",
                    Course = AcademicCourse.Course_1
                }
            });

        await _logic.SyncStudentGroupsAsync();

        using var checkDb = new DepartmentDatabase();

        Assert.That(checkDb.StudentGroups.Count(), Is.EqualTo(1));

        var group = checkDb.StudentGroups.Single();
        Assert.That(group.Id, Is.EqualTo(10));
        Assert.That(group.EducationDirectionId, Is.EqualTo(1));
        Assert.That(group.CuratorId, Is.Null);
        Assert.That(group.GroupName, Is.EqualTo("ПИбд-41"));
        Assert.That(group.Course, Is.EqualTo(AcademicCourse.Course_1));
    }

    [Test]
    public async Task SyncStudentGroupsAsync_ShouldUpdateExistingGroup_WhenDataChanged()
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

            db.EducationDirections.Add(CreateEducationDirection(
                id: 2,
                cipher: "02.03.02",
                shortName: "ФИИТ",
                title: "Фундаментальная информатика",
                qualification: (EducationDirectionQualification)0,
                profile: "Анализ данных",
                description: "Тест 2"));

            db.StudentGroups.Add(CreateStudentGroup(
                id: 10,
                educationDirectionId: 1,
                curatorId: null,
                groupName: "Старая группа",
                course: AcademicCourse.Course_1));

            db.SaveChanges();
        }

        _oneCApiServiceMock
            .Setup(x => x.GetStudentGroupsAsync())
            .ReturnsAsync(new List<StudentGroupOneCDto>
            {
                new StudentGroupOneCDto
                {
                    Id = 10,
                    EducationDirectionId = 2,
                    CuratorId = null,
                    GroupName = "Новая группа",
                    Course = AcademicCourse.Course_2
                }
            });

        await _logic.SyncStudentGroupsAsync();

        using var checkDb = new DepartmentDatabase();

        var group = checkDb.StudentGroups.Single(x => x.Id == 10);

        Assert.That(group.EducationDirectionId, Is.EqualTo(2));
        Assert.That(group.GroupName, Is.EqualTo("Новая группа"));
        Assert.That(group.Course, Is.EqualTo(AcademicCourse.Course_2));
        Assert.That(group.CuratorId, Is.Null);
    }

    [Test]
    public async Task SyncStudentGroupsAsync_ShouldDeleteGroups_ThatAreMissingInOneC()
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

            db.SaveChanges();
        }

        _oneCApiServiceMock
            .Setup(x => x.GetStudentGroupsAsync())
            .ReturnsAsync(new List<StudentGroupOneCDto>
            {
                new StudentGroupOneCDto
                {
                    Id = 10,
                    EducationDirectionId = 1,
                    CuratorId = null,
                    GroupName = "ПИбд-41",
                    Course = AcademicCourse.Course_1
                }
            });

        await _logic.SyncStudentGroupsAsync();

        using var checkDb = new DepartmentDatabase();

        Assert.That(checkDb.StudentGroups.Count(), Is.EqualTo(1));
        Assert.That(checkDb.StudentGroups.Any(x => x.Id == 10), Is.True);
        Assert.That(checkDb.StudentGroups.Any(x => x.Id == 11), Is.False);
    }
}