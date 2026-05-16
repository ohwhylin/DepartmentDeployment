using DepartmentDataModels.Enums;
using DepartmentOneCMockApi.Models;
using System.Linq;
using System.Xml.Linq;

namespace DepartmentOneCMockApi.Data
{
    public static class OneCTestData
    {
        public static List<StudentGroupMockModel> StudentGroups => new()
        {
            new()
            {
                Id = 1,
                EducationDirectionId = 1,
                CuratorId = 1,
                GroupName = "ПИбд-11",
                Course = AcademicCourse.Course_1
            },
            new()
            {
                Id = 2,
                EducationDirectionId = 1,
                CuratorId = 2,
                GroupName = "ПИбд-12",
                Course = AcademicCourse.Course_1
            },
            new()
            {
                Id = 3,
                EducationDirectionId = 1,
                CuratorId = 2,
                GroupName = "ПИбд-13",
                Course = AcademicCourse.Course_1
            },
            new()
            {
                Id = 4,
                EducationDirectionId = 1,
                CuratorId = 3,
                GroupName = "ПИбд-14",
                Course = AcademicCourse.Course_1
            },
            new()
            {
                Id = 5,
                EducationDirectionId = 1,
                CuratorId = 1,
                GroupName = "ПИбд-21",
                Course = AcademicCourse.Course_2
            },
            new()
            {
                Id = 6,
                EducationDirectionId = 1,
                CuratorId = 2,
                GroupName = "ПИбд-22",
                Course = AcademicCourse.Course_2
            },
            new()
            {
                Id = 7,
                EducationDirectionId = 1,
                CuratorId = 2,
                GroupName = "ПИбд-23",
                Course = AcademicCourse.Course_2
            },
            new()
            {
                Id = 8,
                EducationDirectionId = 1,
                CuratorId = 3,
                GroupName = "ПИбд-24",
                Course = AcademicCourse.Course_2
            },
            new()
            {
                Id = 9,
                EducationDirectionId = 1,
                CuratorId = 1,
                GroupName = "ПИбд-31",
                Course = AcademicCourse.Course_3
            },
            new()
            {
                Id = 10,
                EducationDirectionId = 1,
                CuratorId = 2,
                GroupName = "ПИбд-32",
                Course = AcademicCourse.Course_3
            },
            new()
            {
                Id = 11,
                EducationDirectionId = 1,
                CuratorId = 2,
                GroupName = "ПИбд-33",
                Course = AcademicCourse.Course_3
            },
            new()
            {
                Id = 12,
                EducationDirectionId = 1,
                CuratorId = 1,
                GroupName = "ПИбд-41",
                Course = AcademicCourse.Course_4
            },
            new()
            {
                Id = 13,
                EducationDirectionId = 1,
                CuratorId = 2,
                GroupName = "ПИбд-42",
                Course = AcademicCourse.Course_4
            },
            new()
            {
                Id = 14,
                EducationDirectionId = 1,
                CuratorId = 2,
                GroupName = "ПИбд-43",
                Course = AcademicCourse.Course_4
            },
            new()
            {
                Id = 15,
                EducationDirectionId = 2,
                CuratorId = 2,
                GroupName = "ИСЭбд-11",
                Course = AcademicCourse.Course_1
            },
            new()
            {
                Id = 16,
                EducationDirectionId = 2,
                CuratorId = 1,
                GroupName = "ИСЭбд-12",
                Course = AcademicCourse.Course_1
            },
            new()
            {
                Id = 17,
                EducationDirectionId = 2,
                CuratorId = 2,
                GroupName = "ИСЭбд-21",
                Course = AcademicCourse.Course_2
            },
            new()
            {
                Id = 18,
                EducationDirectionId = 2,
                CuratorId = 1,
                GroupName = "ИСЭбд-22",
                Course = AcademicCourse.Course_2
            },
            new()
            {
                Id = 19,
                EducationDirectionId = 2,
                CuratorId = 2,
                GroupName = "ИСЭбд-31",
                Course = AcademicCourse.Course_3
            },
            new()
            {
                Id = 20,
                EducationDirectionId = 2,
                CuratorId = 1,
                GroupName = "ИСЭбд-41",
                Course = AcademicCourse.Course_4
            },
            new()
            {
                Id = 21,
                EducationDirectionId = 3,
                CuratorId = 2,
                GroupName = "БИмд-11",
                Course = AcademicCourse.Course_1
            },
            new()
            {
                Id = 22,
                EducationDirectionId = 3,
                CuratorId = 2,
                GroupName = "БИмд-21",
                Course = AcademicCourse.Course_2
            },
            new()
            {
                Id = 23,
                EducationDirectionId = 4,
                CuratorId = 2,
                GroupName = "ИИПАмд-11",
                Course = AcademicCourse.Course_1
            },
            new()
            {
                Id = 24,
                EducationDirectionId = 4,
                CuratorId = 2,
                GroupName = "ИИПАмд-21",
                Course = AcademicCourse.Course_2
            },
            new()
            {
                Id = 25,
                EducationDirectionId = 5,
                CuratorId = 2,
                GroupName = "ИИБАмд-11",
                Course = AcademicCourse.Course_1
            },
            new()
            {
                Id = 26,
                EducationDirectionId = 5,
                CuratorId = 2,
                GroupName = "ИИБАмд-21",
                Course = AcademicCourse.Course_2
            },
        };

