using Microsoft.AspNetCore.Mvc;
using SitePBL.DAO;
using SitePBL.Models;

namespace SitePBL.Controllers
{
    public class FuncionarioController : PadraoController<FuncionarioViewModel>
    {
        public FuncionarioController() { dao = new FuncionarioDAO(); }

        protected override void ValidarDados(FuncionarioViewModel model, string operacao)
        {
            base.ValidarDados(model, operacao);
            if (string.IsNullOrEmpty(model.nome))
                ModelState.AddModelError("nome", "Campo obrigatório! Preencha o nome!");
            if (string.IsNullOrEmpty(model.cargo))
                ModelState.AddModelError("cargo", "Campo obrigatório! Preencha o cargo!");
            if (model.dataContratacao > DateTime.Now || model.dataContratacao < new DateTime(1900, 1, 1))
                ModelState.AddModelError("dataContratacao", "Digite uma data válida!");
        }

    }
}
