﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        /// <summary>
        /// Adiciona uma viewbag a index
        /// </summary>
        protected override void AdicionarViewbagsIndex()
        {
            //Transforma um enumerador em uma lista
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

            if (model.idFuncionario < 0)
            {
                ModelState.AddModelError("idFuncionario", "Selecione um Funcionario");
            }
            if (model.estadoId < 0)
            {
                ModelState.AddModelError("estadoId", "Selecione um estado");
            }
            if (model.idSensor < 0)
            {
                ModelState.AddModelError("idSensor", "Selecione um Sensor");
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

        public override IActionResult Dashboard()
        {
            int id = 0;
            List<ManutencaoViewModel> empresas = new List<ManutencaoViewModel>();
            List<ManutencaoViewModel> empresas01 = new List<ManutencaoViewModel>();
            var dao = new ManutencaoDAO(); 

            // Chama o método para obter as quantidades de empresas
            empresas = dao.ObterQuantidadesEmpresas(id, empresas);
            empresas01 = dao.ObterSensoresManutencao(id, empresas01);

            // Converte os dados para JSON para usar no JavaScript
            ViewBag.EmpresasData = Newtonsoft.Json.JsonConvert.SerializeObject(empresas);
            ViewBag.ManutencaoData = Newtonsoft.Json.JsonConvert.SerializeObject(empresas01);

            return View();
        }
    }
}
