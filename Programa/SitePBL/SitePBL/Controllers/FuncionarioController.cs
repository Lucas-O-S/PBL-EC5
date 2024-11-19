using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SitePBL.DAO;
using SitePBL.Models;
using static SitePBL.DAO.ManutencaoDAO;
using System.Data.SqlTypes;

namespace SitePBL.Controllers
{
    public class FuncionarioController : PadraoController<FuncionarioViewModel>
    {
        public FuncionarioController() { dao = new FuncionarioDAO(); }

        protected override void ValidarDados(FuncionarioViewModel model, string operacao)
        {
            base.ValidarDados(model, operacao);
            if (string.IsNullOrEmpty(model.nome))
                ModelState.AddModelError("nome", "Campo obrigatório! Preencha o nome!");
            if (string.IsNullOrEmpty(model.cargo))
                ModelState.AddModelError("cargo", "Campo obrigatório! Preencha o cargo!");
            if (model.dataContratacao > DateTime.Now || model.dataContratacao < new DateTime(1900, 1, 1))
                ModelState.AddModelError("dataContratacao", "Digite uma data válida!");
            if (model.imagem == null && operacao == "I")
                ModelState.AddModelError("foto", "Adicione uma imagem");

            if (model.imagem != null && model.imagem.Length / 1024 / 1024 >= 2)
                ModelState.AddModelError("foto", "Imagem limitada a 2 mb.");

            if (ModelState.IsValid)
            {
                if (operacao == "A" && model.imagem == null)
                {
                    FuncionarioViewModel temp = dao.Consulta(model.id);
                    model.imagembyte = temp.imagembyte;

                }
                else
                {
                    model.imagembyte = model.ConvertImageToByte(model?.imagem);

                }
            }
        }
        public IActionResult BuscaAvancada
            (
            DateTime data_hora_inicial,
            DateTime data_hora_final,
            string nome,
            string cargo
            )
        {
            try
            {
                if (data_hora_inicial < SqlDateTime.MinValue.Value || data_hora_inicial > SqlDateTime.MaxValue.Value)
                    data_hora_inicial = SqlDateTime.MinValue.Value;
                if (data_hora_final < SqlDateTime.MinValue.Value || data_hora_final > SqlDateTime.MaxValue.Value)
                    data_hora_final = SqlDateTime.MaxValue.Value;
                if (string.IsNullOrEmpty(nome))
                    nome = "";

                if (string.IsNullOrEmpty(cargo))
                    cargo = "";

                FuncionarioDAO fDAO = new FuncionarioDAO();
                var lista = fDAO.BuscaAvancada(data_hora_inicial, data_hora_final, nome, cargo);
                return PartialView("pvGridFuncionario", lista);

            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
    }
}
