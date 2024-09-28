using Microsoft.AspNetCore.Mvc;

namespace SitePBL.Controllers
{
    public class Funcionario : Controller
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
