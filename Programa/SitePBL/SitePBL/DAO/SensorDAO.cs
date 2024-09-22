using SitePBL.Models;
using System.Data.SqlClient;
using System.Data;

namespace SitePBL.DAO
{
	public class SensorDAO
	{
		//Criar parametros de Sensor
		private SqlParameter[] CriarParametros(SensorViewModel Sensor)
		{
			SqlParameter[] parametros = new SqlParameter[3];
			parametros[0] = new SqlParameter("id", Sensor.id);
			if (Sensor.id != null)
			{
				parametros[1] = new SqlParameter("descricao", Sensor.descricao);

			}
			else
				parametros[1] = new SqlParameter("descricao", DBNull.Value);

			parametros[2] = new SqlParameter("fk_empresa_id", Sensor.empresa);


			return parametros;
		}


		//Monta uma model de Sensor com base do datarow
		private SensorViewModel MontarSensor(DataRow registro)
		{
			SensorViewModel sensor = new SensorViewModel(); ;

			sensor.id = Convert.ToInt32(registro["id"]);
			if (registro["descricao"] != DBNull.Value)
				sensor.descricao = Convert.ToString(registro["descricao"]);
			sensor.empresa = Convert.ToInt32(registro["fk_empresa_id"]);
			return sensor;
		}

		//Classe para inserir um novo Sensor
		//Alterar depois para uma stored precedure
		public void Inserir(SensorViewModel sensor)
		{

			string sql = "";
			HelperDAO.ExecutarSQL(sql, CriarParametros(sensor));

		}

		//Classe para excluir um Sensor
		//Adicionar SP
		public void Excluir(int id)
		{

			string sql = "";
			HelperDAO.ExecutarSQL(sql, null);

		}

		//Alterar Sensor
		//Adicionar SP
		public void Alterar(SensorViewModel sensor)
		{
			string sql = "";
			HelperDAO.ExecutarSQL(sql, CriarParametros(sensor));

		}


		//Consulta um Sensor
		//Adicionar SP
		public SensorViewModel Consulta(int id)
		{
			string sql = "";

			DataTable tabela = HelperDAO.ExecutaSelect(sql, null);

			if (tabela.Rows.Count == 0)
			{
				return null;
			}

			else
			{
				return MontarSensor(tabela.Rows[0]);
			}
		}

		//Lista todos os Sensor
		//Adicionar SP
		public List<SensorViewModel> Listagem()
		{
			List<SensorViewModel> lista = new List<SensorViewModel>();
			string sql = "";
			DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
			foreach (DataRow dr in tabela.Rows)
				lista.Add(MontarSensor(dr));
			return lista;
		}


		//busca proximo id
		//Adicionar SP	
		public int ProximoID()
		{
			string sql = "";
			DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
			return Convert.ToInt32(tabela.Rows[0]["Maior"]);
		}
	}
}
