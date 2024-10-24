﻿using SitePBL.Models;
using System.Data.SqlClient;
using System.Data;

namespace SitePBL.DAO
{
    public class SensorDAO : PadraoDAO<SensorViewModel>
    {
        protected override void SetTabela() { nomeTabela = "sensor"; }

        //Criar parametros de Sensor usando ID
        protected override SqlParameter[] CriaParametros(SensorViewModel Sensor)
        {
            if (Sensor.id != 0)
            {
                SqlParameter[] parametros = new SqlParameter[3];
                parametros[0] = new SqlParameter("id", Sensor.id);
                if (Sensor.descricao != null)
                {
                    parametros[1] = new SqlParameter("descricao", Sensor.descricao);

                }
                else
                    parametros[1] = new SqlParameter("descricao", DBNull.Value);

                if (Sensor.empresa != null)
                    parametros[2] = new SqlParameter("fk_empresa_id", Sensor.empresa);
                else
                    parametros[2] = new SqlParameter("fk_empresa_id", DBNull.Value);


                return parametros;
            }
            else
            {
                SqlParameter[] parametros = new SqlParameter[2];
                if (Sensor.descricao != null)
                {
                    parametros[0] = new SqlParameter("descricao", Sensor.descricao);

                }
                else
                    parametros[0] = new SqlParameter("descricao", DBNull.Value);

                if (Sensor.empresa != null)
                    parametros[1] = new SqlParameter("fk_empresa_id", Sensor.empresa);
                else
                    parametros[1] = new SqlParameter("fk_empresa_id", DBNull.Value);


                return parametros;
            }
            
        }

     

        //Monta uma model de Sensor com base do datarow
        protected override SensorViewModel MontaModel(DataRow registro)
        {
            SensorViewModel sensor = new SensorViewModel(); ;

            sensor.id = Convert.ToInt32(registro["id"]);
            if (registro["descricao"] != DBNull.Value)
                sensor.descricao = Convert.ToString(registro["descricao"]);
            if (registro["fk_empresa_id"] != DBNull.Value)
                sensor.empresa = Convert.ToInt32(registro["fk_empresa_id"]);
            return sensor;
        }

        public List<SensorViewModel> Listagem()
        {
            string sql = "select * from sensor as s order by s.descricao";
            List<SensorViewModel> lista = new List<SensorViewModel>();

            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro));
            return lista;
        }



        public int TesteId(int? id)
        {
            string sql = $"select s.descricao, s.fk_empresa_id from sensor as s right join empresa as e " +
                $"on s.fk_empresa_id = e.id where e.id = {id}";

            DataTable tab = HelperDAO.ExecutaSelect(sql, null);
            if (tab.Rows.Count == 0)
                return 0;
            else
                return 1;
        }
    }
}