        private static readonly Dictionary<int, int> StudentCountByGroupId = new()
        {
            { 1, 30 },
            { 2, 29 },
            { 3, 28 },
            { 4, 19 },
            { 5, 30 },
            { 6, 30 },
            { 7, 29 },
            { 8, 19 },
            { 9, 32 },
            { 10, 32 },
            { 11, 31 },
            { 12, 32 },
            { 13, 31 },
            { 14, 29 },
            { 15, 27 },
            { 16, 25 },
            { 17, 31 },
            { 18, 28 },
            { 19, 32 },
            { 20, 25 },
            { 21, 4 },
            { 22, 4 },
            { 23, 24 },
            { 24, 29 },
            { 25, 15 },
            { 26, 27 }
        };

        public static List<StudentMockModel> Students => GenerateStudents();

        private static List<StudentMockModel> GenerateStudents()
        {
            var firstNames = new[]
            {
                "Алина", "Мария", "Екатерина", "Анна", "Дарья", "Виктория", "Полина", "София",
                "Илья", "Артём", "Дмитрий", "Алексей", "Максим", "Кирилл", "Никита", "Егор"
            };

            var lastNames = new[]
            {
                "Иванов", "Петров", "Сидоров", "Смирнов", "Кузнецов", "Попов", "Васильев", "Новиков",
                "Фёдоров", "Морозов", "Волков", "Соколов", "Лебедев", "Козлов", "Степанов", "Павлов"
            };

            var patronymics = new[]
            {
                "Иванович", "Петрович", "Алексеевич", "Дмитриевич", "Сергеевич", "Андреевич",
                "Ивановна", "Петровна", "Алексеевна", "Дмитриевна", "Сергеевна", "Андреевна"
            };

            var students = new List<StudentMockModel>();
            var studentId = 1;
            var bookNumber = 240001;

            foreach (var group in StudentGroups.OrderBy(g => g.Id))
            {
                var count = StudentCountByGroupId.TryGetValue(group.Id, out var value)
                    ? value
                    : 0;

                for (var i = 0; i < count; i++)
                {
                    var firstName = firstNames[(studentId - 1) % firstNames.Length];
                    var lastName = lastNames[(studentId - 1) % lastNames.Length];
                    var patronymic = patronymics[(studentId - 1) % patronymics.Length];

                    students.Add(new StudentMockModel
                    {
                        Id = studentId,
                        StudentGroupId = group.Id,
                        NumberOfBook = bookNumber.ToString(),
                        FirstName = firstName,
                        LastName = lastName,
                        Patronymic = patronymic,
                        Email = $"student{studentId}@university.ru",
                        StudentState = StudentState.Учится,
                        Description = i == 0 ? "Староста группы" : string.Empty,
                        IsSteward = i == 0
                    });

                    studentId++;
                    bookNumber++;
                }
            }

            return students;
        }


        private static (int blockId, string blockTitle, string blueAsteriskName, bool useForGrouping, int blockOrder) GetBlockInfo(PlanSeed seed)
        {
            if (seed.Index.StartsWith("Б3"))
            {
                return (3, "Государственная итоговая аттестация", "", false, 3);
            }

            if (seed.Index.StartsWith("Б2"))
            {
                return (2, "Практики", "", false, 2);
            }

            return (1, "Дисциплины (модули)", "", true, 1);
        }

        private sealed class PlanSeed
        {
            public string Index { get; set; } = "";
            public string Name { get; set; } = "";
            public int Semester { get; set; }
            public int Zet { get; set; }
            public int AcademicHours { get; set; }

            public int? Exam { get; set; }
            public int? Pass { get; set; }
            public int? GradedPass { get; set; }
            public int? CourseWork { get; set; }
            public int? CourseProject { get; set; }
            public int? Rgr { get; set; }

            public int Lectures { get; set; }
            public int LaboratoryHours { get; set; }
            public int PracticalHours { get; set; }

            public bool IsSpecialBlock { get; set; }

            public PlanSeed Clone()
            {
                return new PlanSeed
                {
                    Index = Index,
                    Name = Name,
                    Semester = Semester,
                    Zet = Zet,
                    AcademicHours = AcademicHours,
                    Exam = Exam,
                    Pass = Pass,
                    GradedPass = GradedPass,
                    CourseWork = CourseWork,
                    CourseProject = CourseProject,
                    Rgr = Rgr,
                    Lectures = Lectures,
                    LaboratoryHours = LaboratoryHours,
                    PracticalHours = PracticalHours,
                    IsSpecialBlock = IsSpecialBlock
                };
            }
        }

        private static int _recordId = 1;
        private static int _disciplineId = 1;

        private static PlanSeed D(
            string index,
            string name,
            int semester,
            int zet,
            int lectures,
            int laboratoryHours,
            int practicalHours,
            int? exam = null,
            int? pass = null,
            int? gradedPass = null,
            int? courseWork = null,
            int? courseProject = null,
            int? rgr = null)
        {
            return new PlanSeed
            {
                Index = index,
                Name = name,
                Semester = semester,
                Zet = zet,
                AcademicHours = lectures + laboratoryHours + practicalHours,
                Exam = exam,
                Pass = pass,
                GradedPass = gradedPass,
                CourseWork = courseWork,
                CourseProject = courseProject,
                Rgr = rgr,
                Lectures = lectures,
                LaboratoryHours = laboratoryHours,
                PracticalHours = practicalHours,
                IsSpecialBlock = false
            };
        }

