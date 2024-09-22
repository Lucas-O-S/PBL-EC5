using SitePBL.Models;
using System.Data.SqlClient;
using System.Data;

namespace SitePBL.DAO
{
	public class EmpresaDAO
	{
		//Criar parametros de empresa
		private SqlParameter[] CriarParametros(EmpresaViewModel empresa)
		{
			SqlParameter[] parametros = new SqlParameter[4];
			parametros[0] = new SqlParameter("id", empresa.id);
			parametros[1] = new SqlParameter("nome", empresa.nome);
			if (empresa.logo != null)
			{
				parametros[2] = new SqlParameter("logo", empresa.logo);

			}
			else
			{
				parametros[2] = new SqlParameter("logo", DBNull.Value);


			}
			parametros[3] = new SqlParameter("sede", empresa.sede);


			return parametros;
		}


		//Monta uma model de empresa com base do datarow
		private EmpresaViewModel MontarEmpresas(DataRow registro)
		{
			EmpresaViewModel empresa = new EmpresaViewModel(); ;

			empresa.id = Convert.ToInt32(registro["id"]);
			empresa.nome = Convert.ToString(registro["nome"]);
			if (registro["logo"] != DBNull.Value)
			{
				empresa.logo = (byte[])registro["logo"];

			}
			empresa.sede = Convert.ToString(registro["sede"]);
			return empresa;
		}

		//Classe para inserir um novo empresa
		//Alterar depois para uma stored precedure
		public void Inserir(EmpresaViewModel empresa)
		{

			string sql = "";
			HelperDAO.ExecutarSQL(sql, CriarParametros(empresa));

		}

		//Classe para excluir um acesso
		//Adicionar SP
		public void Excluir(int id)
		{

			string sql = "";
			HelperDAO.ExecutarSQL(sql, null);

		}

		//Alterar empresa
		//Adicionar SP
		public void Alterar(EmpresaViewModel empresa)
		{
			string sql = "";
			HelperDAO.ExecutarSQL(sql, CriarParametros(empresa));

		}


		//Consulta um empresa
		//Adicionar SP
		public EmpresaViewModel Consulta(int id)
		{
			string sql = "";

			DataTable tabela = HelperDAO.ExecutaSelect(sql, null);

			if (tabela.Rows.Count == 0)
			{
				return null;
			}

			else
			{
				return MontarEmpresas(tabela.Rows[0]);
			}
		}

		//Lista todos os empresa
		//Adicionar SP
		public List<EmpresaViewModel> Listagem()
		{
			List<EmpresaViewModel> lista = new List<EmpresaViewModel>();
			string sql = "";
			DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
			foreach (DataRow dr in tabela.Rows)
				lista.Add(MontarEmpresas(dr));
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
