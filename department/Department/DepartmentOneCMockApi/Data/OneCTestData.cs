using DepartmentDataModels.Enums;
using DepartmentOneCMockApi.Models;

namespace DepartmentOneCMockApi.Data
{
    public static class OneCTestData
    {
        public static List<AcademicPlanMockModel> AcademicPlans => new()
        {
            new AcademicPlanMockModel
            {
                Id = 1,
                EducationDirectionId = 1,
                EducationForm = EducationForm.Очная,
                AcademicCourses = AcademicCourse.Course_1,
                Year = "2022-2026",
                AcademicPlanRecords = new List<AcademicPlanRecordMockModel>
                {
                    new()
                    {
                        Id = 1,
                        AcademicPlanId = 1,
                        DisciplineId = 1,
                        DisciplineBlockId = 1,
                        DisciplineBlockTitle = "Программная инженерия и разработка ПО",
                        DisciplineBlockBlueAsteriskName = "",
                        DisciplineBlockUseForGrouping = true,
                        DisciplineBlockOrder = 1,
                        DisciplineShortName = "Программирование",
                        DisciplineDescription = "Основы программирования на C#",
                        HasExam = true,
                        HasCredit = false,
                        HasCourseWork = false,
                        HasCourseProject = false,
                        Index = "Б1.О.01",
                        Name = "Программирование",
                        Semester = 1,
                        Zet = 4,
                        AcademicHours = 144,
                        Exam = 1,
                        Pass = null,
                        GradedPass = null,
                        CourseWork = null,
                        CourseProject = null,
                        Rgr = null,
                        Lectures = 48,
                        LaboratoryHours = 48,
                        PracticalHours = 48
                    },
                    new()
                    {
                        Id = 2,
                        AcademicPlanId = 1,
                        DisciplineId = 2,
                        DisciplineBlockId = 1,
                        DisciplineBlockTitle = "Фундаментальная и математическая подготовка",
                        DisciplineBlockBlueAsteriskName = "",
                        DisciplineBlockUseForGrouping = true,
                        DisciplineBlockOrder = 1,
                        DisciplineShortName = "Матан",
                        DisciplineDescription = "Базовый курс математического анализа",
                        HasExam = true,
                        HasCredit = false,
                        HasCourseWork = false,
                        HasCourseProject = false,
                        Index = "Б1.О.02",
                        Name = "Математический анализ",
                        Semester = 1,
                        Zet = 3,
                        AcademicHours = 108,
                        Exam = 1,
                        Pass = null,
                        GradedPass = null,
                        CourseWork = null,
                        CourseProject = null,
                        Rgr = null,
                        Lectures = 36,
                        LaboratoryHours = 0,
                        PracticalHours = 72
                    },
                    new()
                    {
                        Id = 3,
                        AcademicPlanId = 1,
                        DisciplineId = 3,
                        DisciplineBlockId = 1,
                        DisciplineBlockTitle = "Фундаментальная и математическая подготовка",
                        DisciplineBlockBlueAsteriskName = "",
                        DisciplineBlockUseForGrouping = true,
                        DisciplineBlockOrder = 1,
                        DisciplineShortName = "БД",
                        DisciplineDescription = "Введение в реляционные базы данных",
                        HasExam = false,
                        HasCredit = true,
                        HasCourseWork = false,
                        HasCourseProject = false,
                        Index = "Б1.О.03",
                        Name = "Базы данных",
                        Semester = 2,
                        Zet = 4,
                        AcademicHours = 144,
                        Exam = null,
                        Pass = 1,
                        GradedPass = null,
                        CourseWork = null,
                        CourseProject = null,
                        Rgr = null,
                        Lectures = 48,
                        LaboratoryHours = 48,
                        PracticalHours = 48
                    }
                }
            },

            new AcademicPlanMockModel
            {
                Id = 2,
                EducationDirectionId = 1,
                EducationForm = EducationForm.Очная,
                AcademicCourses = AcademicCourse.Course_2,
                Year = "2022-2026",
                AcademicPlanRecords = new List<AcademicPlanRecordMockModel>
                {
                    new()
                    {
                        Id = 4,
                        AcademicPlanId = 2,
                        DisciplineId = 4,
                        DisciplineBlockId = 1,
                        DisciplineBlockTitle = "Программная инженерия и разработка ПО",
                        DisciplineBlockBlueAsteriskName = "",
                        DisciplineBlockUseForGrouping = true,
                        DisciplineBlockOrder = 1,
                        DisciplineShortName = "Алгоритмы",
                        DisciplineDescription = "Алгоритмы и структуры данных",
                        HasExam = true,
                        HasCredit = false,
                        HasCourseWork = false,
                        HasCourseProject = false,
                        Index = "Б1.О.04",
                        Name = "Алгоритмы и структуры данных",
                        Semester = 1,
                        Zet = 4,
                        AcademicHours = 144,
                        Exam = 1,
                        Pass = null,
                        GradedPass = null,
                        CourseWork = null,
                        CourseProject = null,
                        Rgr = null,
                        Lectures = 48,
                        LaboratoryHours = 48,
                        PracticalHours = 48
                    },
                    new()
                    {
                        Id = 5,
                        AcademicPlanId = 2,
                        DisciplineId = 5,
                        DisciplineBlockId = 1,
                        DisciplineBlockTitle = "Программная инженерия и разработка ПО",
                        DisciplineBlockBlueAsteriskName = "",
                        DisciplineBlockUseForGrouping = true,
                        DisciplineBlockOrder = 1,
                        DisciplineShortName = "ООП",
                        DisciplineDescription = "Объектно-ориентированное программирование",
                        HasExam = true,
                        HasCredit = false,
                        HasCourseWork = true,
                        HasCourseProject = false,
                        Index = "Б1.О.05",
                        Name = "Объектно-ориентированное программирование",
                        Semester = 1,
                        Zet = 5,
                        AcademicHours = 180,
                        Exam = 1,
                        Pass = null,
                        GradedPass = null,
                        CourseWork = 1,
                        CourseProject = null,
                        Rgr = null,
                        Lectures = 60,
                        LaboratoryHours = 60,
                        PracticalHours = 60
                    },
                    new()
                    {
                        Id = 6,
                        AcademicPlanId = 2,
                        DisciplineId = 6,
                        DisciplineBlockId = 1,
                        DisciplineBlockTitle = "Системное и сетевое администрирование",
                        DisciplineBlockBlueAsteriskName = "",
                        DisciplineBlockUseForGrouping = true,
                        DisciplineBlockOrder = 1,
                        DisciplineShortName = "ОС",
                        DisciplineDescription = "Операционные системы",
                        HasExam = false,
                        HasCredit = false,
                        HasCourseWork = false,
                        HasCourseProject = false,
                        Index = "Б1.О.06",
                        Name = "Операционные системы",
                        Semester = 2,
                        Zet = 3,
                        AcademicHours = 108,
                        Exam = null,
                        Pass = null,
                        GradedPass = 1,
                        CourseWork = null,
                        CourseProject = null,
                        Rgr = null,
                        Lectures = 36,
                        LaboratoryHours = 36,
                        PracticalHours = 36
                    }
                }
            },

            new AcademicPlanMockModel
            {
                Id = 3,
                EducationDirectionId = 1,
                EducationForm = EducationForm.Очная,
                AcademicCourses = AcademicCourse.Course_3,
                Year = "2022-2026",
                AcademicPlanRecords = new List<AcademicPlanRecordMockModel>
                {
                    new()
                    {
                        Id = 7,
                        AcademicPlanId = 3,
                        DisciplineId = 7,
                        DisciplineBlockId = 1,
                        DisciplineBlockTitle = "Системное и сетевое администрирование",
                        DisciplineBlockBlueAsteriskName = "",
                        DisciplineBlockUseForGrouping = true,
                        DisciplineBlockOrder = 1,
                        DisciplineShortName = "Сети",
                        DisciplineDescription = "Сетевые технологии",
                        HasExam = true,
                        HasCredit = false,
                        HasCourseWork = false,
                        HasCourseProject = true,
                        Index = "Б1.О.07",
                        Name = "Сетевые технологии",
                        Semester = 1,
                        Zet = 5,
                        AcademicHours = 180,
                        Exam = 1,
                        Pass = null,
                        GradedPass = null,
                        CourseWork = null,
                        CourseProject = 1,
                        Rgr = null,
                        Lectures = 60,
                        LaboratoryHours = 60,
                        PracticalHours = 60
                    },
                    new()
                    {
                        Id = 8,
                        AcademicPlanId = 3,
                        DisciplineId = 8,
                        DisciplineBlockId = 1,
                        DisciplineBlockTitle = "Системное и сетевое администрирование",
                        DisciplineBlockBlueAsteriskName = "",
                        DisciplineBlockUseForGrouping = true,
                        DisciplineBlockOrder = 1,
                        DisciplineShortName = "ИБ",
                        DisciplineDescription = "Информационная безопасность",
                        HasExam = false,
                        HasCredit = false,
                        HasCourseWork = false,
                        HasCourseProject = false,
                        Index = "Б1.О.08",
                        Name = "Информационная безопасность",
                        Semester = 1,
                        Zet = 3,
                        AcademicHours = 108,
                        Exam = null,
                        Pass = null,
                        GradedPass = 1,
                        CourseWork = null,
                        CourseProject = null,
                        Rgr = null,
                        Lectures = 36,
                        LaboratoryHours = 36,
                        PracticalHours = 36
                    },
                    new()
                    {
                        Id = 9,
                        AcademicPlanId = 3,
                        DisciplineId = 9,
                        DisciplineBlockId = 1,
                        DisciplineBlockTitle = "Программная инженерия и разработка ПО",
                        DisciplineBlockBlueAsteriskName = "",
                        DisciplineBlockUseForGrouping = true,
                        DisciplineBlockOrder = 1,
                        DisciplineShortName = "Проектирование ИС",
                        DisciplineDescription = "Проектирование информационных систем",
                        HasExam = false,
                        HasCredit = true,
                        HasCourseWork = false,
                        HasCourseProject = false,
                        Index = "Б1.О.09",
                        Name = "Проектирование информационных систем",
                        Semester = 2,
                        Zet = 4,
                        AcademicHours = 144,
                        Exam = null,
                        Pass = 1,
                        GradedPass = null,
                        CourseWork = null,
                        CourseProject = null,
                        Rgr = 1,
                        Lectures = 48,
                        LaboratoryHours = 48,
                        PracticalHours = 48
                    }
                }
            },

            new AcademicPlanMockModel
            {
                Id = 4,
                EducationDirectionId = 1,
                EducationForm = EducationForm.Очная,
                AcademicCourses = AcademicCourse.Course_4,
                Year = "2022-2026",
                AcademicPlanRecords = new List<AcademicPlanRecordMockModel>
                {
                    new()
                    {
                        Id = 10,
                        AcademicPlanId = 4,
                        DisciplineId = 10,
                        DisciplineBlockId = 1,
                        DisciplineBlockTitle = "Программная инженерия и разработка ПО",
                        DisciplineBlockBlueAsteriskName = "",
                        DisciplineBlockUseForGrouping = true,
                        DisciplineBlockOrder = 1,
                        DisciplineShortName = "ML",
                        DisciplineDescription = "Машинное обучение",
                        HasExam = true,
                        HasCredit = false,
                        HasCourseWork = true,
                        HasCourseProject = false,
                        Index = "Б1.О.10",
                        Name = "Машинное обучение",
                        Semester = 1,
                        Zet = 6,
                        AcademicHours = 216,
                        Exam = 1,
                        Pass = null,
                        GradedPass = null,
                        CourseWork = 1,
                        CourseProject = null,
                        Rgr = null,
                        Lectures = 72,
                        LaboratoryHours = 72,
                        PracticalHours = 72
                    },
                    new()
                    {
                        Id = 11,
                        AcademicPlanId = 4,
                        DisciplineId = 11,
                        DisciplineBlockId = 1,
                        DisciplineBlockTitle = "Управление и проектная деятельность",
                        DisciplineBlockBlueAsteriskName = "",
                        DisciplineBlockUseForGrouping = true,
                        DisciplineBlockOrder = 1,
                        DisciplineShortName = "ИТ-проекты",
                        DisciplineDescription = "Управление ИТ-проектами",
                        HasExam = false,
                        HasCredit = false,
                        HasCourseWork = false,
                        HasCourseProject = false,
                        Index = "Б1.О.11",
                        Name = "Управление ИТ-проектами",
                        Semester = 2,
                        Zet = 4,
                        AcademicHours = 144,
                        Exam = null,
                        Pass = null,
                        GradedPass = 1,
                        CourseWork = null,
                        CourseProject = null,
                        Rgr = null,
                        Lectures = 48,
                        LaboratoryHours = 0,
                        PracticalHours = 96
                    },
                    new()
                    {
                        Id = 12,
                        AcademicPlanId = 4,
                        DisciplineId = 12,
                        DisciplineBlockId = 2,
                        DisciplineBlockTitle = "Факультативные дисциплины",
                        DisciplineBlockBlueAsteriskName = "*",
                        DisciplineBlockUseForGrouping = false,
                        DisciplineBlockOrder = 2,
                        DisciplineShortName = "Cloud",
                        DisciplineDescription = "Облачные технологии",
                        HasExam = false,
                        HasCredit = true,
                        HasCourseWork = false,
                        HasCourseProject = false,
                        Index = "ФТД.01",
                        Name = "Облачные технологии",
                        Semester = 2,
                        Zet = 3,
                        AcademicHours = 108,
                        Exam = null,
                        Pass = 1,
                        GradedPass = null,
                        CourseWork = null,
                        CourseProject = null,
                        Rgr = null,
                        Lectures = 36,
                        LaboratoryHours = 36,
                        PracticalHours = 36
                    }
                }
            }
        };

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
                GroupName = "ПИбд-21",
                Course = AcademicCourse.Course_2
            },
            new()
            {
                Id = 3,
                EducationDirectionId = 1,
                CuratorId = 2,
                GroupName = "ПИбд-31",
                Course = AcademicCourse.Course_3
            },
            new()
            {
                Id = 4,
                EducationDirectionId = 1,
                CuratorId = 3,
                GroupName = "ПИбд-41",
                Course = AcademicCourse.Course_4
            }
        };

        public static List<StudentMockModel> Students => new()
        {
            // ПИбд-11
            new() { Id = 1, StudentGroupId = 1, NumberOfBook = "22001", FirstName = "Алина", LastName = "Кузнецова", Patronymic = "Сергеевна", Email = "a.kuznetsova@university.ru", StudentState = StudentState.Учится, Description = "Староста группы", IsSteward = true },
            new() { Id = 2, StudentGroupId = 1, NumberOfBook = "22002", FirstName = "Илья", LastName = "Громов", Patronymic = "Андреевич", Email = "i.gromov@university.ru", StudentState = StudentState.Учится, Description = "", IsSteward = false },
            new() { Id = 3, StudentGroupId = 1, NumberOfBook = "22003", FirstName = "Полина", LastName = "Фролова", Patronymic = "Олеговна", Email = "p.frolova@university.ru", StudentState = StudentState.Учится, Description = "", IsSteward = false },
            new() { Id = 4, StudentGroupId = 1, NumberOfBook = "22004", FirstName = "Егор", LastName = "Савельев", Patronymic = "Игоревич", Email = "e.saveliev@university.ru", StudentState = StudentState.Учится, Description = "", IsSteward = false },

            // ПИбд-21
            new() { Id = 5, StudentGroupId = 2, NumberOfBook = "21001", FirstName = "Мария", LastName = "Орлова", Patronymic = "Павловна", Email = "m.orlova@university.ru", StudentState = StudentState.Учится, Description = "Староста группы", IsSteward = true },
            new() { Id = 6, StudentGroupId = 2, NumberOfBook = "21002", FirstName = "Даниил", LastName = "Мельников", Patronymic = "Ильич", Email = "d.melnikov@university.ru", StudentState = StudentState.Учится, Description = "", IsSteward = false },
            new() { Id = 7, StudentGroupId = 2, NumberOfBook = "21003", FirstName = "Виктория", LastName = "Ершова", Patronymic = "Максимовна", Email = "v.ershova@university.ru", StudentState = StudentState.Академ, Description = "Академический отпуск с весеннего семестра", IsSteward = false },
            new() { Id = 8, StudentGroupId = 2, NumberOfBook = "21004", FirstName = "Артем", LastName = "Белов", Patronymic = "Денисович", Email = "a.belov@university.ru", StudentState = StudentState.Учится, Description = "", IsSteward = false },

            // ПИбд-31
            new() { Id = 9, StudentGroupId = 3, NumberOfBook = "20001", FirstName = "Наталья", LastName = "Соколова", Patronymic = "Игоревна", Email = "n.sokolova@university.ru", StudentState = StudentState.Учится, Description = "Староста группы", IsSteward = true },
            new() { Id = 10, StudentGroupId = 3, NumberOfBook = "20002", FirstName = "Кирилл", LastName = "Поляков", Patronymic = "Романович", Email = "k.polyakov@university.ru", StudentState = StudentState.Учится, Description = "Есть академическая задолженность", IsSteward = false },
            new() { Id = 11, StudentGroupId = 3, NumberOfBook = "20003", FirstName = "Елизавета", LastName = "Комарова", Patronymic = "Васильевна", Email = "e.komarova@university.ru", StudentState = StudentState.Учится, Description = "", IsSteward = false },
            new() { Id = 12, StudentGroupId = 3, NumberOfBook = "20004", FirstName = "Степан", LastName = "Жуков", Patronymic = "Петрович", Email = "s.zhukov@university.ru", StudentState = StudentState.Учится, Description = "", IsSteward = false },

            // ПИбд-41
            new() { Id = 13, StudentGroupId = 4, NumberOfBook = "19001", FirstName = "Анна", LastName = "Тарасова", Patronymic = "Дмитриевна", Email = "a.tarasova@university.ru", StudentState = StudentState.Учится, Description = "Староста группы", IsSteward = true },
            new() { Id = 14, StudentGroupId = 4, NumberOfBook = "19002", FirstName = "Максим", LastName = "Киселев", Patronymic = "Алексеевич", Email = "m.kiselev@university.ru", StudentState = StudentState.Учится, Description = "", IsSteward = false },
            new() { Id = 15, StudentGroupId = 4, NumberOfBook = "19003", FirstName = "Дарья", LastName = "Миронова", Patronymic = "Станиславовна", Email = "d.mironova@university.ru", StudentState = StudentState.Учится, Description = "", IsSteward = false },
            new() { Id = 16, StudentGroupId = 4, NumberOfBook = "19004", FirstName = "Павел", LastName = "Логинов", Patronymic = "Евгеньевич", Email = "p.loginov@university.ru", StudentState = StudentState.Учится, Description = "Фигурирует в приказе на отчисление", IsSteward = false }
        };

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

            // 5. Восстановление студента
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