        private static PlanSeed P(
            string index,
            string name,
            int semester,
            int zet = 3,
            int academicHours = 108)
        {
            return new PlanSeed
            {
                Index = index,
                Name = name,
                Semester = semester,
                Zet = zet,
                AcademicHours = academicHours,
                IsSpecialBlock = true
            };
        }

        private static List<PlanSeed> Copy(List<PlanSeed> source)
        {
            return source.Select(x => x.Clone()).ToList();
        }

        private static void Move(List<PlanSeed> plan, string index, int newSemester)
        {
            var item = plan.First(x => x.Index == index);
            item.Semester = newSemester;
        }

        private static void SetHours(List<PlanSeed> plan, string index, int lectures, int laboratoryHours, int practicalHours)
        {
            var item = plan.First(x => x.Index == index);
            item.Lectures = lectures;
            item.LaboratoryHours = laboratoryHours;
            item.PracticalHours = practicalHours;
            item.AcademicHours = lectures + laboratoryHours + practicalHours;
        }

        private static void Remove(List<PlanSeed> plan, string index)
        {
            var item = plan.FirstOrDefault(x => x.Index == index);
            if (item != null)
                plan.Remove(item);
        }

        private static void Add(List<PlanSeed> plan, PlanSeed item)
        {
            plan.Add(item);
        }

        private static AcademicPlanMockModel BuildPlan(
    int planId,
    int educationDirectionId,
    string year,
    List<PlanSeed> seeds)
        {
            var records = new List<AcademicPlanRecordMockModel>();

            foreach (var seed in seeds)
            {
                var block = GetBlockInfo(seed);

                records.Add(new AcademicPlanRecordMockModel
                {
                    Id = _recordId++,
                    AcademicPlanId = planId,
                    DisciplineId = _disciplineId++,

                    DisciplineBlockId = block.blockId,
                    DisciplineBlockTitle = block.blockTitle,
                    DisciplineBlockBlueAsteriskName = block.blueAsteriskName,
                    DisciplineBlockUseForGrouping = block.useForGrouping,
                    DisciplineBlockOrder = block.blockOrder,

                    DisciplineShortName = seed.Name,
                    DisciplineDescription = seed.Name,

                    HasExam = seed.Exam.HasValue,
                    HasCredit = seed.Pass.HasValue || seed.GradedPass.HasValue,
                    HasCourseWork = seed.CourseWork.HasValue,
                    HasCourseProject = seed.CourseProject.HasValue,

                    Index = seed.Index,
                    Name = seed.Name,
                    Semester = seed.Semester,
                    Zet = seed.Zet,
                    AcademicHours = seed.AcademicHours,

                    Exam = seed.Exam,
                    Pass = seed.Pass,
                    GradedPass = seed.GradedPass,
                    CourseWork = seed.CourseWork,
                    CourseProject = seed.CourseProject,
                    Rgr = seed.Rgr,

                    Lectures = seed.IsSpecialBlock ? 0 : seed.Lectures,
                    LaboratoryHours = seed.IsSpecialBlock ? 0 : seed.LaboratoryHours,
                    PracticalHours = seed.IsSpecialBlock ? 0 : seed.PracticalHours
                });
            }

            return new AcademicPlanMockModel
            {
                Id = planId,
                EducationDirectionId = educationDirectionId,
                EducationForm = EducationForm.Очная,
                AcademicCourses = AcademicCourse.Course_1,
                Year = year,
                AcademicPlanRecords = records
            };
        }

        //
        // 09.03.04 — EducationDirectionId = 1
        //

        private static List<PlanSeed> Base090304()
        {
            return new List<PlanSeed>
            {
                D("Б1.О.13", "Введение в программную инженерию", 1, 2, 16, 0, 16, pass: 1),
                D("Б1.О.22", "Информационные системы и технологии", 1, 2, 16, 32, 0, exam: 1),
                D("Б1.О.23", "Теоретические основы информатики", 1, 2, 16, 0, 16, pass: 1, rgr: 1),

                D("Б1.О.14", "Организация ЭВМ и системы", 2, 2, 16, 32, 0, exam: 1),
                P("Б2.О.01(У)", "Ознакомительная практика", 2, 3, 108),

                D("Б1.О.15", "Базы данных", 3, 2, 16, 32, 0, exam: 1, courseProject: 1),
                D("Б1.О.16", "Системы управления базами данных", 3, 2, 16, 32, 0, pass: 1),

                D("Б1.О.20", "Методы моделирования", 4, 2, 16, 0, 16, pass: 1),
                D("Б1.О.21", "Технологии программирования", 4, 3, 16, 64, 0, exam: 1, courseWork: 1),

                D("Б1.О.33", "Алгоритмы и структуры данных", 5, 2, 16, 32, 0, exam: 1),
                D("Б1.В.01", "Проектирование и архитектура программных систем", 5, 2, 16, 32, 0, pass: 1, courseProject: 1),
                D("Б1.В.03", "Методы искусственного интеллекта", 5, 2, 16, 32, 0, pass: 1),

                D("Б1.В.05", "Интернет-программирование", 6, 2, 16, 32, 0, exam: 1),
                D("Б1.В.07", "Программирование на Java", 6, 2, 16, 32, 0, pass: 1),

                D("Б1.В.12", "Тестирование программного обеспечения", 7, 2, 16, 32, 0, exam: 1),
                D("Б1.В.13", "Конструирование программного обеспечения", 7, 2, 16, 32, 0, exam: 1, courseWork: 1),
                P("Б2.О.02(П)", "Научно-исследовательская работа", 7, 3, 108),

                P("Б1.О.24", "Проектный практикум", 8, 3, 108),
                P("Б2.О.03(П)", "Технологическая (проектно-технологическая) практика", 8, 3, 108),
                P("Б2.В.01(П)", "Преддипломная практика", 8, 3, 108),
                P("Б3.01", "Подготовка к сдаче и сдача государственного экзамена", 8, 3, 108),
                P("Б3.02", "Выполнение и защита выпускной квалификационной работы", 8, 3, 108)
            };
        }

