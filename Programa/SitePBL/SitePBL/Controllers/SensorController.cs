using Microsoft.AspNetCore.Mvc;
using SitePBL.DAO;
using SitePBL.Models;
using System.Runtime.Intrinsics.Arm;

namespace SitePBL.Controllers
{
    public class SensorController : Controller
    {
        private string tabela = "sensor";
        private string ordem = "descricao";
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Listagem()
        {
            try
            {
                SensorDAO dao = new SensorDAO();
                List<SensorViewModel> lista = dao.ListagemJoin();
                return View("Listagem", lista);
            }
            catch (Exception ex)
            {
                return View("error", new ErrorViewModel(ex.ToString()));
            }
        }
        public IActionResult Delete(int id)
        {
            try
            {
                SensorDAO dao = new SensorDAO();
                dao.Excluir(id, tabela);
                return RedirectToAction("Listagem");
            }
            catch (Exception ex)
            {
                return View("error", new ErrorViewModel(ex.ToString()));
            }
        }

        public IActionResult Edit(int id)
        {
            try
            {
                ViewBag.Operacao = "A";
                SensorDAO dao = new SensorDAO();
                SensorViewModel sensor = dao.Consulta(id, tabela);
                if (sensor == null)
                    return RedirectToAction("Listagem");
                else
                    return View("Form", sensor);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        public IActionResult Create()
        {
            try
            {
                ViewBag.Operacao = "I";
                SensorViewModel sensor = new SensorViewModel();

                SensorDAO dao = new SensorDAO();
                return View("Form", sensor);
            }
            catch (Exception ex)
            {
                return View("error", new ErrorViewModel(ex.ToString()));
            }
        }

        private void ValidaDados(SensorViewModel sensor, string operacao)
        {
            ModelState.Clear(); // limpa os erros criados automaticamente pelo Asp.net (que podem estar com msg em inglês)
            SensorDAO dao = new SensorDAO();

            if (string.IsNullOrEmpty(sensor.descricao))
                ModelState.AddModelError("nome", "Campo obrigatório. Preencha o nome.");
            if (sensor.empresa <= 0 && dao.TesteId(sensor) == false)
                ModelState.AddModelError("cargo", "Campo obrigatório. Preencha o cargo");
        }

        public IActionResult Enviar(SensorViewModel sensor, string Operacao)
        {
            try
            {
                ValidaDados(sensor, Operacao);
                if (ModelState.IsValid)
                {
                    SensorDAO dao = new SensorDAO();
                    if (Operacao == "I")
                        dao.Inserir(sensor);
                    else
                        dao.Alterar(sensor);

                    return RedirectToAction("listagem");
                }
                else
                {
                    ViewBag.Operacao = Operacao;
                    return View("Form", sensor);
                }
            }
            catch (Exception ex)
            {
                return View("error", new ErrorViewModel(ex.ToString()));
            }
        }
    }
}
