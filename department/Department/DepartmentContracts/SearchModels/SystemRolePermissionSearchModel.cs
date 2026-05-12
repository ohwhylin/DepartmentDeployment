namespace DepartmentContracts.SearchModels
{
    public class SystemRolePermissionSearchModel
    {
        public int? Id { get; set; }
        public int? RoleId { get; set; }
        public int? PermissionId { get; set; }
        public string? RoleCode { get; set; }
        public string? PermissionCode { get; set; }
    }
}