        private static List<PlanSeed> Plan090304_2022_2026()
        {
            return Copy(Base090304());
        }

        private static List<PlanSeed> Plan090304_2023_2027()
        {
            var plan = Copy(Base090304());

            Move(plan, "Б1.В.05", 5);
            Move(plan, "Б1.В.03", 6);

            SetHours(plan, "Б1.О.22", 16, 64, 0);
            SetHours(plan, "Б1.О.21", 16, 32, 0);
            SetHours(plan, "Б1.В.07", 16, 64, 0);

            return plan;
        }

        private static List<PlanSeed> Plan090304_2024_2028()
        {
            var plan = Copy(Base090304());

            Move(plan, "Б1.О.20", 3);
            Move(plan, "Б1.О.16", 4);
            Move(plan, "Б1.В.12", 6);
            Move(plan, "Б1.В.07", 7);

            SetHours(plan, "Б1.О.15", 16, 64, 0);
            SetHours(plan, "Б1.В.12", 16, 32, 0);
            SetHours(plan, "Б1.В.13", 16, 64, 0);

            return plan;
        }

        private static List<PlanSeed> Plan090304_2025_2029()
        {
            var plan = Copy(Base090304());

            Remove(plan, "Б1.В.07");
            Add(plan, D("Б1.В.14", "Разработка веб-приложений", 6, 2, 16, 32, 0, exam: 1));

            Move(plan, "Б1.В.03", 6);
            Move(plan, "Б1.В.05", 5);

            SetHours(plan, "Б1.О.14", 16, 64, 0);
            SetHours(plan, "Б1.В.01", 16, 64, 0);
            SetHours(plan, "Б1.В.03", 16, 32, 0);

            return plan;
        }

        private static List<PlanSeed> Plan090304_2026_2030()
        {
            var plan = Copy(Base090304());

            Move(plan, "Б1.В.01", 4);
            Move(plan, "Б1.О.33", 6);
            Move(plan, "Б1.В.05", 5);
            Move(plan, "Б1.В.03", 6);

            SetHours(plan, "Б1.О.33", 16, 64, 0);
            SetHours(plan, "Б1.О.20", 16, 0, 16);
            SetHours(plan, "Б1.В.13", 16, 64, 0);

            return plan;
        }

        private static List<AcademicPlanMockModel> Build090304Plans()
        {
            return new List<AcademicPlanMockModel>
            {
                BuildPlan(1, 1, "2022-2026", Plan090304_2022_2026()),
                BuildPlan(2, 1, "2023-2027", Plan090304_2023_2027()),
                BuildPlan(3, 1, "2024-2028", Plan090304_2024_2028()),
                BuildPlan(4, 1, "2025-2029", Plan090304_2025_2029()),
                BuildPlan(5, 1, "2026-2030", Plan090304_2026_2030())
            };
        }

        //
        // 09.03.03 — EducationDirectionId = 2
        //

        private static List<PlanSeed> Base090303()
        {
            return new List<PlanSeed>
            {
                D("Б1.О.01", "Программирование", 1, 2, 16, 32, 0, exam: 1),
                D("Б1.О.02", "Основы алгоритмизации и программирования", 1, 2, 16, 0, 16, pass: 1),
                D("Б1.О.03", "Основы информационных технологий", 1, 2, 16, 32, 0, pass: 1),

                D("Б1.О.16", "Основы прикладной информатики", 2, 2, 16, 0, 16, pass: 1, rgr: 1),
                D("Б1.О.17", "Организация вычислительных машин и систем", 2, 2, 16, 32, 0, exam: 1),
                P("Б2.О.01(У)", "Ознакомительная практика", 2, 3, 108),

                D("Б1.О.18", "Базы данных", 3, 2, 16, 32, 0, exam: 1, courseProject: 1),
                D("Б1.О.19", "Системы управления базами данных", 3, 2, 16, 32, 0, pass: 1),
                D("Б1.О.28", "Алгоритмы и структуры данных", 3, 2, 16, 32, 0, exam: 1),

                D("Б1.О.22", "Методы моделирования", 4, 2, 16, 0, 16, pass: 1, courseWork: 1),
                D("Б1.О.23", "Операционные системы", 4, 2, 16, 32, 0, exam: 1),
                D("Б1.О.24", "Проектирование информационных систем", 4, 2, 16, 32, 0, pass: 1),

                D("Б1.В.01", "Интернет-программирование", 5, 2, 16, 32, 0, exam: 1),
                D("Б1.В.02", "Построение информационных систем", 5, 2, 16, 32, 0, pass: 1),
                D("Б1.В.03", "Методы искусственного интеллекта", 5, 2, 16, 32, 0, pass: 1),

                D("Б1.В.07", "Экспертные системы", 6, 2, 16, 32, 0, exam: 1),
                D("Б1.В.08", "Сетевые технологии в экономике", 6, 2, 16, 32, 0, pass: 1),

                D("Б1.В.ДВ.03.01", "Теория и практика экономических информационных систем", 7, 2, 16, 32, 0, pass: 1),
                D("Б1.В.ДВ.03.02", "Сервис-ориентированное программирование", 7, 2, 16, 32, 0, exam: 1),
                P("Б2.О.02(П)", "Научно-исследовательская работа", 7, 3, 108),

                P("Б2.О.03(П)", "Технологическая (проектно-технологическая) практика", 8, 3, 108),
                P("Б2.В.01(П)", "Преддипломная практика", 8, 3, 108),
                P("Б3.01", "Подготовка к сдаче и сдача государственного экзамена", 8, 3, 108),
                P("Б3.02", "Выполнение и защита выпускной квалификационной работы", 8, 3, 108)
            };
        }

