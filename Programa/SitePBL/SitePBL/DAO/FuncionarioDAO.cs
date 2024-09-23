using SitePBL.Models;
using System.Data.SqlClient;
using System.Data;

namespace SitePBL.DAO
{
	public class FuncionarioDAO
	{
		//Criar parametros de funcionario
		private SqlParameter[] CriarParametros(FuncionarioViewModel funcionario)
		{
			SqlParameter[] parametros = new SqlParameter[4];
			parametros[0] = new SqlParameter("id", funcionario.id);
			parametros[1] = new SqlParameter("nome", funcionario.nome);
			if (funcionario.foto != null)
			{
				//evita problemas de conversão de foto
                parametros[2] = new SqlParameter("foto", SqlDbType.VarBinary)
                {
                    Value = funcionario.foto

                };
            }
			else
			{
                parametros[2] = new SqlParameter("foto", SqlDbType.VarBinary)
                {
                    Value = DBNull.Value

                };

            }
			parametros[3] = new SqlParameter("cargo", funcionario.cargo);


			return parametros;
		}


		//Monta uma model de funcionario com base do datarow
		private FuncionarioViewModel MontarFuncionario(DataRow registro)
		{
			FuncionarioViewModel funcionario = new FuncionarioViewModel(); ;

			funcionario.id = Convert.ToInt32(registro["id"]);
			funcionario.nome = Convert.ToString(registro["nome"]);
			if (registro["foto"] != DBNull.Value)
			{
				funcionario.foto = (byte[])registro["foto"];

			}
			funcionario.cargo = Convert.ToString(registro["cargo"]);
			return funcionario;
		}

		//Classe para inserir um novo funcionario
		//Alterar depois para uma stored precedure
		public void Inserir(FuncionarioViewModel empresa)
		{

			string sql = "";
			HelperDAO.ExecutarSQL(sql, CriarParametros(empresa));

		}

		//Classe para excluir um funcionario
		//Adicionar SP
		public void Excluir(int id)
		{

			string sql = "";
			HelperDAO.ExecutarSQL(sql, null);

		}

		//Alterar funcionario
		//Adicionar SP
		public void Alterar(FuncionarioViewModel funcionario)
		{
			string sql = "";
			HelperDAO.ExecutarSQL(sql, CriarParametros(funcionario));

		}


		//Consulta um funcionario
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
				return MontarFuncionario(tabela.Rows[0]);
			}
		}

		//Lista todos os funcionario
		//Adicionar SP
		public List<FuncionarioViewModel> Listagem()
		{
			List<FuncionarioViewModel> lista = new List<FuncionarioViewModel>();
			string sql = "";
			DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
			foreach (DataRow dr in tabela.Rows)
				lista.Add(MontarFuncionario(dr));
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
