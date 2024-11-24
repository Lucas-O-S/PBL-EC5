using Microsoft.AspNetCore.Mvc;
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

            if (model.empresaId < 0)
            {
                ModelState.AddModelError("empresaId", "Selecione um Sensor");
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
                var lista = sDAO.BuscaAvancada(descricao, empresa, tipo);
                return PartialView("pvSensor", lista);

            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult TrocaMalha(string malha)
        {
            ViewBag.Malha = malha;
            return View("Dashboard", null);
        }

        public static async Task<List<SensorViewModel>> PegarUltimos50Dados(string host, string lamp)
        {
            List<SensorViewModel> listaDados = new List<SensorViewModel>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Construção correta da URL
                    string url = $"http://{host}:8666/v2/entities/urn:ngsi-ld:Lamp:{lamp}/attrs/temperature?lastN=50";

                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        // Ajuste da desserialização com base no formato esperado
                        listaDados = JsonConvert.DeserializeObject<List<SensorViewModel>>(jsonResponse);
                    }
                    else
                    {
                        // Log de erro em caso de falha na requisição
                        Console.WriteLine($"Erro ao acessar API: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Tratamento de erro com log da exceção
                Console.WriteLine($"Erro ao buscar dados: {ex.Message}");
            }

            return listaDados;
        }

        public async Task<IActionResult> PegarUltimosDados()
        {
            try
            {
                // Nome específico da lâmpada (ou sensor) que deseja consultar
                string lamp = "03y";
                var dados = await PegarUltimos50Dados(HelperFiwareDAO.host, lamp);

                if (dados == null || dados.Count == 0)
                {
                    ModelState.AddModelError("descricao", "Não foi possível obter dados do Fiware.");
                    return View("Dashboard", null); // Ou outra View que faça sentido
                }

                // Retorna os dados para a View chamada "Dashboard"
                return View("Dashboard", dados);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

    }
}
