using Microsoft.AspNetCore.Mvc;

namespace SitePBL.Controllers
{
    public class AcessoController : Controller
    {
        public IActionResult Login()
        {
            return View("Login");
        }
		public IActionResult Form()
		{
			return View("Form");
		}
	}
}
