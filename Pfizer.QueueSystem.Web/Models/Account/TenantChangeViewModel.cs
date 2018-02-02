using Abp.AutoMapper;
using Pfizer.QueueSystem.Sessions.Dto;

namespace Pfizer.QueueSystem.Web.Models.Account
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}