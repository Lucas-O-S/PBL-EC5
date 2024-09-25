using SitePBL.Models;
using System.Data.SqlClient;
using System.Data;
using System.Xml.Serialization;

namespace SitePBL.DAO
{

	public class TemperaturaDAO : PadraoDAO<TemperaturaViewModel>
    {
        protected override void SetTabela() { nomeTabela = "temperatura"; }

        //Criar parametros de temperatura
        protected override SqlParameter[] CriaParametrosNoId(TemperaturaViewModel temperatura)
		{
			SqlParameter[] parametros = new SqlParameter[3];
			parametros[0] = new SqlParameter("data_hora", temperatura.data_hora);
			parametros[1] = new SqlParameter("valor", temperatura.valor);
			parametros[2] = new SqlParameter("fk_sensor_id", temperatura.idSensor);


			return parametros;
        }

		//Não usar, não há ID
        protected override SqlParameter[] CriaParametrosId(TemperaturaViewModel temperatura)
        {
     

            return null;
        }


        //Monta uma model de temperatura com base do datarow
        protected override TemperaturaViewModel MontaModel(DataRow registro)
		{
			TemperaturaViewModel temperatura = new TemperaturaViewModel(); ;

			temperatura.data_hora = Convert.ToDateTime(registro["data_hora"]);
			temperatura.valor = Convert.ToInt32(registro["valor"]);
			temperatura.idSensor = Convert.ToInt32(registro["fk_sensor_id"]);
			return temperatura;
		}

        public override void Delete(TemperaturaViewModel temperatura)
		{

			HelperDAO.ExecutaProc("sp_delete_temperatura", CriaParametrosNoId(temperatura));
		}




    }
}
