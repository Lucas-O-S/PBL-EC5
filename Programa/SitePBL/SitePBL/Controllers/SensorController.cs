﻿using Microsoft.AspNetCore.Mvc;
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

    }
}
