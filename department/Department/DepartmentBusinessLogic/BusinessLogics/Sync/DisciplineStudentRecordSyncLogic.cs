using DepartmentContracts.BindingModels;
using DepartmentContracts.BusinessLogicsContracts;
using DepartmentContracts.BusinessLogicsContracts.Sync;
using DepartmentContracts.StoragesContracts;
using DepartmentContracts.ViewModels;

namespace DepartmentBusinessLogic.BusinessLogics.Sync
{
    public class DisciplineStudentRecordSyncLogic : IDisciplineStudentRecordSyncLogic
    {
        private readonly IOneCApiService _oneCApiService;
        private readonly IDisciplineStudentRecordStorage _disciplineStudentRecordStorage;
        private readonly IDisciplineStorage _disciplineStorage;
        private readonly IStudentStorage _studentStorage;

        public DisciplineStudentRecordSyncLogic(
            IOneCApiService oneCApiService,
            IDisciplineStudentRecordStorage disciplineStudentRecordStorage,
            IDisciplineStorage disciplineStorage,
            IStudentStorage studentStorage)
        {
            _oneCApiService = oneCApiService;
            _disciplineStudentRecordStorage = disciplineStudentRecordStorage;
            _disciplineStorage = disciplineStorage;
            _studentStorage = studentStorage;
        }

        public async Task SyncDisciplineStudentRecordsAsync()
        {
            var oneCRecords = await _oneCApiService.GetDisciplineStudentRecordsAsync(); // Получаем записи успеваемости из OneC

            var currentRecords = _disciplineStudentRecordStorage.GetFullList() ?? new List<DisciplineStudentRecordViewModel>();
            var currentDisciplines = _disciplineStorage.GetFullList() ?? new List<DisciplineViewModel>();
            var currentStudents = _studentStorage.GetFullList() ?? new List<StudentViewModel>();

            foreach (var oneCRecord in oneCRecords)
            {
                var existingDiscipline = currentDisciplines.FirstOrDefault(x => x.Id == oneCRecord.DisciplineId);
                if (existingDiscipline == null)
                {
                    throw new InvalidOperationException(
                        $"Не найдена дисциплина с Id = {oneCRecord.DisciplineId} для записи успеваемости Id = {oneCRecord.Id}.");
                }

                var existingStudent = currentStudents.FirstOrDefault(x => x.Id == oneCRecord.StudentId);
                if (existingStudent == null)
                {
                    throw new InvalidOperationException(
                        $"Не найден студент с Id = {oneCRecord.StudentId} для записи успеваемости Id = {oneCRecord.Id}.");
                }

                var existingRecord = currentRecords.FirstOrDefault(x => x.Id == oneCRecord.Id);

                var model = new DisciplineStudentRecordBindingModel
                {
                    Id = oneCRecord.Id,
                    DisciplineId = oneCRecord.DisciplineId,
                    StudentId = oneCRecord.StudentId,
                    Semester = oneCRecord.Semester,
                    Variant = oneCRecord.Variant,
                    SubGroup = oneCRecord.SubGroup,
                    MarkType = oneCRecord.MarkType,
                };

                if (existingRecord == null)
                {
                    _disciplineStudentRecordStorage.Insert(model);

                    currentRecords.Add(new DisciplineStudentRecordViewModel
                    {
                        Id = model.Id,
                        DisciplineId = model.DisciplineId,
                        StudentId = model.StudentId,
                        Semester = model.Semester,
                        Variant = model.Variant,
                        SubGroup = model.SubGroup,
                        MarkType = model.MarkType,
                    });
                }
                else
                {
                    var needUpdate =
                        existingRecord.DisciplineId != oneCRecord.DisciplineId ||
                        existingRecord.StudentId != oneCRecord.StudentId ||
                        existingRecord.Semester != oneCRecord.Semester ||
                        existingRecord.Variant != oneCRecord.Variant ||
                        existingRecord.SubGroup != oneCRecord.SubGroup ||
                        existingRecord.MarkType != oneCRecord.MarkType;

                    if (needUpdate)
                    {
                        _disciplineStudentRecordStorage.Update(model);

                        existingRecord.DisciplineId = model.DisciplineId;
                        existingRecord.StudentId = model.StudentId;
                        existingRecord.Semester = model.Semester;
                        existingRecord.Variant = model.Variant;
                        existingRecord.SubGroup = model.SubGroup;
                        existingRecord.MarkType = model.MarkType;
                    }
                }
            }

            DeleteRemovedDisciplineStudentRecords(oneCRecords, currentRecords);
        }

        private void DeleteRemovedDisciplineStudentRecords(
            List<DepartmentContracts.Dtos.OneC.DisciplineStudentRecordOneCDto> oneCRecords,
            List<DisciplineStudentRecordViewModel> currentRecords)
        {
            var oneCRecordIds = oneCRecords
                .Select(x => x.Id)
                .ToHashSet();

            var recordsToDelete = currentRecords
                .Where(x => !oneCRecordIds.Contains(x.Id))
                .ToList();

            foreach (var record in recordsToDelete)
            {
                _disciplineStudentRecordStorage.Delete(new DisciplineStudentRecordBindingModel
                {
                    Id = record.Id
                });

                currentRecords.Remove(record);
            }
        }
    }
}