using System.Collections.Generic;
using Pfizer.QueueSystem.Roles.Dto;

namespace Pfizer.QueueSystem.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }

        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}