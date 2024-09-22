using SitePBL.Models;
using System.Data.SqlClient;
using System.Data;

namespace SitePBL.DAO
{

	public class TemperaturaDAO
	{
		//Criar parametros de temperatura
		private SqlParameter[] CriarParametros(TemperaturaViewModel temperatura)
		{
			SqlParameter[] parametros = new SqlParameter[4];
			parametros[0] = new SqlParameter("data_hora", temperatura.data_hora);
			parametros[1] = new SqlParameter("valor", temperatura.valor);
			parametros[2] = new SqlParameter("fk_sensor_id", temperatura.idSensor);


			return parametros;
		}


		//Monta uma model de temperatura com base do datarow
		private TemperaturaViewModel MontarTemperatura(DataRow registro)
		{
			TemperaturaViewModel temperatura = new TemperaturaViewModel(); ;

			temperatura.data_hora = Convert.ToDateTime(registro["data_hora"]);
			temperatura.valor = Convert.ToInt32(registro["valor"]);
			temperatura.idSensor = Convert.ToInt32(registro["fk_sensor_id"]);
			return temperatura;
		}

		//Classe para inserir um novo temperatura
		//Alterar depois para uma stored precedure
		public void Inserir(TemperaturaViewModel temperatura)
		{

			string sql = "";
			HelperDAO.ExecutarSQL(sql, CriarParametros(temperatura));

		}

		//Classe para excluir um temperatura
		//Adicionar SP
		public void Excluir(int id)
		{

			string sql = "";
			HelperDAO.ExecutarSQL(sql, null);

		}

		//Alterar temperatura
		//Adicionar SP
		public void Alterar(TemperaturaViewModel temperatura)
		{
			string sql = "";
			HelperDAO.ExecutarSQL(sql, CriarParametros(temperatura));

		}


		//Consulta um temperatura
		//Adicionar SP
		public TemperaturaViewModel Consulta(int id)
		{
			string sql = "";

			DataTable tabela = HelperDAO.ExecutaSelect(sql, null);

			if (tabela.Rows.Count == 0)
			{
				return null;
			}

			else
			{
				return MontarTemperatura(tabela.Rows[0]);
			}
		}

		//Lista todos os temperatura
		//Adicionar SP
		public List<TemperaturaViewModel> Listagem()
		{
			List<TemperaturaViewModel> lista = new List<TemperaturaViewModel>();
			string sql = "";
			DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
			foreach (DataRow dr in tabela.Rows)
				lista.Add(MontarTemperatura(dr));
			return lista;
		}


	
	}
}