        private static List<PlanSeed> Plan090303_2022_2026()
        {
            return Copy(Base090303());
        }

        private static List<PlanSeed> Plan090303_2023_2027()
        {
            var plan = Copy(Base090303());

            Move(plan, "Б1.В.03", 6);
            Move(plan, "Б1.В.08", 5);

            SetHours(plan, "Б1.О.17", 16, 64, 0);
            SetHours(plan, "Б1.В.01", 16, 64, 0);

            return plan;
        }

        private static List<PlanSeed> Plan090303_2024_2028()
        {
            var plan = Copy(Base090303());

            Move(plan, "Б1.О.22", 3);
            Move(plan, "Б1.О.19", 4);

            SetHours(plan, "Б1.О.18", 16, 64, 0);
            SetHours(plan, "Б1.В.07", 16, 64, 0);

            return plan;
        }

        private static List<PlanSeed> Plan090303_2025_2029()
        {
            var plan = Copy(Base090303());

            Move(plan, "Б1.В.01", 6);
            Move(plan, "Б1.В.07", 5);

            SetHours(plan, "Б1.О.24", 16, 64, 0);
            SetHours(plan, "Б1.В.03", 16, 64, 0);

            return plan;
        }

        private static List<PlanSeed> Plan090303_2026_2030()
        {
            var plan = Copy(Base090303());

            Move(plan, "Б1.О.28", 4);
            Move(plan, "Б1.О.22", 5);

            SetHours(plan, "Б1.О.16", 16, 0, 16);
            SetHours(plan, "Б1.В.ДВ.03.02", 16, 64, 0);

            return plan;
        }

        private static List<AcademicPlanMockModel> Build090303Plans()
        {
            return new List<AcademicPlanMockModel>
            {
                BuildPlan(6, 2, "2022-2026", Plan090303_2022_2026()),
                BuildPlan(7, 2, "2023-2027", Plan090303_2023_2027()),
                BuildPlan(8, 2, "2024-2028", Plan090303_2024_2028()),
                BuildPlan(9, 2, "2025-2029", Plan090303_2025_2029()),
                BuildPlan(10, 2, "2026-2030", Plan090303_2026_2030())
            };
        }

        //
        // 09.04.04 — EducationDirectionId = 4
        //

        private static List<PlanSeed> Base090404()
        {
            return new List<PlanSeed>
            {
                D("Б1.О.04", "Управление проектами в области искусственного интеллекта", 1, 2, 16, 32, 0, exam: 1),
                D("Б1.О.05", "Методы анализа данных в предиктивной аналитике", 1, 2, 16, 32, 0, pass: 1),

                D("Б1.О.08", "Методы интеллектуального анализа естественного языка", 2, 2, 16, 32, 0, exam: 1),
                D("Б1.О.09", "Методы глубокого обучения и трансформеры", 2, 2, 16, 64, 0, pass: 1),
                P("Б2.О.01(У)", "Ознакомительная практика", 2, 3, 108),

                D("Б1.В.01", "Технологии обработки и анализа больших данных в предиктивной аналитике", 3, 2, 16, 64, 0, exam: 1),
                D("Б1.В.03", "Методы искусственного интеллекта в предиктивной аналитике", 3, 2, 16, 32, 0, pass: 1),

                D("Б1.В.ДВ.01.01", "Проектирование интеллектуальных систем", 4, 2, 16, 64, 0, pass: 1),
                P("Б2.О.02(П)", "Научно-исследовательская работа", 4, 3, 108),
                P("Б2.В.01(П)", "Преддипломная практика", 4, 3, 108),
                P("Б3.02", "Выполнение и защита выпускной квалификационной работы", 4, 3, 108)
            };
        }

        private static List<PlanSeed> Plan090404_2022_2024()
        {
            return Copy(Base090404());
        }

        private static List<PlanSeed> Plan090404_2023_2025()
        {
            var plan = Copy(Base090404());

            Move(plan, "Б1.О.05", 2);
            Move(plan, "Б1.О.08", 1);

            SetHours(plan, "Б1.О.09", 16, 32, 0);
            SetHours(plan, "Б1.В.01", 16, 32, 0);

            return plan;
        }

        private static List<PlanSeed> Plan090404_2024_2026()
        {
            var plan = Copy(Base090404());

            Move(plan, "Б1.В.03", 4);
            Remove(plan, "Б1.В.ДВ.01.01");
            Add(plan, D("Б1.В.02", "Анализ многомерных данных", 3, 2, 16, 32, 0, pass: 1));
            Add(plan, D("Б1.В.ДВ.02.01", "Интеллектуальные САПР", 4, 2, 16, 64, 0, pass: 1));

            return plan;
        }

