using Abp.Authorization;
using Pfizer.QueueSystem.Authorization.Roles;
using Pfizer.QueueSystem.Authorization.Users;

namespace Pfizer.QueueSystem.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
