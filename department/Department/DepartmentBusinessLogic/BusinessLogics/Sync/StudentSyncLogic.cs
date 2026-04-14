using DepartmentContracts.BindingModels;
using DepartmentContracts.BusinessLogicsContracts;
using DepartmentContracts.BusinessLogicsContracts.Sync;
using DepartmentContracts.StoragesContracts;
using DepartmentContracts.ViewModels;

namespace DepartmentBusinessLogic.BusinessLogics.Sync
{
    public class StudentSyncLogic : IStudentSyncLogic
    {
        private readonly IOneCApiService _oneCApiService;
        private readonly IStudentStorage _studentStorage;
        private readonly IStudentGroupStorage _studentGroupStorage;

        public StudentSyncLogic(
            IOneCApiService oneCApiService,
            IStudentStorage studentStorage,
            IStudentGroupStorage studentGroupStorage)
        {
            _oneCApiService = oneCApiService;
            _studentStorage = studentStorage;
            _studentGroupStorage = studentGroupStorage;
        }

        public async Task SyncStudentsAsync()
        {
            var oneCStudents = await _oneCApiService.GetStudentsAsync();
            var currentStudents = _studentStorage.GetFullList() ?? new List<StudentViewModel>();
            var currentGroups = _studentGroupStorage.GetFullList() ?? new List<StudentGroupViewModel>();

            foreach (var oneCStudent in oneCStudents)
            {
                if (oneCStudent.StudentGroupId.HasValue)
                {
                    var existingGroup = currentGroups.FirstOrDefault(x => x.Id == oneCStudent.StudentGroupId.Value);
                    if (existingGroup == null)
                    {
                        throw new InvalidOperationException(
                            $"Не найдена учебная группа с Id = {oneCStudent.StudentGroupId.Value} для студента '{oneCStudent.LastName} {oneCStudent.FirstName} {oneCStudent.Patronymic}'.");
                    }
                }

                var existingStudent = currentStudents.FirstOrDefault(x => x.Id == oneCStudent.Id);

                var model = new StudentBindingModel
                {
                    Id = oneCStudent.Id,
                    StudentGroupId = oneCStudent.StudentGroupId,
                    NumberOfBook = oneCStudent.NumberOfBook,
                    FirstName = oneCStudent.FirstName,
                    LastName = oneCStudent.LastName,
                    Patronymic = oneCStudent.Patronymic,
                    Email = oneCStudent.Email,
                    StudentState = oneCStudent.StudentState,
                    Description = oneCStudent.Description,
                    Photo = oneCStudent.Photo ?? Array.Empty<byte>(),
                    IsSteward = oneCStudent.IsSteward
                };

                if (existingStudent == null)
                {
                    _studentStorage.Insert(model);

                    currentStudents.Add(new StudentViewModel
                    {
                        Id = model.Id,
                        StudentGroupId = model.StudentGroupId,
                        NumberOfBook = model.NumberOfBook,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Patronymic = model.Patronymic,
                        Email = model.Email,
                        StudentState = model.StudentState,
                        Description = model.Description,
                        Photo = model.Photo,
                        IsSteward = model.IsSteward
                    });
                }
                else
                {
                    var needUpdate =
                        existingStudent.StudentGroupId != oneCStudent.StudentGroupId ||
                        existingStudent.NumberOfBook != oneCStudent.NumberOfBook ||
                        existingStudent.FirstName != oneCStudent.FirstName ||
                        existingStudent.LastName != oneCStudent.LastName ||
                        existingStudent.Patronymic != oneCStudent.Patronymic ||
                        existingStudent.Email != oneCStudent.Email ||
                        existingStudent.StudentState != oneCStudent.StudentState ||
                        existingStudent.Description != oneCStudent.Description ||
                        existingStudent.IsSteward != oneCStudent.IsSteward ||
                        !ByteArrayEquals(existingStudent.Photo, oneCStudent.Photo);

                    if (needUpdate)
                    {
                        _studentStorage.Update(model);

                        existingStudent.StudentGroupId = model.StudentGroupId;
                        existingStudent.NumberOfBook = model.NumberOfBook;
                        existingStudent.FirstName = model.FirstName;
                        existingStudent.LastName = model.LastName;
                        existingStudent.Patronymic = model.Patronymic;
                        existingStudent.Email = model.Email;
                        existingStudent.StudentState = model.StudentState;
                        existingStudent.Description = model.Description;
                        existingStudent.Photo = model.Photo;
                        existingStudent.IsSteward = model.IsSteward;
                    }
                }
            }

            DeleteRemovedStudents(oneCStudents, currentStudents);
        }

        private void DeleteRemovedStudents(
            List<DepartmentContracts.Dtos.OneC.StudentOneCDto> oneCStudents,
            List<StudentViewModel> currentStudents)
        {
            var oneCStudentIds = oneCStudents
                .Select(x => x.Id)
                .ToHashSet();

            var studentsToDelete = currentStudents
                .Where(x => !oneCStudentIds.Contains(x.Id))
                .ToList();

            foreach (var student in studentsToDelete)
            {
                _studentStorage.Delete(new StudentBindingModel
                {
                    Id = student.Id
                });

                currentStudents.Remove(student);
            }
        }

        private bool ByteArrayEquals(byte[]? first, byte[]? second)
        {
            first ??= Array.Empty<byte>();
            second ??= Array.Empty<byte>();

            if (first.Length != second.Length)
            {
                return false;
            }

            for (int i = 0; i < first.Length; i++)
            {
                if (first[i] != second[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}