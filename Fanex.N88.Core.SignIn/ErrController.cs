using System.Web.Mvc;

namespace Fanex.N88.Core.SignIn
{
    public class ErrController : Controller
    {
        [AllowAnonymous]
        public ActionResult NotFound()
        {
            return View("Fanex.N88.Core.SignIn.Views.NotFound");
        }
    }
}