        private static List<PlanSeed> Plan090404_2025_2027()
        {
            var plan = Copy(Base090404());

            Remove(plan, "Б1.В.ДВ.01.01");
            Add(plan, D("Б1.В.ДВ.02.02", "Математическое моделирование информационных систем", 4, 2, 16, 64, 0, pass: 1));

            SetHours(plan, "Б1.О.05", 16, 64, 0);
            SetHours(plan, "Б1.В.03", 16, 64, 0);

            return plan;
        }

        private static List<PlanSeed> Plan090404_2026_2028()
        {
            var plan = Copy(Base090404());

            Move(plan, "Б1.В.01", 2);
            Move(plan, "Б1.О.09", 3);

            SetHours(plan, "Б1.В.01", 16, 64, 0);
            SetHours(plan, "Б1.О.08", 16, 32, 0);

            return plan;
        }

        private static List<AcademicPlanMockModel> Build090404Plans()
        {
            return new List<AcademicPlanMockModel>
            {
                BuildPlan(11, 4, "2022-2024", Plan090404_2022_2024()),
                BuildPlan(12, 4, "2023-2025", Plan090404_2023_2025()),
                BuildPlan(13, 4, "2024-2026", Plan090404_2024_2026()),
                BuildPlan(14, 4, "2025-2027", Plan090404_2025_2027()),
                BuildPlan(15, 4, "2026-2028", Plan090404_2026_2028())
            };
        }

        //
        // 09.04.03 — EducationDirectionId = 5
        //

        private static List<PlanSeed> Base090403()
        {
            return new List<PlanSeed>
            {
                D("Б1.О.05", "Методы анализа данных в бизнес-аналитике", 1, 2, 16, 32, 0, exam: 1),
                D("Б1.О.08", "Методы интеллектуального анализа естественного языка", 1, 2, 16, 32, 0, pass: 1),

                D("Б1.О.09", "Методы глубокого обучения в бизнес-аналитике", 2, 2, 16, 64, 0, exam: 1),
                P("Б2.О.01(У)", "Ознакомительная практика", 2, 3, 108),

                D("Б1.В.01", "Системы и технологии Web-аналитики", 3, 2, 16, 64, 0, pass: 1),
                D("Б1.В.03", "Методы искусственного интеллекта в бизнес-аналитике", 3, 2, 16, 32, 0, exam: 1),

                D("Б1.В.ДВ.01.01", "Интеллектуальные информационно-аналитические системы", 4, 2, 16, 64, 0, pass: 1),
                P("Б2.О.02(П)", "Научно-исследовательская работа", 4, 3, 108),
                P("Б2.В.01(П)", "Преддипломная практика", 4, 3, 108),
                P("Б3.02", "Выполнение и защита выпускной квалификационной работы", 4, 3, 108)
            };
        }

        private static List<PlanSeed> Plan090403_2022_2024()
        {
            return Copy(Base090403());
        }

        private static List<PlanSeed> Plan090403_2023_2025()
        {
            var plan = Copy(Base090403());

            Add(plan, D("Б1.О.04", "Управление проектами в области искусственного интеллекта", 1, 2, 16, 32, 0, pass: 1));
            Move(plan, "Б1.О.08", 2);

            return plan;
        }

        private static List<PlanSeed> Plan090403_2024_2026()
        {
            var plan = Copy(Base090403());

            Remove(plan, "Б1.В.ДВ.01.01");
            Add(plan, D("Б1.В.ДВ.01.02", "Обработка больших данных в бизнес-аналитике", 4, 2, 16, 64, 0, pass: 1));

            SetHours(plan, "Б1.О.09", 16, 32, 0);
            SetHours(plan, "Б1.В.01", 16, 32, 0);

            return plan;
        }

        private static List<PlanSeed> Plan090403_2025_2027()
        {
            var plan = Copy(Base090403());

            Move(plan, "Б1.В.03", 4);
            Add(plan, D("Б1.В.ДВ.02.02", "Интеллектуальные информационные системы на основе хранилищ данных", 3, 2, 16, 64, 0, pass: 1));

            return plan;
        }

        private static List<PlanSeed> Plan090403_2026_2028()
        {
            var plan = Copy(Base090403());

            Move(plan, "Б1.О.05", 2);
            Remove(plan, "Б1.В.ДВ.01.01");
            Add(plan, D("Б1.В.ДВ.02.01", "Интеллектуальные информационные системы", 4, 2, 16, 64, 0, pass: 1));

            SetHours(plan, "Б1.О.05", 16, 64, 0);
            SetHours(plan, "Б1.В.03", 16, 64, 0);

            return plan;
        }

        private static List<AcademicPlanMockModel> Build090403Plans()
        {
            return new List<AcademicPlanMockModel>
            {
                BuildPlan(16, 5, "2022-2024", Plan090403_2022_2024()),
                BuildPlan(17, 5, "2023-2025", Plan090403_2023_2025()),
                BuildPlan(18, 5, "2024-2026", Plan090403_2024_2026()),
                BuildPlan(19, 5, "2025-2027", Plan090403_2025_2027()),
                BuildPlan(20, 5, "2026-2028", Plan090403_2026_2028())
            };
        }

        private static List<AcademicPlanMockModel> BuildAllAcademicPlans()
        {
            _recordId = 1;
            _disciplineId = 1;

            var result = new List<AcademicPlanMockModel>();
            result.AddRange(Build090304Plans());
            result.AddRange(Build090303Plans());
            result.AddRange(Build090404Plans());
            result.AddRange(Build090403Plans());

            return result;
        }

        public static List<AcademicPlanMockModel> AcademicPlans => BuildAllAcademicPlans();

