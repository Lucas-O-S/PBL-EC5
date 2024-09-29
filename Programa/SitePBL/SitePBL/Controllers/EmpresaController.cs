using Microsoft.AspNetCore.Mvc;
using SitePBL.Models;
using SitePBL.DAO;
using System.ComponentModel.DataAnnotations.Schema;

namespace SitePBL.Controllers
{
    public class EmpresaController : Controller
    {
        //variaveis para selecionar a tabela, e ordem de organização da lista
        private string tabela = "empresa";
        private string ordem = "nome";

        public IActionResult Form()
        {
            return View("Form");
        }
        public IActionResult Listagem()
        {
            try
            {
                EmpresaDAO dao = new EmpresaDAO();
                List<EmpresaViewModel> lista = dao.Listagem(tabela, ordem);
                return View("Listagem", lista);
            }
            catch (Exception ex)
            {
                return View("error", new ErrorViewModel(ex.ToString()));
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                EmpresaDAO dao = new EmpresaDAO();
                dao.Excluir(id, tabela);
                return RedirectToAction("Listagem");
            }
            catch (Exception ex)
            {
                return View("error", new ErrorViewModel(ex.ToString()));
            }
        }
        public IActionResult Edit(int id)
        {
            try
            {
                ViewBag.Operacao = "A";
                EmpresaDAO dao = new EmpresaDAO();
                EmpresaViewModel empresa = dao.Consulta(id, tabela);
                if (empresa == null)
                    return RedirectToAction("Listagem");
                else
                    return View("Form", empresa);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        public IActionResult Create()
        {
            try
            {
                ViewBag.Operacao = "I";
                EmpresaViewModel empresa = new EmpresaViewModel();

                EmpresaDAO dao = new EmpresaDAO();
                return View("Form", empresa);
            }
            catch (Exception ex)
            {
                return View("error", new ErrorViewModel(ex.ToString()));
            }
        }

        private void ValidaDados(EmpresaViewModel empresa, string operacao)
        {
            ModelState.Clear(); // limpa os erros criados automaticamente pelo Asp.net (que podem estar com msg em inglês)
            EmpresaDAO dao = new EmpresaDAO();
            
            if (string.IsNullOrEmpty(empresa.nome))
                ModelState.AddModelError("nome", "Campo obrigatório. Preencha o nome.");
            if (string.IsNullOrEmpty(empresa.sede))
                ModelState.AddModelError("sede", "Campo obrigatório. Preencha a sede");
        }

        public IActionResult Enviar(EmpresaViewModel empresa, string Operacao)
        {
            try
            {
                ValidaDados(empresa, Operacao);
                if (ModelState.IsValid)
                {
                    EmpresaDAO dao = new EmpresaDAO();
                    if (Operacao == "I")
                        dao.Inserir(empresa);
                    else
                        dao.Alterar(empresa);

                    return RedirectToAction("listagem");
                }
                else
                {
                    ViewBag.Operacao = Operacao;
                    return View("Form", empresa);
                }
            }
            catch (Exception ex)
            {
                return View("error", new ErrorViewModel(ex.ToString()));
            }
        }
    }
}
