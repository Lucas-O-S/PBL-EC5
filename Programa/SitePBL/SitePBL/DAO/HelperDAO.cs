using System.Data;
using System.Data.SqlClient;

namespace SitePBL.DAO
{
    public static class HelperDAO
    {
		//Executa um comando sql
        public static void ExecutarSQL(string sql, SqlParameter[] parametros)
        {
            using (SqlConnection conexao = ConexaoDB.GetConexao())
            {
                using (SqlCommand comando = new SqlCommand(sql, conexao))
                {
                    if (parametros != null)
                        comando.Parameters.AddRange(parametros);
                    comando.ExecuteNonQuery();

                    conexao.Close();
                }
            }

        }
		
		//Executa um select sql
        public static DataTable ExecutaSelect(string sql, SqlParameter[] parametros)
        {
            using (SqlConnection conexao = ConexaoDB.GetConexao())
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conexao))
                {
                    if (parametros != null)
                        adapter.SelectCommand.Parameters.AddRange(parametros);
                    DataTable tabela = new DataTable();
                    adapter.Fill(tabela);
                    conexao.Close();
                    return tabela;

                }
            }
        }

		//Executa um precedure sql
		public static void ExecutaProc(string sql, SqlParameter[] parametros)
		{
			using (SqlConnection conexao = ConexaoDB.GetConexao())
			{
				using (SqlCommand comando = new SqlCommand(sql, conexao))
				{
					comando.CommandType = CommandType.StoredProcedure;
					if (parametros != null)
						comando.Parameters.AddRange(parametros);
					comando.ExecuteNonQuery();
					conexao.Close();

				}
			}
		}

		/// <summary>
        /// Executa um select  atravez de sp
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parametros"></param>
        /// <returns>DataTable</returns>
		public static DataTable ExecutaProcSelect(string sql, SqlParameter[] parametros)
		{
			using (SqlConnection conexao = ConexaoDB.GetConexao())
			{
				using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conexao))
				{
					if (parametros != null)
						adapter.SelectCommand.Parameters.AddRange(parametros);

					adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

					DataTable tabela = new DataTable();
					adapter.Fill(tabela);
					conexao.Close();

					return tabela;

				}
			}
		}

        /// <summary>
        /// Criar um parametro sql com o id somente
        /// </summary>
        /// <param name="id"></param>
        /// <returns>SqlParameter id</returns>
        public static SqlParameter[] CriarParametroID(int id)
        {
            return new SqlParameter[]
            {
                new SqlParameter("id", id)
            };
        }

        
    }
}
