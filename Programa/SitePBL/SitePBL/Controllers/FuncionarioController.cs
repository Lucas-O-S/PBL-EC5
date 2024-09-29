using Microsoft.AspNetCore.Mvc;
using SitePBL.DAO;
using SitePBL.Models;

namespace SitePBL.Controllers
{
    public class FuncionarioController : Controller
    {

        //variaveis para selecionar a tabela, e ordem de organização da lista
        private string tabela = "funcionario";
        private string ordem = "nome";

        public IActionResult Form()
        {
            return View("Form");
        }
        public IActionResult Listagem()
        {
            try
            {
                FuncionarioDAO dao = new FuncionarioDAO();
                List<FuncionarioViewModel> lista = dao.Listagem(tabela, ordem);
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
                FuncionarioDAO dao = new FuncionarioDAO();
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
                FuncionarioDAO dao = new FuncionarioDAO();
                FuncionarioViewModel funcionario = dao.Consulta(id, tabela);
                if (funcionario == null)
                    return RedirectToAction("Listagem");
                else
                    return View("Form", funcionario);
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
                FuncionarioViewModel funcionario = new FuncionarioViewModel();

                FuncionarioDAO dao = new FuncionarioDAO();
                return View("Form", funcionario);
            }
            catch (Exception ex)
            {
                return View("error", new ErrorViewModel(ex.ToString()));
            }
        }

        private void ValidaDados(FuncionarioViewModel funcionario, string operacao)
        {
            ModelState.Clear(); // limpa os erros criados automaticamente pelo Asp.net (que podem estar com msg em inglês)
            FuncionarioDAO dao = new FuncionarioDAO();
            
            if (string.IsNullOrEmpty(funcionario.nome))
                ModelState.AddModelError("nome", "Campo obrigatório. Preencha o nome.");
            if (string.IsNullOrEmpty(funcionario.cargo))
                ModelState.AddModelError("cargo", "Campo obrigatório. Preencha o cargo");
        }
        public IActionResult Enviar(FuncionarioViewModel funcionario, string Operacao)
        {
            try
            {
                ValidaDados(funcionario, Operacao);
                if (ModelState.IsValid)
                {
                    FuncionarioDAO dao = new FuncionarioDAO();
                    if (Operacao == "I")
                        dao.Inserir(funcionario);
                    else
                        dao.Alterar(funcionario);

                    return RedirectToAction("listagem");
                }
                else
                {
                    ViewBag.Operacao = Operacao;
                    return View("Form", funcionario);
                }
            }
            catch (Exception ex)
            {
                return View("error", new ErrorViewModel(ex.ToString()));
            }
        }
    }
}
