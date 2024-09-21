using SitePBL.Models;
using System.Data.SqlClient;
using System.Data;

namespace SitePBL.DAO
{
	public class FuncionarioDAO
	{
		//Criar parametros de acessos
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


		//Monta uma model de acesso com base do datarow
		private FuncionarioViewModel MontarEmpresas(DataRow registro)
		{
			FuncionarioViewModel funcionario = new FuncionarioViewModel(); ;

			funcionario.id = Convert.ToInt32(registro["id"]);
			funcionario.nome = Convert.ToString(registro["nome"]);
			if (registro["logo"] != DBNull.Value)
			{
				funcionario.foto = (byte[])registro["foto"];

			}
			funcionario.cargo = Convert.ToString(registro["cargo"]);
			return funcionario;
		}

		//Classe para inserir um novo acesso
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

		//Alterar Acesso
		//Adicionar SP
		public void Alterar(EmpresaViewModel empresa)
		{
			string sql = "";
			HelperDAO.ExecutarSQL(sql, CriarParametros(empresa));

		}


		//Consulta um acesso
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

		//Lista todos os acessos
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
