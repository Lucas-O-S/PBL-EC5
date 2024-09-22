using SitePBL.Models;
using System.Data.SqlClient;
using System.Data;

namespace SitePBL.DAO
{
	public class ManutencaoDAO
	{
		//Criar parametros de manutencao 
		private SqlParameter[] CriarParametros(ManutencaoViewModel manutencao )
		{
			SqlParameter[] parametros = new SqlParameter[4];
			parametros[0] = new SqlParameter("id", manutencao.data_hora);
			parametros[1] = new SqlParameter("fk_sensor_id", manutencao.idSensor);

			parametros[2] = new SqlParameter("fk_funcionario_id", manutencao.idFuncionario) ;

			parametros[3] = new SqlParameter("estado", manutencao.estado);


			return parametros;
		}


		//Monta uma model de manutencao  com base do datarow
		private ManutencaoViewModel MontarManutencao(DataRow registro)
		{
			ManutencaoViewModel manutencao = new ManutencaoViewModel(); ;

			manutencao.data_hora = Convert.ToDateTime(registro["data_hora"]);
			manutencao.idFuncionario = Convert.ToInt32(registro["fk_funcionario_id"]);
			manutencao.idSensor = Convert.ToInt32(registro["fk_sensor_id"]);
			manutencao.estado = Convert.ToString(registro["estado"]);
			return manutencao;
		}

		//Classe para inserir um novo manutencao 
		//Alterar depois para uma stored precedure
		public void Inserir(ManutencaoViewModel manutencao)
		{

			string sql = "";
			HelperDAO.ExecutarSQL(sql, CriarParametros(manutencao));

		}

		//Classe para excluir um manutencao 
		//Adicionar SP
		public void Excluir(int id)
		{

			string sql = "";
			HelperDAO.ExecutarSQL(sql, null);

		}

		//Alterar manutencao 
		//Adicionar SP
		public void Alterar(ManutencaoViewModel funcionario)
		{
			string sql = "";
			HelperDAO.ExecutarSQL(sql, CriarParametros(funcionario));

		}


		//Consulta um manutencao 
		//Adicionar SP
		public ManutencaoViewModel Consulta(int id)
		{
			string sql = "";

			DataTable tabela = HelperDAO.ExecutaSelect(sql, null);

			if (tabela.Rows.Count == 0)
			{
				return null;
			}

			else
			{
				return MontarManutencao(tabela.Rows[0]);
			}
		}

		//Lista todos os manutencao 
		//Adicionar SP
		public List<ManutencaoViewModel> Listagem()
		{
			List<ManutencaoViewModel> lista = new List<ManutencaoViewModel>();
			string sql = "";
			DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
			foreach (DataRow dr in tabela.Rows)
				lista.Add(MontarManutencao(dr));
			return lista;
		}



	}
}
