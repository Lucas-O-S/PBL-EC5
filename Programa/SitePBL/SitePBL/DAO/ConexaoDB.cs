using System.Data.SqlClient;

namespace SitePBL.DAO
{
    public class ConexaoDB
    {
        public static SqlConnection GetConexao()
        {
            string strCon = "Data Source=LOCALHOST\\sqlexpress;  Database=Termo_Light; user id=sa; password=123456";
            SqlConnection conexao = new SqlConnection(strCon);
            conexao.Open();
            return conexao;
        }
    }
}
