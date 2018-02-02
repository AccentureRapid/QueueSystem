using System.Web.Mvc;

namespace Pfizer.QueueSystem.Web.Controllers
{
    public class AboutController : QueueSystemControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}