using System.Net;
using System.Net.Http.Json;
using DepartmentContracts.BindingModels;
using DepartmentContracts.ViewModels;
using DepartmentDataModels.Enums;
using DepartmentDatabaseImplement;
using DepartmentDatabaseImplement.Models;
using DepartmentRestApi.Tests.Infrastructure;
using NUnit.Framework;

namespace DepartmentRestApi.Tests.Integration;

[TestFixture]
public class LecturersControllerTests
{
    private CustomWebApplicationFactory _factory = null!;
    private HttpClient _client = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _factory = new CustomWebApplicationFactory();
        _client = _factory.CreateClient();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _client.Dispose();
        _factory.Dispose();
    }

    [SetUp]
    public void SetUp()
    {
        TestDatabaseHelper.RecreateDatabase();
        Environment.SetEnvironmentVariable("DB_CONNECTION_STRING", TestEnvironment.ConnectionString);
    }

    private static LecturerStudyPost CreateStudyPost(int id, string title, int hours)
    {
        return LecturerStudyPost.Create(new LecturerStudyPostBindingModel
        {
            Id = id,
            StudyPostTitle = title,
            Hours = hours
        })!;
    }

    private static LecturerDepartmentPost CreateDepartmentPost(int id, string title, int order)
    {
        return LecturerDepartmentPost.Create(new LecturerDepartmentPostBindingModel
        {
            Id = id,
            DepartmentPostTitle = title,
            Order = order
        })!;
    }

    private static Lecturer CreateLecturer(
        int id,
        int? studyPostId,
        int departmentPostId,
        string firstName,
        string lastName,
        string patronymic)
    {
        return Lecturer.Create(new LecturerBindingModel
        {
            Id = id,
            LecturerStudyPostId = studyPostId,
            LecturerDepartmentPostId = departmentPostId,
            FirstName = firstName,
            LastName = lastName,
            Patronymic = patronymic,
            Abbreviation = $"{lastName} {firstName[0]}.{patronymic[0]}.",
            DateBirth = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            Address = "г. Ульяновск",
            Email = "lecturer@test.local",
            MobileNumber = "89990000000",
            HomeNumber = "8422000000",
            Rank = Rank.Отсуствует,
            Rank2 = 0,
            Description = "Тестовый преподаватель",
            OnlyForPrivate = false
        })!;
    }

    private static LecturerBindingModel CreateLecturerBindingModel(
        int id,
        int? studyPostId,
        int departmentPostId,
        string firstName,
        string lastName,
        string patronymic)
    {
        return new LecturerBindingModel
        {
            Id = id,
            LecturerStudyPostId = studyPostId,
            LecturerDepartmentPostId = departmentPostId,
            FirstName = firstName,
            LastName = lastName,
            Patronymic = patronymic,
            DateBirth = new DateTime(1991, 2, 3),
            Address = "г. Ульяновск",
            Email = "lecturer@test.local",
            MobileNumber = "89990000000",
            HomeNumber = "8422000000",
            Rank2 = 0,
            Description = "Тестовый преподаватель",
            OnlyForPrivate = false
        };
    }

    private static void SeedPosts()
    {
        using var db = new DepartmentDatabase();

        db.LecturerStudyPosts.AddRange(
            CreateStudyPost(1, "Ассистент", 900),
            CreateStudyPost(2, "Доцент", 850));

        db.LecturerDepartmentPosts.AddRange(
            CreateDepartmentPost(10, "Преподаватель кафедры", 1),
            CreateDepartmentPost(20, "Заведующий кафедрой", 2));

        db.SaveChanges();
    }

    [Test]
    public async Task GetLecturerList_ShouldReturnLecturersWithPostTitles()
    {
        SeedPosts();

        using (var db = new DepartmentDatabase())
        {
            db.Lecturers.Add(CreateLecturer(
                id: 100,
                studyPostId: 2,
                departmentPostId: 20,
                firstName: "Анна",
                lastName: "Иванова",
                patronymic: "Сергеевна"));

            db.SaveChanges();
        }

        var response = await _client.GetAsync("/api/Lecturers/GetLecturerList");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var result = await response.Content.ReadFromJsonAsync<List<LecturerViewModel>>();

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Count, Is.EqualTo(1));

        var lecturer = result[0];
        Assert.That(lecturer.Id, Is.EqualTo(100));
        Assert.That(lecturer.LecturerStudyPostId, Is.EqualTo(2));
        Assert.That(lecturer.LecturerDepartmentPostId, Is.EqualTo(20));
        Assert.That(lecturer.LecturerStudyPost, Is.EqualTo("Доцент"));
        Assert.That(lecturer.LecturerDepartmentPost, Is.EqualTo("Заведующий кафедрой"));
    }

    [Test]
    public async Task GetLecturer_ShouldReturnLecturerWithPostTitles_WhenLecturerExists()
    {
        SeedPosts();

        using (var db = new DepartmentDatabase())
        {
            db.Lecturers.Add(CreateLecturer(
                id: 100,
                studyPostId: 1,
                departmentPostId: 10,
                firstName: "Мария",
                lastName: "Петрова",
                patronymic: "Игоревна"));

            db.SaveChanges();
        }

        var response = await _client.GetAsync("/api/Lecturers/GetLecturer?id=100");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var result = await response.Content.ReadFromJsonAsync<LecturerViewModel>();

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Id, Is.EqualTo(100));
        Assert.That(result.LecturerStudyPost, Is.EqualTo("Ассистент"));
        Assert.That(result.LecturerDepartmentPost, Is.EqualTo("Преподаватель кафедры"));
    }

    [Test]
    public async Task GetLecturer_ShouldReturnNotFound_WhenLecturerDoesNotExist()
    {
        var response = await _client.GetAsync("/api/Lecturers/GetLecturer?id=999");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task LecturerCreate_ShouldCreateLecturerWithCorrectPosts()
    {
        SeedPosts();

        var model = CreateLecturerBindingModel(
            id: 100,
            studyPostId: 2,
            departmentPostId: 20,
            firstName: "Ольга",
            lastName: "Смирнова",
            patronymic: "Андреевна");

        var response = await _client.PostAsJsonAsync("/api/Lecturers/LecturerCreate", model);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var result = await response.Content.ReadFromJsonAsync<bool>();
        Assert.That(result, Is.True);

        using var db = new DepartmentDatabase();
        var lecturer = db.Lecturers.SingleOrDefault(x => x.Id == 100);

        Assert.That(lecturer, Is.Not.Null);
        Assert.That(lecturer!.LecturerStudyPostId, Is.EqualTo(2));
        Assert.That(lecturer.LecturerDepartmentPostId, Is.EqualTo(20));
        Assert.That(lecturer.FirstName, Is.EqualTo("Ольга"));
        Assert.That(lecturer.LastName, Is.EqualTo("Смирнова"));
        Assert.That(lecturer.Abbreviation, Is.EqualTo("Смирнова О.А."));
    }

    [Test]
    public async Task LecturerUpdate_ShouldUpdateLecturerAndPosts()
    {
        SeedPosts();

        using (var db = new DepartmentDatabase())
        {
            db.Lecturers.Add(CreateLecturer(
                id: 100,
                studyPostId: 1,
                departmentPostId: 10,
                firstName: "Анна",
                lastName: "Иванова",
                patronymic: "Сергеевна"));

            db.SaveChanges();
        }

        var model = CreateLecturerBindingModel(
            id: 100,
            studyPostId: 2,
            departmentPostId: 20,
            firstName: "Анна",
            lastName: "Соколова",
            patronymic: "Петровна");

        var updateResponse = await _client.PostAsJsonAsync("/api/Lecturers/LecturerUpdate", model);

        Assert.That(updateResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var updateResult = await updateResponse.Content.ReadFromJsonAsync<bool>();
        Assert.That(updateResult, Is.True);

        var getResponse = await _client.GetAsync("/api/Lecturers/GetLecturer?id=100");
        var lecturer = await getResponse.Content.ReadFromJsonAsync<LecturerViewModel>();

        Assert.That(lecturer, Is.Not.Null);
        Assert.That(lecturer!.LecturerStudyPostId, Is.EqualTo(2));
        Assert.That(lecturer.LecturerDepartmentPostId, Is.EqualTo(20));
        Assert.That(lecturer.LastName, Is.EqualTo("Соколова"));
        Assert.That(lecturer.Patronymic, Is.EqualTo("Петровна"));
        Assert.That(lecturer.LecturerStudyPost, Is.EqualTo("Доцент"));
        Assert.That(lecturer.LecturerDepartmentPost, Is.EqualTo("Заведующий кафедрой"));
        Assert.That(lecturer.Abbreviation, Is.EqualTo("Соколова А.П."));
    }

    [Test]
    public async Task LecturerDelete_ShouldDeleteLecturer()
    {
        SeedPosts();

        using (var db = new DepartmentDatabase())
        {
            db.Lecturers.Add(CreateLecturer(
                id: 100,
                studyPostId: 1,
                departmentPostId: 10,
                firstName: "Ирина",
                lastName: "Кузнецова",
                patronymic: "Владимировна"));

            db.SaveChanges();
        }

        var response = await _client.PostAsJsonAsync(
            "/api/Lecturers/LecturerDelete",
            new LecturerBindingModel { Id = 100 });

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var result = await response.Content.ReadFromJsonAsync<bool>();
        Assert.That(result, Is.True);

        using var checkDb = new DepartmentDatabase();
        Assert.That(checkDb.Lecturers.Any(x => x.Id == 100), Is.False);
    }

    [Test]
    public async Task LecturerDelete_ShouldReturnBadRequest_WhenIdIsInvalid()
    {
        var response = await _client.PostAsJsonAsync(
            "/api/Lecturers/LecturerDelete",
            new LecturerBindingModel { Id = 0 });

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }
}