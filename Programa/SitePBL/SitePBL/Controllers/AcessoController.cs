using Microsoft.AspNetCore.Mvc;
using SitePBL.DAO;
using SitePBL.Models;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace SitePBL.Controllers
{
    ///implementar padrao controller

    public class AcessoController : PadraoController<AcessoViewModel>
    {
        public AcessoController() { dao = new AcessoDAO(); }

        protected override void ValidarDados(AcessoViewModel model, string operacao)
        {
            base.ValidarDados(model, operacao);
        }
    }
}
