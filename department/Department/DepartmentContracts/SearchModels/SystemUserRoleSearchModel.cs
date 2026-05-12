namespace DepartmentContracts.SearchModels
{
    public class SystemUserRoleSearchModel
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public int? RoleId { get; set; }
        public string? UserLogin { get; set; }
        public string? RoleCode { get; set; }
    }
}