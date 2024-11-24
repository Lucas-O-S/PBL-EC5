﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SitePBL.DAO;
using SitePBL.Models;
using System.Data;
using System.Runtime.Intrinsics.Arm;

namespace SitePBL.Controllers
{

    public class SensorController : PadraoController<SensorViewModel>
    {
        public SensorController() { dao = new SensorDAO(); }

        private async Task ValidarDadosFiware(SensorViewModel model, string operacao)
        {
            if (!await HelperFiwareDAO.VerificarServer(HelperFiwareDAO.host))
            {
                ModelState.AddModelError("descricao", "Server indisponivel no momento");
            }
            if (ModelState.IsValid && operacao == "I")
            {
                HelperFiwareDAO.CriarLamp(HelperFiwareDAO.host, model.descricao);
            }
        }

        protected override void AdicionarViewbagsForm()
        {
            EmpresaDAO eDAO = new EmpresaDAO();
            ViewBag.empresas = eDAO.Listagem();

        }
        protected override void AdicionarViewbagsIndex()
        {
            EmpresaDAO eDAO = new EmpresaDAO();
            ViewBag.empresas = eDAO.Listagem();
        }


        protected override void ValidarDados(SensorViewModel model, string operacao)
        {
            base.ValidarDados(model, operacao);

            if (operacao == "I")
            {
                SensorDAO sDAO = new SensorDAO();

                if (sDAO.VerificarSensoresRepetidos(model.descricao) > 0)
                    ModelState.AddModelError("descricao", "Já existe este sensor, escolha outra descrição");


                if (ModelState.IsValid)
                    ValidarDadosFiware(model, operacao).GetAwaiter().GetResult();
            }

        }

        public IActionResult BuscaAvancada(string descricao, string empresa, int tipo)
        {
            try
            {
                if (string.IsNullOrEmpty(descricao))
                    descricao = "";
				if (string.IsNullOrEmpty(empresa))
					empresa = "";
				SensorDAO sDAO = new SensorDAO();
                var lista = sDAO.BuscaAvancada(descricao,empresa,tipo);
				return PartialView("pvSensor",lista);

			}
			catch (Exception erro)
			{
				return View("Error", new ErrorViewModel(erro.ToString()));
			}
        }

        public IActionResult TrocaMalha(string malha)
        {
            ViewBag.Malha = malha;
            return View("Dashboard",null);
        }

        public static async Task<List<SensorViewModel>> PegarUltimos50Dados(string host)
        {
            List<SensorViewModel> listaDados = new List<SensorViewModel>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = $"{host}/v2/entities?limit=50&orderBy=!dateCreated";
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        listaDados = JsonConvert.DeserializeObject<List<SensorViewModel>>(jsonResponse);
                    }
                }
            }
            catch (Exception)
            {
                // Tratamento de erro
            }

            return listaDados;
        }

        public async Task<IActionResult> PegarUltimosDados()
        {
            try
            {
                // Chama o método que consulta os últimos 50 dados no Fiware
                var dados = await PegarUltimos50Dados(HelperFiwareDAO.host);    

                if (dados == null)
                {
                    ModelState.AddModelError("descricao", "Não foi possível obter dados do Fiware.");
                    return View("Dashboard", null); // Ou outra View que faça sentido
                }

                // Supondo que você vai retornar esses dados para uma View chamada "Dashboard"
                return View("Dashboard", dados);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

    }
}
