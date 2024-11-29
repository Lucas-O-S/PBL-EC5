using System.Data.SqlClient;

namespace SitePBL.DAO
{
    /// <summary>
    /// Classe estatica para criar conexão com o banco de dados
    /// </summary>
    public static class ConexaoDB
    {
        /// <summary>
        /// Cria conexão com o banco de dados
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetConexao()
        {
            string strCon = "Data Source=LOCALHOST;  Database=Termo_Light; user id=sa; password=123456";
            SqlConnection conexao = new SqlConnection(strCon);
            conexao.Open();
            return conexao;
        }
    }
}
