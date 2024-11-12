using Microsoft.AspNetCore.Mvc;
using SitePBL.DAO;
using SitePBL.Models;
using System.Data;
using System.Runtime.Intrinsics.Arm;

namespace SitePBL.Controllers
{

    public class SensorController : PadraoController<SensorViewModel>
    {
        public SensorController() { dao = new SensorDAO(); }

        protected override void ValidarDados(SensorViewModel model, string operacao)
        {
            base.ValidarDados(model, operacao);
        }

    }
}
