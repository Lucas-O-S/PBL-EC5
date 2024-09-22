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
			parametros[1] = new SqlParameter("descricao", Sensor.descricao);
			parametros[2] = new SqlParameter("fk_empresa_id", Sensor.empresa);


			return parametros;
		}


		//Monta uma model de Sensor com base do datarow
		private SensorViewModel MontarSensor(DataRow registro)
		{
			SensorViewModel funcionario = new SensorViewModel(); ;

			funcionario.id = Convert.ToInt32(registro["id"]);
			funcionario.nome = Convert.ToString(registro["nome"]);
			funcionario.cargo = Convert.ToString(registro["fk_empresa_id"]);
			return funcionario;
		}

		//Classe para inserir um novo Sensor
		//Alterar depois para uma stored precedure
		public void Inserir(FuncionarioViewModel empresa)
		{

			string sql = "";
			HelperDAO.ExecutarSQL(sql, CriarParametros(empresa));

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
		public void Alterar(FuncionarioViewModel funcionario)
		{
			string sql = "";
			HelperDAO.ExecutarSQL(sql, CriarParametros(funcionario));

		}


		//Consulta um Sensor
		//Adicionar SP
		public FuncionarioViewModel Consulta(int id)
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
		public List<FuncionarioViewModel> Listagem()
		{
			List<FuncionarioViewModel> lista = new List<FuncionarioViewModel>();
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
