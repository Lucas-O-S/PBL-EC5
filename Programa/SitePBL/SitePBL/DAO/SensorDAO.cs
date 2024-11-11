using SitePBL.Models;
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

                if (Sensor.empresaId != null)
                    parametros[2] = new SqlParameter("fk_empresa_id", Sensor.empresaId);
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

                if (Sensor.empresaId != null)
                    parametros[1] = new SqlParameter("fk_empresa_id", Sensor.empresaId);
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
                sensor.empresaId = Convert.ToInt32(registro["fk_empresa_id"]);
            return sensor;
        }

  
    }
}
