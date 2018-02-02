using Abp.Web.Mvc.Views;

namespace Pfizer.QueueSystem.Web.Views
{
    public abstract class QueueSystemWebViewPageBase : QueueSystemWebViewPageBase<dynamic>
    {

    }

    public abstract class QueueSystemWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected QueueSystemWebViewPageBase()
        {
            LocalizationSourceName = QueueSystemConsts.LocalizationSourceName;
        }
    }
}