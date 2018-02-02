using System.Collections.Generic;
using Pfizer.QueueSystem.Roles.Dto;
using Pfizer.QueueSystem.Users.Dto;

namespace Pfizer.QueueSystem.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}