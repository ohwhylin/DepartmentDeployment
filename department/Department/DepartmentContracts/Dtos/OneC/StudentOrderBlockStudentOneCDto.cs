namespace DepartmentContracts.Dtos.OneC
{
    public class StudentOrderBlockStudentOneCDto
    {
        public int Id { get; set; }
        public int StudentOrderBlockId { get; set; }
        public int StudentId { get; set; }
        public int? StudentGroupFromId { get; set; }
        public int? StudentGroupToId { get; set; }
    }
}