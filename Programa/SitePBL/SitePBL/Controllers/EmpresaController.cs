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

            if (string.IsNullOrEmpty(model.nome))
            {
                ModelState.AddModelError("nome","Nome está vazio");
            }
            if (string.IsNullOrEmpty(model.sede))
            {
                ModelState.AddModelError("sede", "Sede está vazia");
            }
            if (model.imagem == null && operacao == "I")
                ModelState.AddModelError("foto", "Adicione uma imagem");

            if (model.imagem != null && model.imagem.Length / 1024 / 1024 >= 2)
                ModelState.AddModelError("foto", "Imagem limitada a 2 mb.");

            if (ModelState.IsValid)
            {
                if (operacao == "A" && model.imagem == null)
                {
                    EmpresaViewModel temp = dao.Consulta(model.id);
                    model.imagembyte = temp.imagembyte;

                }
                else
                {
                    model.imagembyte = model.ConvertImageToByte(model.imagem);

                }
            }

        }
    }


}
