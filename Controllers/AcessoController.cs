using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SitePBL.DAO;
using SitePBL.Models;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace SitePBL.Controllers
{
    ///implementar padrao controller

    public class AcessoController : PadraoController<AcessoViewModel>
    {
        protected bool ExigeAutenticacao = false;

        public AcessoController() { dao = new AcessoDAO(); }

        protected override void ValidarDados(AcessoViewModel model, string operacao)
        {
            base.ValidarDados(model, operacao);
        }

        public IActionResult Login()
        {
            ViewBag.Operacao = "A";
            AcessoViewModel a = new AcessoViewModel();
            return View(NomeViewForm, a);
        }

        public IActionResult Cadastro()
        {
            ViewBag.Operacao = "I";
            AcessoViewModel a = new AcessoViewModel();
            return View(NomeViewForm, a);
        }

        public IActionResult Enviar(AcessoViewModel model, string operacao)
        {
            AcessoDAO a = new AcessoDAO();

            if(operacao == "A")
            {
                if (a.Login(model.loginUsuario, model.senha))
                {
                    HttpContext.Session.SetString("Logado", "true");
                    return RedirectToAction("index", "home");
                }
                else
                {
                    ViewBag.Operacao = operacao;
                    return View("Form");
                }
            }
            else
            {
                a.Insert(model);
                if (a.Login(model.nomeUsuario, model.senha))
                {
                    HttpContext.Session.SetString("Logado", "true");
                    return RedirectToAction("index", "home");
                }
                else
                {
                    ViewBag.Operacao = operacao;
                    return View("Form");
                }
            }
        }

        public IActionResult LogOff()
        {
            HttpContext.Session.Clear();
            ViewBag.Operacao = "A";
            return RedirectToAction("Form");
        }
    }
}
