using System.Collections.Generic;
using System.Linq;
using Pfizer.QueueSystem.Roles.Dto;
using Pfizer.QueueSystem.Users.Dto;

namespace Pfizer.QueueSystem.Web.Models.Users
{
    public class EditUserModalViewModel
    {
        public UserDto User { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }

        public bool UserIsInRole(RoleDto role)
        {
            return User.Roles != null && User.Roles.Any(r => r == role.DisplayName);
        }
    }
}