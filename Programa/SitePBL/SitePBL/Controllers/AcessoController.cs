using Microsoft.AspNetCore.Mvc;
using SitePBL.DAO;
using SitePBL.Models;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace SitePBL.Controllers
{
    public class AcessoController : Controller
    {
        private string tabela = "acesso";

        public IActionResult Login()
        {
            return View("Login");
        }
        public IActionResult Form()
        {
            try
            {
                ViewBag.Operacao = "I";
                AcessoViewModel acesso = new AcessoViewModel();


                return View("form", acesso);

            }
            catch (Exception erro)
            {
                return View("error", new ErrorViewModel(erro.ToString()));
            }
        }
        public IActionResult Edit()
        {
            try
            {
                ViewBag.Operacao = "A";
                AcessoViewModel acesso = new AcessoViewModel();
                AcessoDAO dao = new AcessoDAO();


                return View("form", acesso);

            }
            catch (Exception erro)
            {
                return View("error", new ErrorViewModel(erro.ToString()));
            }
        }
        public IActionResult Enviar()
        {
            try
            {
                AcessoViewModel acesso = new AcessoViewModel();
                AcessoDAO dao = new AcessoDAO();
                dao.Insert(acesso);

                return View("/Home/index");
            }
            catch (Exception erro)
            {
                return View("error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                AcessoDAO dao = new AcessoDAO();
                dao.Delete (id);
                return RedirectToAction("");
            }
            catch (Exception ex)
            {
                return View("error", new ErrorViewModel(ex.ToString()));
            }
        }
    }
}