        public static List<DisciplineStudentRecordMockModel> DisciplineStudentRecords => GenerateDisciplineStudentRecords();

        private static List<DisciplineStudentRecordMockModel> GenerateDisciplineStudentRecords()
        {
            var result = new List<DisciplineStudentRecordMockModel>();
            var recordByDisciplineId = AcademicPlans
                .SelectMany(x => x.AcademicPlanRecords)
                .ToDictionary(x => x.DisciplineId ?? 0, x => x);

            var groupById = StudentGroups.ToDictionary(x => x.Id, x => x);

            int id = 1;

            foreach (var student in Students)
            {
                if (!student.StudentGroupId.HasValue || !groupById.TryGetValue(student.StudentGroupId.Value, out var group))
                    continue;

                var disciplineIds = group.Course switch
                {
                    AcademicCourse.Course_1 => Enumerable.Range(1, 3),
                    AcademicCourse.Course_2 => Enumerable.Range(1, 6),
                    AcademicCourse.Course_3 => Enumerable.Range(1, 9),
                    AcademicCourse.Course_4 => Enumerable.Range(1, 12),
                    _ => Enumerable.Empty<int>()
                };

                foreach (var disciplineId in disciplineIds)
                {
                    var planRecord = recordByDisciplineId[disciplineId];

                    var variant =
                        planRecord.Exam == 1 ? "Экзамен" :
                        planRecord.GradedPass == 1 ? "Дифф. зачет" :
                        planRecord.Pass == 1 ? "Зачет" :
                        "Аттестация";

                    var semester = planRecord.Semester == 1
                        ? Semesters.Первый
                        : Semesters.Второй;

                    var mark = GetDemoMark(student, disciplineId);

                    result.Add(new DisciplineStudentRecordMockModel
                    {
                        Id = id++,
                        DisciplineId = disciplineId,
                        StudentId = student.Id,
                        Semester = semester,
                        Variant = variant,
                        SubGroup = ((student.Id - 1) % 2) + 1,
                        MarkType = mark
                    });
                }
            }

            return result;
        }

        private static MarkType GetDemoMark(StudentMockModel student, int disciplineId)
        {
            if (student.StudentState == StudentState.Академ && disciplineId >= 5)
                return MarkType.Неявка;

            if (student.Id == 10 && (disciplineId == 5 || disciplineId == 8))
                return MarkType.Неудовлетворительно;

            var marks = new[]
            {
                MarkType.Отлично,
                MarkType.Хорошо,
                MarkType.Удовлетворительно,
                MarkType.Хорошо,
                MarkType.Отлично
            };

            return marks[(student.Id + disciplineId) % marks.Length];
        }

