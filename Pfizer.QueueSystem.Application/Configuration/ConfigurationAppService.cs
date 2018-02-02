using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Pfizer.QueueSystem.Configuration.Dto;

namespace Pfizer.QueueSystem.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : QueueSystemAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
