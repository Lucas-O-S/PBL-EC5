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

        public AcessoController() { 
            dao = new AcessoDAO(); 
            ExigeAutenticacao = false;
        }

        protected override void ValidarDados(AcessoViewModel acesso, string operacao)
        {
            base.ValidarDados(acesso, operacao);
            AcessoDAO dao = new AcessoDAO();

            if (operacao == "I")
            {

                if (string.IsNullOrEmpty(acesso.nomeUsuario))
                    ModelState.AddModelError("nomeUsuario", "Campo obrigatório! Preencha o nome!");

                if (string.IsNullOrEmpty(acesso.loginUsuario))
                    ModelState.AddModelError("loginUsuario", "Campo obrigatório! Preencha o login!");
                if (dao.VerificarLogin(acesso.loginUsuario) > 0)
                    ModelState.AddModelError("login", "Este login já existe! Tente outro.");

                if (string.IsNullOrEmpty(acesso.nomeEmpresa))
                    ModelState.AddModelError("nomeEmpresa", "Campo obrigatório! Preencha o nome da empresa!");

                if (string.IsNullOrEmpty(acesso.senha))
                    ModelState.AddModelError("senha", "Campo obrigatório! Preencha a senha!");
            }
            else
            {

                if (dao.Login(acesso.loginUsuario, acesso.senha))
                {
                    if (string.IsNullOrEmpty(acesso.loginUsuario))
                        ModelState.AddModelError("loginUsuario", "Campo obrigatório! Preencha o login!");
                    if (string.IsNullOrEmpty(acesso.senha))
                        ModelState.AddModelError("senha", "Campo obrigatório! Preencha a senha!");
                }
                else
                {
                    ModelState.AddModelError("loginE", "Login ou senha incorretos! Tente novamente.");
                }
            }
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
            ValidarDados(model,operacao);
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
            return RedirectToAction("login");
        }
    }
}
