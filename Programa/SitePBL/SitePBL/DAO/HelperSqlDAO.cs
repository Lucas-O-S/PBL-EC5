using System.Data;
using System.Data.SqlClient;

namespace SitePBL.DAO
{
    public static class HelperSqlDAO
    {
		/// <summary>
        /// Executa um precedure sql
        /// </summary>
        /// <param name="sql">string sql da chamada</param>
        /// <param name="parametros">parametros a serem utilizados</param>
		public static void ExecutaProc(string sql, SqlParameter[] parametros)
		{
            ///Cria conexão
			using (SqlConnection conexao = ConexaoDB.GetConexao())
			{
                /// Cria comandos
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
        /// Executa um select atraves de sp e devolve uma data table
        /// </summary>
        /// <param name="sql">string sql da chamada</param>
        /// <param name="parametros">parametros a serem utilizados</param>
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
        /// <param name="id">id que sera usado</param>
        /// <returns>SqlParameter id</returns>
        public static SqlParameter[] CriarParametros(int id)
        {
            return new SqlParameter[]
            {
                new SqlParameter("id", id)
            };
        }

        /// <summary>
        /// /Cria parametros sql somente com id e tabela
        /// </summary>
        /// <param name="id">id da consulta</param>
        /// <param name="tabela">nome da tabela</param>
        /// <returns></returns>
        public static SqlParameter[] CriarParametros(int id, string tabela)
        {
            return new SqlParameter[]
            {
                new SqlParameter("id", id),
                new SqlParameter("tabela", tabela)

            };
        }


    }
}
