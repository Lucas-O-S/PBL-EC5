using Microsoft.AspNetCore.Mvc;
using SitePBL.DAO;
using SitePBL.Models;
using System.Data.SqlTypes;


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

        protected override void AdicionarViewbagsIndex()
        {
            ViewBag.estados = Enum.GetValues(typeof(ManutencaoDAO.estados)).Cast<ManutencaoDAO.estados>().ToList();
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

		public IActionResult BuscaAvancada(
            DateTime data_hora_inicial,
			DateTime data_hora_final,
			string funcionario,
			string empresa,
			string sensor,
			int estado)
		{
			try
			{
				if (data_hora_inicial < SqlDateTime.MinValue.Value || data_hora_inicial > SqlDateTime.MaxValue.Value)
					data_hora_inicial = SqlDateTime.MinValue.Value;
                if (data_hora_final < SqlDateTime.MinValue.Value || data_hora_final > SqlDateTime.MaxValue.Value)
                    data_hora_final = SqlDateTime.MaxValue.Value ;

				if (string.IsNullOrEmpty(empresa))
					empresa = "";
                
				if (string.IsNullOrEmpty(funcionario))
                    funcionario = "";
                
				if (string.IsNullOrEmpty(sensor))
					sensor = "";

				ManutencaoDAO mDAO = new ManutencaoDAO();
				var lista = mDAO.BuscaAvancada(data_hora_inicial,data_hora_final,funcionario,empresa,sensor,estado);
				return PartialView("pvManutencao", lista);

			}
			catch (Exception erro)
			{
				return View("Error", new ErrorViewModel(erro.ToString()));
			}
		}
	}
}
