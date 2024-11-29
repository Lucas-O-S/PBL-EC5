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

        /// <summary>
        /// Validação de dados ao criar login ou utiliza-lo
        /// </summary>
        /// <param name="acesso"></param>
        /// <param name="operacao"></param>
        protected override void ValidarDados(AcessoViewModel acesso, string operacao)
        {
            //Chama a versão base
            base.ValidarDados(acesso, operacao);
            AcessoDAO dao = new AcessoDAO();

            //Se for Cadastro
            if (operacao == "I")
            {
                //Verificação de valores vazios
                if (string.IsNullOrEmpty(acesso.nomeUsuario))
                    ModelState.AddModelError("nomeUsuario", "Campo obrigatório! Preencha o nome!");

                if (string.IsNullOrEmpty(acesso.loginUsuario))
                    ModelState.AddModelError("loginUsuario", "Campo obrigatório! Preencha o login!");

                if (string.IsNullOrEmpty(acesso.nomeEmpresa))
                    ModelState.AddModelError("nomeEmpresa", "Campo obrigatório! Preencha o nome da empresa!");

                if (string.IsNullOrEmpty(acesso.senha))
                    ModelState.AddModelError("senha", "Campo obrigatório! Preencha a senha!");

                //Se não estiverem vazios verificara se existe o login 
                if (!string.IsNullOrEmpty(acesso.loginUsuario))
                {
                    if (dao.RepeticaoLogin(acesso.loginUsuario))
                        ModelState.AddModelError("loginUsuario", "Este login já existe! Tente outro.");
                }
                
       
            }
            
            //Se for login verifica se os dados foram fornecidos
            else
            {
                if (string.IsNullOrEmpty(acesso.loginUsuario))
                    ModelState.AddModelError("loginUsuario", "Campo obrigatório! Preencha o login!");
                if (string.IsNullOrEmpty(acesso.senha))
                    ModelState.AddModelError("senha", "Campo obrigatório! Preencha a senha!");            
            }
        }


        /// <summary>
        /// Manda para a form como um login
        /// </summary>
        /// <returns>Tela form na operação de Login</returns>
        public IActionResult Login()
        {
            ViewBag.Operacao = "A";
            AcessoViewModel a = new AcessoViewModel();
            return View(NomeViewForm, a);
        }

        /// <summary>
        /// Manda para a form como um cadastro
        /// </summary>
        /// <returns>Tela de cadastro com a operação de cadastro</returns>
        public IActionResult Cadastro()
        {
            ViewBag.Operacao = "I";
            AcessoViewModel a = new AcessoViewModel();
            return View(NomeViewForm, a);
        }

        /// <summary>
        /// Envia o resultado
        /// </summary>
        /// <param name="model">model de acesso</param>
        /// <param name="operacao">operação usada, A = Login e I = Cadastro</param>
        /// <returns>Resultado da operação</returns>
        public IActionResult Enviar(AcessoViewModel model, string operacao)
        {
            AcessoDAO a = new AcessoDAO();
            ValidarDados(model, operacao);
            if (operacao == "A") // Operação de login
            {
                if (ModelState.IsValid && a.Login(model.loginUsuario, model.senha))
                {
                    HttpContext.Session.SetString("Logado", "true");
                    // Redireciona para a tela de transição
                    return View("Transicao");
                }
                else
                {
                    ViewBag.Operacao = operacao;
                    ViewBag.Erro = "Usuário ou senha inválidos.";
                    return View("Form");
                }
            }
            else // Operação de cadastro
            {
                if (ModelState.IsValid)
                {
                    a.Insert(model);
                    ViewBag.Operacao = "A";
                    // Redireciona para a tela de transição
                    return View("form");
                }
                else
                {
                    ViewBag.Operacao = operacao;
                    ViewBag.Erro = "Cadastro não efetuado. Tente novamente.";
                    return View("Form");
                }
            }
        }

        /// <summary>
        /// Finaliza a session
        /// </summary>
        /// <returns>Redireciona ao login</returns>
        public IActionResult LogOff()
        {
            HttpContext.Session.Clear();
            ViewBag.Operacao = "A";
            return RedirectToAction("login");
        }
    }
}
