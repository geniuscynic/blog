using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Admin.Service.Permission.Input
{
    public class PermissionSaveTenantPermissionsInput
    {
        [Required(ErrorMessage = "�⻧����Ϊ�գ�")]
        public long TenantId { get; set; }

        [Required(ErrorMessage = "Ȩ�޲���Ϊ�գ�")]
        public List<long> PermissionIds { get; set; }
    }
}