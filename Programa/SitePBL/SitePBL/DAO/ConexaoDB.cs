using System.Data.SqlClient;

namespace SitePBL.DAO
{
    public class ConexaoDB
    {
        public static SqlConnection GetConexao()
        {
            string strCon = "Data Source=LOCALHOST\\SQLEXPRESS;  Database=Termo_Light; user id=SA; password=123456";
            SqlConnection conexao = new SqlConnection(strCon);
            conexao.Open();
            return conexao;
        }
    }
}
