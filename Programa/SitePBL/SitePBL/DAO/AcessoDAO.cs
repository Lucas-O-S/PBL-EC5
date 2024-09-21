using SitePBL.Models;
using System.Data;
using System.Data.SqlClient;

namespace SitePBL.DAO
{
	public class AcessoDAO
	{
		//Criar parametros de acessos
		private SqlParameter[] CriarParametros(AcessoViewModel acesso)
		{
			SqlParameter[] parametros = new SqlParameter[3];
			parametros[0] = new SqlParameter("id", acesso.id);
			parametros[1] = new SqlParameter("senha", acesso.senha);
			parametros[2] = new SqlParameter("fk_empresa_id", acesso.empresa);

			return parametros;
		}


		//Monta uma model de acesso com base do datarow
		private AcessoViewModel MontarAcesso(DataRow registro)
		{
			AcessoViewModel acesso = new AcessoViewModel(); ;

			acesso.id = Convert.ToInt32(registro["id"]);
			acesso.senha = Convert.ToString(registro["senha"]);
			acesso.empresa = Convert.ToInt32(registro["fk_empresa_id"]);
			return acesso;
		}

		//Classe para inserir um novo acesso
		//Alterar depois para uma stored precedure
		public void Inserir (AcessoViewModel acesso)
		{

			string sql = "";
			HelperDAO.ExecutarSQL(sql,CriarParametros(acesso));
		
		}

		//Classe para excluir um acesso
		//Adicionar SP
		public void Excluir(int id)
		{

			string sql = "";
			HelperDAO.ExecutarSQL(sql, null);

		}

		//Alterar Acesso
		//Adicionar SP
		public void Alterar(AcessoViewModel acesso)
		{
			string sql = "";
			HelperDAO.ExecutarSQL(sql, CriarParametros(acesso));

		}


		//Consulta um acesso
		//Adicionar SP
		public AcessoViewModel Consulta(int id)
		{
			string sql = "";

			DataTable tabela = HelperDAO.ExecutaSelect(sql, null);

			if (tabela.Rows.Count == 0)
			{
				return null;
			}

			else
			{
				return MontarAcesso(tabela.Rows[0]);
			}
		}

		//Lista todos os acessos
		//Adicionar SP
		public List<AcessoViewModel> Listagem()
		{
			List<AcessoViewModel> lista = new List<AcessoViewModel>();
			string sql = "";
			DataTable tabela = HelperDAO.ExecutaSelect(sql,null);
			foreach(DataRow dr in tabela.Rows)
				lista.Add(MontarAcesso(dr));
			return lista;
		}


		//busca proximo id
		//Adicionar SP	
		public int ProximoID()
		{
			string sql = "";
			DataTable tabela = HelperDAO.ExecutaSelect(sql,null);
			return Convert.ToInt32(tabela.Rows[0]["Maior"]);
		}


	}
}
