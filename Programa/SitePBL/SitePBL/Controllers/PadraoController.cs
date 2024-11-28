﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SitePBL.DAO;
using SitePBL.Models;
using System.Reflection;

namespace SitePBL.Controllers
{
    public abstract class PadraoController<T> : Controller where T : PadraoViewModel
    {

        protected PadraoDAO<T> dao { get; set; }
        protected string NomeViewIndex { get; set; } = "index";
        protected string NomeViewForm { get; set; } = "form";

        protected bool ExigeAutenticacao { get; set; } = true;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (ExigeAutenticacao && !HelperController.VerificaUserLogado(HttpContext.Session))
                context.Result = RedirectToAction("Login", "Acesso");
            else
            {
                ViewBag.Logado = true;
                base.OnActionExecuting(context);
            }
        }

        /// <summary>
        /// Metodo virtual usado para adicionar dados a ViewBags a create e edit, assim não precisa modifica-las diretamente
        /// </summary>
        protected virtual void AdicionarViewbagsForm() { }
        protected virtual void AdicionarViewbagsIndex() { }

        protected virtual IActionResult RedirecionaParaIndex(T model)
        {
            return RedirectToAction(NomeViewIndex);
        }

        public virtual IActionResult Index()
        {
            try
            {
                AdicionarViewbagsIndex();
                var lista = dao.Listagem();
                return View(NomeViewIndex, lista);

            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public virtual IActionResult Create()
        {
            try
            {
                ViewBag.operacao = "I";
                T model = Activator.CreateInstance(typeof(T)) as T;
                AdicionarViewbagsForm();
                return View(NomeViewForm, model);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public virtual IActionResult Save(T model, string operacao)
        {
            try
            {
                ValidarDados(model, operacao);
                if (ModelState.IsValid == false)
                {
                    AdicionarViewbagsForm();
                    ViewBag.operacao = operacao;
                    return View(NomeViewForm, model);
                }
                else
                {
                    if (operacao == "I")
                        dao.Insert(model);


                    else
                        dao.Update(model);
                    return RedirecionaParaIndex(model);
                }
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        /// <summary>
        /// Limpa a model state
        /// </summary>
        /// <param name="model">model generica</param>
        /// <param name="operacao">operação realizada</param>
        protected virtual void ValidarDados(T model, string operacao) { ModelState.Clear(); }

        public virtual IActionResult Edit(int id)
        {
            try
            {
                ViewBag.operacao = "A";
                var model = dao.Consulta(id);
                AdicionarViewbagsForm();

                if (model == null)
                    return RedirecionaParaIndex(model);
                else
                    return View(NomeViewForm, model);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        public IActionResult Delete(int id)
        {
            try
            {
                var model = dao.Consulta(id);
                if (model != null)
                    dao.Delete(id);

                return RedirecionaParaIndex(model);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public virtual IActionResult Dashboard()
        {
            return View("Dashboard");
        }
    }
}

