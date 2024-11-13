using Microsoft.AspNetCore.Mvc;
using SitePBL.DAO;
using SitePBL.Models;

namespace SitePBL.Controllers
{
    public class ManutencaoController : PadraoController<ManutencaoViewModel>
    {
        public ManutencaoController() { dao = new ManutencaoDAO(); }
        public override IActionResult Create()
        {
            try
            {
                ViewBag.operacao = "I";
                ManutencaoViewModel model = new ManutencaoViewModel();
                
                FuncionarioDAO fDAO = new FuncionarioDAO();
                ViewBag.funcionarios = fDAO.Listagem();

                return View(NomeViewForm, model);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        protected override void ValidarDados(ManutencaoViewModel model, string operacao)
        {
            base.ValidarDados(model, operacao);

        }
    }
}