        public static List<StudentOrderMockModel> StudentOrders => new()
        {
            new StudentOrderMockModel
            {
                Id = 1,
                OrderNumber = "201-к",
                StudentOrderType = StudentOrderType.Зачисление,
                Blocks = new List<StudentOrderBlockMockModel>
                {
                    new StudentOrderBlockMockModel
                    {
                        Id = 1,
                        StudentOrderId = 1,
                        EducationDirectionId = 1,
                        StudentOrderType = StudentOrderType.Зачисление,
                        Students = new List<StudentOrderBlockStudentMockModel>
                        {
                            new() { Id = 1, StudentOrderBlockId = 1, StudentId = 1, StudentGroupFromId = null, StudentGroupToId = 1 },
                            new() { Id = 2, StudentOrderBlockId = 1, StudentId = 2, StudentGroupFromId = null, StudentGroupToId = 1 },
                            new() { Id = 3, StudentOrderBlockId = 1, StudentId = 3, StudentGroupFromId = null, StudentGroupToId = 1 },
                            new() { Id = 4, StudentOrderBlockId = 1, StudentId = 4, StudentGroupFromId = null, StudentGroupToId = 1 }
                        }
                    }
                }
            },

            new StudentOrderMockModel
            {
                Id = 2,
                OrderNumber = "57-лс",
                StudentOrderType = StudentOrderType.ВАкадем,
                Blocks = new List<StudentOrderBlockMockModel>
                {
                    new StudentOrderBlockMockModel
                    {
                        Id = 2,
                        StudentOrderId = 2,
                        EducationDirectionId = 1,
                        StudentOrderType = StudentOrderType.ВАкадем,
                        Students = new List<StudentOrderBlockStudentMockModel>
                        {
                            new() { Id = 5, StudentOrderBlockId = 2, StudentId = 7, StudentGroupFromId = 2, StudentGroupToId = null }
                        }
                    }
                }
            },

            new StudentOrderMockModel
            {
                Id = 3,
                OrderNumber = "74-лс",
                StudentOrderType = StudentOrderType.ИзАкадема,
                Blocks = new List<StudentOrderBlockMockModel>
                {
                    new StudentOrderBlockMockModel
                    {
                        Id = 3,
                        StudentOrderId = 3,
                        EducationDirectionId = 1,
                        StudentOrderType = StudentOrderType.ИзАкадема,
                        Students = new List<StudentOrderBlockStudentMockModel>
                        {
                            new() { Id = 6, StudentOrderBlockId = 3, StudentId = 7, StudentGroupFromId = null, StudentGroupToId = 2 }
                        }
                    }
                }
            },

            new StudentOrderMockModel
            {
                Id = 4,
                OrderNumber = "88-п",
                StudentOrderType = StudentOrderType.ПереводВГруппу,
                Blocks = new List<StudentOrderBlockMockModel>
                {
                    new StudentOrderBlockMockModel
                    {
                        Id = 4,
                        StudentOrderId = 4,
                        EducationDirectionId = 1,
                        StudentOrderType = StudentOrderType.ПереводВГруппу,
                        Students = new List<StudentOrderBlockStudentMockModel>
                        {
                            new() { Id = 7, StudentOrderBlockId = 4, StudentId = 12, StudentGroupFromId = 3, StudentGroupToId = 2 },
                            new() { Id = 8, StudentOrderBlockId = 4, StudentId = 10, StudentGroupFromId = 3, StudentGroupToId = 4 }
                        }
                    }
                }
            },

            new StudentOrderMockModel
            {
                Id = 5,
                OrderNumber = "96-в",
                StudentOrderType = StudentOrderType.Восстановить,
                Blocks = new List<StudentOrderBlockMockModel>
                {
                    new StudentOrderBlockMockModel
                    {
                        Id = 5,
                        StudentOrderId = 5,
                        EducationDirectionId = 1,
                        StudentOrderType = StudentOrderType.Восстановить,
                        Students = new List<StudentOrderBlockStudentMockModel>
                        {
                            new() { Id = 9, StudentOrderBlockId = 5, StudentId = 15, StudentGroupFromId = null, StudentGroupToId = 4 }
                        }
                    }
                }
            },

            new StudentOrderMockModel
            {
                Id = 6,
                OrderNumber = "103-лс",
                StudentOrderType = StudentOrderType.ОтчислитьЗаНеуспевамость,
                Blocks = new List<StudentOrderBlockMockModel>
                {
                    new StudentOrderBlockMockModel
                    {
                        Id = 6,
                        StudentOrderId = 6,
                        EducationDirectionId = 1,
                        StudentOrderType = StudentOrderType.ОтчислитьЗаНеуспевамость,
                        Students = new List<StudentOrderBlockStudentMockModel>
                        {
                            new() { Id = 10, StudentOrderBlockId = 6, StudentId = 16, StudentGroupFromId = 4, StudentGroupToId = null }
                        }
                    }
                }
            },

            new StudentOrderMockModel
            {
                Id = 7,
                OrderNumber = "111-лс",
                StudentOrderType = StudentOrderType.ОтчислитьПоСобственному,
                Blocks = new List<StudentOrderBlockMockModel>
                {
                    new StudentOrderBlockMockModel
                    {
                        Id = 7,
                        StudentOrderId = 7,
                        EducationDirectionId = 1,
                        StudentOrderType = StudentOrderType.ОтчислитьПоСобственному,
                        Students = new List<StudentOrderBlockStudentMockModel>
                        {
                            new() { Id = 11, StudentOrderBlockId = 7, StudentId = 8, StudentGroupFromId = 2, StudentGroupToId = null }
                        }
                    }
                }
            },

            new StudentOrderMockModel
            {
                Id = 8,
                OrderNumber = "125-комб",
                StudentOrderType = StudentOrderType.ПереводВГруппу,
                Blocks = new List<StudentOrderBlockMockModel>
                {
                    new StudentOrderBlockMockModel
                    {
                        Id = 8,
                        StudentOrderId = 8,
                        EducationDirectionId = 1,
                        StudentOrderType = StudentOrderType.ПереводВГруппу,
                        Students = new List<StudentOrderBlockStudentMockModel>
                        {
                            new() { Id = 12, StudentOrderBlockId = 8, StudentId = 5, StudentGroupFromId = 2, StudentGroupToId = 3 },
                            new() { Id = 13, StudentOrderBlockId = 8, StudentId = 6, StudentGroupFromId = 2, StudentGroupToId = 3 }
                        }
                    },
                    new StudentOrderBlockMockModel
                    {
                        Id = 9,
                        StudentOrderId = 8,
                        EducationDirectionId = 1,
                        StudentOrderType = StudentOrderType.ВАкадем,
                        Students = new List<StudentOrderBlockStudentMockModel>
                        {
                            new() { Id = 14, StudentOrderBlockId = 9, StudentId = 11, StudentGroupFromId = 3, StudentGroupToId = null }
                        }
                    },
                    new StudentOrderBlockMockModel
                    {
                        Id = 10,
                        StudentOrderId = 8,
                        EducationDirectionId = 1,
                        StudentOrderType = StudentOrderType.ОтчислитьЗаНеуспевамость,
                        Students = new List<StudentOrderBlockStudentMockModel>
                        {
                            new() { Id = 15, StudentOrderBlockId = 10, StudentId = 10, StudentGroupFromId = 4, StudentGroupToId = null }
                        }
                    }
                }
            },

            new StudentOrderMockModel
            {
                Id = 9,
                OrderNumber = "131-р",
                StudentOrderType = StudentOrderType.ПереводВГруппу,
                Blocks = new List<StudentOrderBlockMockModel>
                {
                    new StudentOrderBlockMockModel
                    {
                        Id = 11,
                        StudentOrderId = 9,
                        EducationDirectionId = 1,
                        StudentOrderType = StudentOrderType.ПереводВГруппу,
                        Students = new List<StudentOrderBlockStudentMockModel>
                        {
                            new() { Id = 16, StudentOrderBlockId = 11, StudentId = 1, StudentGroupFromId = 1, StudentGroupToId = 2 },
                            new() { Id = 17, StudentOrderBlockId = 11, StudentId = 2, StudentGroupFromId = 1, StudentGroupToId = 2 }
                        }
                    }
                }
            }
        };
    }
}