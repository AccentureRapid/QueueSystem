using System.Threading.Tasks;
using Abp.Application.Services;
using Pfizer.QueueSystem.Configuration.Dto;

namespace Pfizer.QueueSystem.Configuration
{
    public interface IConfigurationAppService: IApplicationService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}