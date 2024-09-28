using Microsoft.AspNetCore.Mvc;

namespace SitePBL.Controllers
{
    public class Empresa : Controller
    {
        public IActionResult Form()
        {
            return View("Form");
        }
        public IActionResult Listagem()
        {
            return View("Listagem");
        }
    }
}
