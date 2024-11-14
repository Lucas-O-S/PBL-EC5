using Microsoft.AspNetCore.Mvc;
using SitePBL.DAO;
using SitePBL.Models;

namespace SitePBL.Controllers
{
    public class ManutencaoController : PadraoController<ManutencaoViewModel>
    {

        public ManutencaoController() { dao = new ManutencaoDAO(); }

        /// <summary>
        /// Adiciona Lista de funcionarios, sensores e estados
        /// </summary>
        /// <param name="model"></param>
        protected override void AdicionarViewbagsForm()
        {

            FuncionarioDAO fDAO = new FuncionarioDAO();
            ViewBag.funcionarios = fDAO.Listagem();

            SensorDAO cDAO = new SensorDAO();
            ViewBag.sensores = cDAO.Listagem();

            ViewBag.estados = Enum.GetValues(typeof(ManutencaoDAO.estados)).Cast<ManutencaoDAO.estados>().ToList() ;

        }

  
        protected override void ValidarDados(ManutencaoViewModel model, string operacao)
        {
            base.ValidarDados(model, operacao);


            if (model.data_hora < DateTime.Now && operacao == "I")
            {
                ModelState.AddModelError("data_hora", "Valor invalido, escolha uma data mais para frente");
            }

            if (operacao == "A" )
            {
                if (model.data_hora < new DateTime(1900, 1, 1))
                {
                    ModelState.AddModelError("data_hora", "Valor invalido, escolha uma data mais para frente");

                }

                if (model.estadoId == 0 && model.data_hora > DateTime.Now)
                {
                    ModelState.AddModelError("data_hora", "Escolha data anterior");

                }

            }


        }
    }
}
