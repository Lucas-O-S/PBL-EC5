using Microsoft.AspNetCore.Mvc;
using SitePBL.Models;
using SitePBL.DAO;
using System.ComponentModel.DataAnnotations.Schema;

namespace SitePBL.Controllers
{

    public class EmpresaController : PadraoController<EmpresaViewModel>
    {
        public EmpresaController() { dao = new EmpresaDAO(); }

        protected override void ValidarDados(EmpresaViewModel model, string operacao)
        {
            base.ValidarDados(model, operacao);
        }
    }
}
