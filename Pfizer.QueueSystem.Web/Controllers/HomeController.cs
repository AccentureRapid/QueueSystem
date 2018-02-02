using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace Pfizer.QueueSystem.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : QueueSystemControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}