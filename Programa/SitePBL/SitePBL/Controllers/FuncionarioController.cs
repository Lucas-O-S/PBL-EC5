using Microsoft.AspNetCore.Mvc;
using SitePBL.DAO;
using SitePBL.Models;

namespace SitePBL.Controllers
{
    public class FuncionarioController : PadraoController<FuncionarioViewModel>
    {
        public FuncionarioController() { dao = new FuncionarioDAO();

        protected override void ValidarDados(FuncionarioViewModel model, string operacao)
        {
            base.ValidarDados(model, operacao);
        }


    }
}
